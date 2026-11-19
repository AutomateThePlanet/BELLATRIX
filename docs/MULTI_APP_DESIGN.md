# Multi-app support for Bellatrix.Desktop: design

Status: proposal (investigation only, no code changed)
Date: 2026-11-17
Scope: `src/Bellatrix.Desktop`, `src/Bellatrix.Core` (one additive method), `tests/Bellatrix.Desktop.Tests`
Reference implementation: BiFrost `RMH.Test.Automation/Bifrost.Desktop` (`infrastructure/AppSessionManager.cs`, `AppSession.cs`, `DriverFactory.cs`, `plugins/AppLifecyclePlugin.cs`, `plugins/AppiumServerPlugin.cs`, `settings/*`)

## Scope: REQUIRED items only

Added 2026-11-18. **Agreement with the client:** port the RMH tests from BiFrost to BELLATRIX with the same behavior. No new features or fixes beyond what that requires.

* **REQUIRED** means RMH's tests could not run on BELLATRIX without the item, or would behave differently than on BiFrost.
* **Everything else is DEFERRED**, listed in the **[Deferred backlog](#deferred-backlog)** at the end. When in doubt, an item is DEFERRED.
* The sections below (TL;DR, §1–§11) stay as background. **Where they disagree with this list, this list wins.** This applies in particular to the step plans in §6 and §11.9, and to the "step 0 ships first" decision in §10.8. The async fix is deferred for this migration; it can still ship as an independent BELLATRIX patch, outside this scope.
* Two other BELLATRIX items are REQUIRED for RMH but are not multi-app design: the submodule stop files and `apiSettings.ignoreCertificateErrors`. They are listed in RMH `docs/BELLATRIX_MIGRATION_PLAN.md`, "Scope", items 2 and 3.
* **Sizes:** S is up to about a day, M a few days, L a week or more.

**Execution order:**

| # | Item (this doc) | Size | RMH tests / usages that depend on it |
|---|---|---|---|
| 1 | **`executionSettings.automationName`**: read from config, default `"NovaWindows"` (B15), replacing the literal at `WrappedWebDriverCreateService.cs:49`. This is the execution-level value only (§11.1 row 2). Both drivers accept the capability set BELLATRIX already sends (§11.3; §11.4 C1–C7), so no per-driver capability code and no profile abstraction are needed. Anton's decision ("support both drivers via config"). | S | All 103 RMH desktop test cases run on the `Windows` driver today. |
| 2 | **Maximize after start**, opt-in: `executionSettings.maximizeOnStart`, default `false`. This is the part of the §3 "Window size" row / §6 step 7 that RMH needs. | S | All RMH desktop tests. BiFrost maximizes every app after launch (`DriverFactory.cs:82-99`). |
| 3 | **Named-section read `ConfigurationService.GetSection<T>(string)`** in Core (the additive method from the header) | S | `RMH.DatabaseReset` (`DatabaseResetService.cs:14,67`), which runs in every RMH desktop fixture. |
| 4 | **`ApplicationSettings` + `DesktopSettings.ApplicationsSettings`** with `appPath`, and the resolver, without timeouts or `automationName` (§6 step 1 subset). Then **`[App(x)]` resolves `x` as a name first**, keeping the path fallback (B2), and `AppInitializationInfo.AppName` (§6 step 3 subset). | M | `[App(AppNames.X, RestartEveryTime)]` on `PosTest`, `StoreManagerTest` and `CentralManagerTest`; every `SwitchApp(AppNames.X)`. |
| 5 | **Sessions and switching** (§6 step 4 subset, design (a) in §5.1). See the list below the table. | L | 6 `App.SwitchApp` calls. Four are in `Cashier/SalesRepresentative{Central,Store}Manager…CreationTests` (switch to POS). Two are in `CentralLoginPage.Actions.cs:25` and `StoreLoginPage.Actions.cs:22` (switch **back** to the primary app, which must reuse its session). |
| 6 | **`App.CloseOrKill(name, closeThroughUi)` + `AppSession.EnsureExited`**, using `powerShell {command}` (§6 step 5, without the log-once flag and without `AppExitVerificationPlugin`). The `Windows` shape is verified from source (§11.4 P1–P3); the returned token (P4) is checked by test T12. | M | 4 calls: `StoreManagerTest.TestCleanup`, `CentralManagerTest.TestCleanup`, and `SalesRepresentativeCentralManagerCreationTests` ×2. |
| 7 | **Minimum tests** for items 1–6 (§7). See the list below the table. | M | They guard the behavior RMH depends on. The parity proof itself is the RMH baseline comparison. |

**Details for the larger rows:**

* **Item 5, sessions and switching:**
  * `AppSession` and `AppSessionService` live in the test-class container.
  * `TestExecutionEngine.StartApp` registers the primary app.
  * `App.SwitchApp(name)` activates an existing session, or starts one with `RestartEveryTime`, and re-registers `WindowsDriver`.
  * `AppLifecyclePlugin.PostTestCleanup` re-activates the primary app and disposes the secondaries (B5).
  * `DisposeDriverService` disposes every session and catches a failed `Quit()` (§3 "Disposal").
* **Item 7, minimum tests (§7):**
  * T1, the existing suite unchanged, is the backward-compatibility gate.
  * T2: name lookup.
  * T4: switch and start.
  * T7: switch back, reusing the session.
  * T10: secondaries closed after the test.
  * T12: `CloseOrKill` with a no-op UI close.

**The open branch `fix/async-container-context` (§10, step 0) is DEFERRED for this migration.**
* **Desktop:** RMH has 0 `async` tests and no `await` anywhere in its desktop test, page or facade code. The §10.4 desktop impact (a null `WindowsDriver` and `SwitchApp` registering into `Main` after an `await`) cannot happen in RMH.
* **API:** RMH's API tests reach `App` only through static extension members, so the `App` instance is never built after an `await`. They use no `App.ApiClient`, no in-test plugins and no auth attributes. Details are in the RMH plan's Scope section.
* **What would move it to REQUIRED:** RMH code awaiting before UI calls or before `SwitchApp`; RMH starting to use `App.ApiClient`, `GetApiClientService`, in-test plugins or auth attributes; or RMH needing API attachments in Allure.

## TL;DR

* **Recommended: design (a), "the active app is a container registration."** A per-test-class `AppSessionService` instance is registered in the test-class child container (`ServicesCollection.Current`). It holds every session the test opened. `App.SwitchApp(name)` re-registers the chosen session's `WindowsDriver`, its effective `TimeoutSettings` and its `IViewSnapshotProvider` in that same container. **No code that resolves `WindowsDriver` has to change**, and that code is where most of the risk is.
* No static or `ThreadLocal` session manager. BiFrost needed `ThreadLocal` only because it removed DI. In Bellatrix the child container keyed by the test class's full name already gives each class its own state, and does it more reliably than `ThreadLocal`: NUnit can run `[OneTimeSetUp]` and `[Test]` on different threads, which loses a `ThreadLocal` session.
* Config stays backward compatible. `desktopSettings.timeoutSettings` stays the default, and a new optional `desktopSettings.applicationsSettings` map holds per-app values that override it key by key. `[App(x)]` treats `x` as an app name when it is a key in `applicationsSettings`, and as a path otherwise, which is today's behavior.
* **Both Appium Windows drivers are first-class** (`Windows`/WinAppDriver and `NovaWindows`). `executionSettings.automationName` sets the default (`NovaWindows`) and `applicationsSettings.<App>.automationName` overrides it per app. Everything driver-specific goes through `IDesktopDriverProfile` (§11).
* **Step 0 (async `ServicesCollection.Current` fix, §10) ships first as its own patch release**, together with or followed by the Allure `ThreadLocal` fix (§10.8).
* There are 7 PR-sized steps. Each one alone leaves existing single-app suites unchanged. The only behavior changes are opt-in, and they are flagged in [Breaking-change register](#8-breaking-change-register).

---

## 1. Every place that resolves the driver, timeouts or app path

Line numbers are from `master` at `4e5e187d`.

### 1.1 `WindowsDriver` resolution and capture

| File:line | What | When it is bound | Multi-app impact |
|---|---|---|---|
| `src/Bellatrix.Desktop/components/Core/Component.cs:50` | `WrappedDriver = ServicesCollection.Current.Resolve<WindowsDriver>()` | **constructor** (captured) | A component stays bound to the app that was active when it was created. This is the right behavior (see §2.3). |
| `src/Bellatrix.Desktop/components/Core/Component.cs:51` | `_viewSnapshotProvider = Resolve<IViewSnapshotProvider>()` | constructor | Resolves the stale `Main` registration (see `App.cs:41`). Must follow the active app. |
| `src/Bellatrix.Desktop/components/Core/ComponentsList.cs:52, 58, 64` | `WrappedDriver = Resolve<WindowsDriver>()` in all 3 constructors | constructor | Same as `Component`. |
| `src/Bellatrix.Desktop/waitstrategies/WaitStrategy.cs:26` | `WrappedWebDriver = Resolve<WindowsDriver>()` | constructor | Wait strategies are created per call, so they use the active app. That is correct. |
| `src/Bellatrix.Desktop/validators/ValidateControlExtensions.cs:29` | `Resolve<WindowsDriver>()` | per call | Uses the active app. It should use the component's driver; see step 4. |
| `src/Bellatrix.Desktop/llm/FindByPrompt.cs:42` | `WrappedDriver => Resolve<WindowsDriver>()` | property (lazy) | Uses the active app. Correct. |
| `src/Bellatrix.Desktop/llm/FindByPrompt.cs:148` | `Resolve<IViewSnapshotProvider>()` | per call | Same stale-`Main` problem as `Component.cs:51`. |
| `src/Bellatrix.Desktop/services/DesktopService.cs:20` / `AppService.cs:25` | constructor injection of `WindowsDriver` | Unity builds it per `Resolve<AppService>()` | `App.AppService` (`App.cs:46`) resolves on every access, so it follows the active app automatically. |
| `src/Bellatrix.Desktop/infrastructure/App.cs:41` | `ServicesCollection.Main.RegisterInstance<IViewSnapshotProvider>(AppService)` | in **`App` constructor**, into **Main** | Existing bug: `App` is transient (Unity builds a new one on every `DesktopTest.App` access), so this keeps overwriting one global instance, which is unsafe across parallel classes and is bound to whatever driver was current. Fix in step 2. |
| `src/Bellatrix.Desktop/screenshots/VanillaWebDriverScreenshotEngine.cs:32` | `serviceContainer.Resolve<WindowsDriver>()` (`e.Container`) | per call, from `ScreenshotPlugin.PreTestCleanup` | Takes the screenshot of the active app. It runs before `AppLifecyclePlugin.PostTestCleanup`, so the session is still alive. |
| `src/Bellatrix.Desktop/infrastructure/TestExecutionEngine.cs:29-30` | `WrappedWebDriverCreateService.Create(...)` then `childContainer.RegisterInstance(wrappedWebDriver)` | app start | **The one place that registers the driver.** |
| `src/Bellatrix.Desktop/infrastructure/TestExecutionEngine.cs:54-55` | `DisposeAll` resolves the driver from each child | teardown | Must also dispose secondary sessions. |
| `src/Bellatrix.Desktop/services/DisposeDriverService.cs:20-24, 31-37, 48-63` | quit and unregister `WindowsDriver` in the child and in **Main** | teardown | Must quit **all** sessions of the container, not only the one that is active. |
| `src/Bellatrix.Desktop/infrastructure/mstest/DesktopTest.cs:25`, `nunit/DesktopTest.cs:25` | `FindCollection(className).Resolve<App>()` | per access | No change. |
| `src/Bellatrix.Desktop/pageobjects/DesktopPage.cs:20` | `Resolve<App>()` | per access | No change. |

`ComponentCreateService`, `ComponentsRepository`, `FindStrategyCreateExtensions.cs:25,33` and `ComponentWaitService` never touch the driver directly. They go through `Component`, `ComponentsList` and `WaitStrategy`.

### 1.2 `DesktopSettings.TimeoutSettings` reads (global today, per-app after the change)

| File:line | Key |
|---|---|
| `components/Core/ComponentsList.cs:160-161` | `ElementToExistTimeout`, `SleepInterval` |
| `validators/ValidateControlExtensions.cs:27-28` | `ValidationsTimeout`, `SleepInterval` |
| `waitstrategies/WaitStrategy.cs:28` | `SleepInterval` |
| `waitstrategies/WaitNotBeVisibleStrategy.cs:26` | `ElementNotToBeVisibleTimeout` |
| `waitstrategies/WaitNotExistStrategy.cs:27` | `ElementToNotExistTimeout` |
| `waitstrategies/WaitToBeClickable.cs:26` | `ElementToBeClickableTimeout` |
| `waitstrategies/WaitToBeVisibleStrategy.cs:26` | `ElementToBeVisibleTimeout` |
| `waitstrategies/WaitToExistStrategy.cs:27` | `ElementToExistTimeout` |
| `waitstrategies/WaitToHaveContentStrategy.cs:26` | `ElementToHaveContentTimeout` |
| `services/WrappedWebDriverCreateService.cs:44-45, 79` | `CreateSessionTimeout`, `WaitForAppLaunchTimeout`, `ImplicitWaitTimeout` |

### 1.3 App path, lifecycle and execution settings

| File:line | What |
|---|---|
| `plugins/execution/Attributes/AppAttribute.cs:25-57` | 5 constructors, all `string appPath` first |
| `settings/AppInitializationInfo.cs:47, 63-83` | `AppPath` getter normalizes the `AssemblyFolder` and `UserFolder` prefixes. `Equals`/`GetHashCode` (`:53-56, 85-88`) use `AppPath + Lifecycle + Size`. |
| `plugins/execution/AppLifecyclePlugin.cs:109` | `AppPath != "Root"` special case |
| `plugins/execution/AppLifecyclePlugin.cs:159-168` | fallback with no attribute: `executionSettings.defaultLifeCycle`, `.resolution`, `.defaultAppPath` |
| `plugins/execution/AppLifecyclePlugin.cs:222` | `executionSettings.arguments` turned into Appium capabilities |
| `services/WrappedWebDriverCreateService.cs:34` | `executionSettings.url`, in a **static constructor** |
| `services/WrappedWebDriverCreateService.cs:43, 51-65` | `appWorkingDir = Path.GetDirectoryName(AppPath)`, `app`/`appTopLevelWindow` |
| `infrastructure/App.cs:43, 63` | `executionSettings.shouldStartLocalService`, `.url`, both read once into statics |

### 1.4 `Bellatrix.Core` and `Bellatrix.Plugins.Screenshots`

* `src/Bellatrix.Core/settings/ConfigurationService.cs:35`: `GetSection<T>()` binds a fresh instance on every call, and there is no way to read a nested path. Per-app timeout merging needs one additive overload (step 1).
* `src/Bellatrix.Core/settings/SettingsExtensions.cs:25`: `NormalizeAppPath()` handles `AssemblyFolder` and `RootFolder`. Desktop does not use it; `AppInitializationInfo` has its own copy that handles `AssemblyFolder` and `UserFolder`. Reuse the desktop one for `applicationsSettings[].appPath` so both forms behave the same.
* `src/Bellatrix.Core/infrastructure/ioc/ServicesCollection.cs`: see §2.
* `src/Bellatrix.Core/infrastructure/NUnitBaseTest.cs:75` and `MSTestBaseTest.cs:237` create the child container named by the test class's full name.
* `src/Bellatrix.Plugins.Screenshots/ScreenshotPlugin.cs:44, 63, 71`: never resolves `WindowsDriver` itself. It calls `IScreenshotEngine.TakeScreenshot(e.Container)`, and the desktop engine is the one in §1.1. **No change is needed in that project.**

---

## 2. How `ServicesCollection.Current` scoping works, and whether re-registering on switch is safe

### 2.1 Mechanics

* `ServicesCollection.Main` is one process-wide root Unity container (lazy static, `ServicesCollection.cs:36-49`).
* Each test class gets a child container, `Main.CreateChildServicesCollection(testClassType.FullName)`. NUnit creates it in `[OneTimeSetUp]` and MSTest on the first `[TestInitialize]` of the class. The child inherits all `Main` registrations, and a registration in the child shadows the one in `Main`.
* **`Current` is not per thread and not ambient.** It walks the **current call stack** from the outermost frame inward (`ServicesCollection.cs:62-73`) and returns the child container of the first frame whose `DeclaringType.FullName` names a child container, that is, the test class. If no such frame exists, it returns `Main`.

The consequences:

1. Any code called synchronously from a method **declared on the test class** resolves that class's container, whatever thread it runs on. That covers test methods and overrides of `TestsArrange`, `TestInit` and the other virtual hooks. It does **not** cover the base-class lifecycle methods (`NUnitBaseTest.CoreTestInit` and the rest), or plugins raised from them. Those resolve `Main` through `Current`, and only `e.Container` (explicit `FindCollection`) is correct there. See §10.
2. Code that runs **without the test class on its stack** silently gets `Main`. That includes `Task.Run`, timers, continuations after `await`, static constructors, `[AssemblyInitialize]`/`[AssemblyCleanup]`, and event handlers raised on other threads. `Resolve<WindowsDriver>()` then returns `null`, because Unity cannot build a `WindowsDriver`, and the resolve exception is swallowed. This limitation exists today and is unrelated to multi-app, but `SwitchApp` must not be called from such contexts. Document it.
3. Unity `RegisterInstance` replaces the default registration of the same type and name in that container. All `ServicesCollection` mutations are under its own `_lockObject`.

### 2.2 Parallel runs

| Parallelism | Today | With design (a) |
|---|---|---|
| Sequential | Safe | Safe |
| **Class-level** (MSTest `[assembly: Parallelize(Scope = ClassLevel)]`, NUnit `ParallelScope.Fixtures`) | Safe: one container per class | **Safe**: the session registry, the active driver and the active timeouts all live in that class's child container. A switch in class A cannot be seen from class B. |
| Method-level inside one class (`ParallelScope.Children`) | **Already unsafe**: one `WindowsDriver` per class container, shared by concurrent tests | Still unsafe. Out of scope. Document it as unsupported, as it is today. |

The BiFrost `ThreadLocal` model is less safe than the container model. NUnit does not guarantee that `[OneTimeSetUp]`, `[SetUp]` and `[Test]` share a thread, so a session registered on one thread can be missing on another. **This is a reason not to port that structure.**

Three existing global-state hazards need fixing or noting, because multi-app makes them worse:

* `App.cs:41` registers `IViewSnapshotProvider` into **Main** from a transient `App` constructor. With two apps, or two parallel classes, the LLM locator and `Component` snapshots read whatever app last constructed an `App`. Step 2 moves it into the child container and re-registers it on switch.
* `DisposeDriverService.cs:37, 63` unregister `WindowsDriver` in **Main**. This is harmless, because nothing registers the driver there, but keep it as is.
* `AppLifecyclePlugin.cs:130-133` documents a known `ReuseIfStarted` bug across classes. Multi-app neither fixes it nor makes it worse.

### 2.3 Components created for a previous app

`Component`, `ComponentsList` and `WaitStrategy` capture `WrappedDriver` **in the constructor**. After `SwitchApp("B")`:

* A component created while A was active keeps using **A's driver**. This is the desired behavior: an element belongs to the session that found it, and an `AppiumElement` id is meaningless in another WinAppDriver or NovaWindows session. Test case T5 asserts it.
* Its **wait strategies** are created later, inside `ToExist()`, `ToBeVisible()` and similar, and resolve the **active** driver, which is now B. That is a real cross-app bug: the wait polls B for an element of A. **Fix (step 4):** construct wait strategies with the owning component's driver and timeouts when one is available (`WaitStrategy` gets an overload that takes `WindowsDriver`), and fall back to `Current` otherwise. `ValidateControlExtensions.cs:29` gets the same fix; the component is in scope there, so it uses `control.WrappedDriver`.
* If A's session has been **disposed** (`CloseOrKill("A")`), old A components throw `WebDriverException` (session not found). That is expected; T6 asserts a clear message.

Re-registering the active `WindowsDriver` on switch is therefore safe for parallel classes and correct for already-created components, provided step 4 makes waits and validations follow the component's driver rather than the active one.

---

## 3. BELLATRIX and BiFrost lifecycle compared, and the minimal change set

| Concern | BELLATRIX today | BiFrost | Port? |
|---|---|---|---|
| Where sessions live | One `WindowsDriver` registered in the class child container (`TestExecutionEngine.cs:30`) | Static `AppSessionManager` with `ThreadLocal<Dictionary<string, AppSession>>` and `ThreadLocal<string> active` | **Behavior yes, structure no.** It becomes an `AppSessionService` instance in the child container. The statics exist only because BiFrost removed DI, so drop them. |
| Driver creation | `WrappedWebDriverCreateService.Create` (static) | `DriverFactory.StartApp` → `AppSession` | Extend `Create`. Do not add a factory. |
| App settings | One global `TimeoutSettings`, app path from the attribute or `defaultAppPath` | `applicationsSettings[name]` merged over `defaultTimeoutSettings` with `GetMergedSection` | Yes, re-expressed through `ConfigurationService` (§4). |
| Command timeout | none: a stuck call waits Appium's 240s proxy timeout | `new WindowsDriver(uri, opts, TimeSpan.FromSeconds(commandTimeout))` plus a warning when unset | **Yes, a lesson.** Opt-in through `timeoutSettings.commandTimeout`; 0 or unset keeps today's behavior. |
| `newCommandTimeout` | not set | `int?`, added only when set | Yes, opt-in. |
| `appWorkingDir` | `Path.GetDirectoryName(AppPath)` | explicit `workingDirectory` | Yes, as `workingDirectory ?? GetDirectoryName(appPath)`. |
| Window size | `ChangeWindowSize` exists (`WrappedWebDriverCreateService.cs:107`) but is **never called**, so `[App(path, w, h)]` is ignored | called after start | **Flagged behavior change**, in its own PR (step 7) behind `executionSettings.applyWindowSize` (default `false`). |
| Close-button hover workaround | present | present | already ported |
| `automationName` | `NovaWindows` | `Windows` | Keep `NovaWindows`. **[Superseded by §11: both drivers are first-class, chosen by `executionSettings.automationName` with a per-app override, through `IDesktopDriverProfile`.]** |
| Lifecycle | `AppLifecyclePlugin` with named instances `_currentAppConfiguration`, `_previousAppConfiguration`, `_isAppStartedDuringPreTestsArrange`; restart/reuse decision in `ShouldRestartApp`; shutdown in `PostTestCleanup` and `PostTestsCleanup` | `AppLifecyclePlugin` with fields; `ShutdownNonActiveApps()` after every test; `SwitchTo(primary)` before cleanup | **Extend Bellatrix's plugin**: before the existing `PostTestCleanup` logic, re-activate the primary app and dispose the secondary sessions. |
| Explicit app switch | n/a | `App.SwitchApp(name)`: switch if open, otherwise start and register | Add to `Bellatrix.Desktop.App`, backed by container services. |
| Robust close | n/a | `App.CloseOrKill(name, closeThroughUi)` → `AppSession.EnsureExited` (`Wait-Process`, then `Stop-Process` through Appium's `powerShell` script) | **Yes, a lesson.** Bellatrix already starts Appium with `--allow-insecure=novawindows:power_shell` (`App.cs:136`). The "report the missing flag once" static boolean becomes a named `bool` in `ServicesCollection.Main`. |
| Appium server | `App.StartAppiumServer()` (static, assembly level, `npm`/`ps1`, NovaWindows install or update, `WaitPortToGetBusy`) | `AppiumServerPlugin` per test class, using `AppiumServiceBuilder`; fails fast if a server is already up; checks `IsRunning`; `--use-drivers`; log file | **Port the checks only**: refuse, or reuse with a clear message, when `{url}/status` already answers; throw with the log path if the port never opens. Starting per class exists in BiFrost only because it has no assembly-level hook wired through DI, so **drop it** and keep `StartAppiumServer`. |
| Disposal | `DisposeDriverService.Dispose(container)` / `DisposeAll()` | `AppSession.Dispose` catches a failed `Quit` and still marks the session disposed | Yes: catch per session, so one crashed app cannot block disposal of the others. |
| Fallback clicks | `Component.DefaultActions.cs:26-60` | `PerformWithFallbacks` (`windows: invoke`, then native click, then Actions); aggregates causes; never retries a lost session | **Separate PR, not part of multi-app.** Listed so it isn't lost. |

**Minimal change set for multi-app:** 3 new files and 9 to 10 modified files across steps 1 to 4, plus 8 timeout call sites in step 3. See §5.3.

---

## 4. Config: backward compatibility, schema and default merging

### 4.1 Rules

1. Nothing existing is renamed. `desktopSettings.timeoutSettings` stays and **is** the default. BiFrost calls it `defaultTimeoutSettings`; we do not adopt that name, because it would break every existing settings file.
2. `desktopSettings.applicationsSettings` is **optional**. If it is missing or empty, behavior is exactly today's.
3. How `[App(x)]` and `executionSettings.defaultAppPath = x` resolve:
   * If `x` matches a key of `applicationsSettings` (case-insensitive), it is a **named app**: path, working directory, default lifecycle, resolution and merged timeouts come from that entry. Explicit attribute arguments (lifecycle other than `NotSet`, size) still win.
   * Otherwise `x` is a **path** or `Root`, exactly as today. The legacy path is not looked up in `applicationsSettings` at all, so legacy suites never see per-app timeouts by accident.
   * Ambiguity is impossible for existing users: before they add `applicationsSettings`, no key can match.
4. `App.SwitchApp(name)` accepts a configured name. For the primary app it also accepts the session key that `App.ActiveAppName` reports: the name for a named app, the normalized path for a legacy one.
5. Effective timeouts for app N are a copy of `desktopSettings.timeoutSettings` with the keys present under `desktopSettings.applicationsSettings.N.timeoutSettings` bound over it. Keys that are not present keep the default. `Microsoft.Extensions.Configuration.ConfigurationBinder.Bind(instance)` already only sets the keys it finds, so no nullable duplicate class is needed.

### 4.2 Schema (`testFrameworkSettings.*.json`)

```jsonc
"desktopSettings": {
  "timeoutSettings": {                 // existing: the defaults for every app
    "implicitWaitTimeout": "0",
    "validationsTimeout": "30",
    "sleepInterval": "1",
    "elementToBeVisibleTimeout": "30",
    "elementToExistTimeout": "30",
    "elementToNotExistTimeout": "30",
    "elementToBeClickableTimeout": "30",
    "elementNotToBeVisibleTimeout": "30",
    "elementToHaveContentTimeout": "15",
    "createSessionTimeout": "10",
    "waitForAppLaunchTimeout": "10",
    "commandTimeout": "0",             // NEW, optional. Seconds; 0 or missing = no client-side command timeout (today's behavior)
    "newCommandTimeout": null          // NEW, optional. Seconds; missing = Appium default (60s); 0 = disabled
  },
  "executionSettings": {               // existing, unchanged
    "defaultLifeCycle": "restart every time",
    "shouldStartLocalService": "false",
    "resolution": "",
    "defaultAppPath": "AssemblyFolder\\Demos\\Wpf\\WPFSampleApp.exe",   // a path OR an applicationsSettings key
    "url": "http://127.0.0.1:4723",
    "arguments": [ { } ]
  },
  "applicationsSettings": {            // NEW, optional. Keyed by app name so environment files can override one app
    "WpfSample": {
      "appPath": "AssemblyFolder\\Demos\\Wpf\\WPFSampleApp.exe",       // required; same AssemblyFolder/UserFolder/Root rules
      "workingDirectory": "",          // optional; default = directory of appPath
      "defaultLifeCycle": "",          // optional; default = executionSettings.defaultLifeCycle
      "resolution": "",                // optional; default = executionSettings.resolution
      "timeoutSettings": {             // optional; only the keys that differ
        "elementToExistTimeout": "10"
      }
    },
    "WinFormsSample": {
      "appPath": "AssemblyFolder\\Demos\\WinForms\\WindowsFormsSampleApp.exe"
    }
  }
}
```

Precedence for each value, highest first:

| Value | 1st | 2nd | 3rd |
|---|---|---|---|
| Lifecycle | `[App]` argument ≠ `NotSet` | `applicationsSettings[n].defaultLifeCycle` | `executionSettings.defaultLifeCycle` |
| Size | `[App]` size constructor | `applicationsSettings[n].resolution` | `executionSettings.resolution` |
| Each timeout key | `applicationsSettings[n].timeoutSettings.<key>` | `timeoutSettings.<key>` | class default |
| Explicit wait or validation `timeoutInSeconds` argument | always wins (unchanged) | | |

### 4.3 Settings classes (additive only)

* `settings/TimeoutSettings.cs`: add `int CommandTimeout`, `int? NewCommandTimeout`, and an internal `Clone()` (a `MemberwiseClone` wrapper).
* `settings/DesktopSettings.cs`: add `Dictionary<string, ApplicationSettings> ApplicationsSettings { get; set; } = new();` Look keys up with `OrdinalIgnoreCase`, because the binder's dictionary is case-sensitive while configuration keys are not.
* `settings/ApplicationSettings.cs` (new): `Name`, `AppPath`, `WorkingDirectory`, `DefaultLifeCycle`, `Resolution`, `TimeoutSettings`. The bound `TimeoutSettings` is partial, with zeros for keys that are not set, so it is **never read directly**. Only the merged result from `ApplicationSettingsResolver` is used.
* `settings/AppInitializationInfo.cs`: add `string AppName` (`null` for legacy) and `TimeoutSettings TimeoutSettings` (effective). Keep `Equals`/`GetHashCode` on `AppPath + Lifecycle + Size`, so `ShouldRestartApp` behaves the same for legacy apps.
* `Bellatrix.Core/settings/ConfigurationService.cs`: add `public static TSection GetSection<TSection>(string sectionPath, TSection target = null)`. It binds `_root.GetSection(sectionPath)` onto `target` (or a new instance) and returns it. This is additive and leaves `GetSection<T>()` untouched.

---

## 5. Two designs compared

### 5.1 Design (a): register the active app's services in the container on switch (recommended)

New types (Bellatrix style: Automate The Planet header, StyleCop, existing namespaces):

| File | Type | Responsibility |
|---|---|---|
| `src/Bellatrix.Desktop/settings/ApplicationSettings.cs` | `ApplicationSettings` | binds `applicationsSettings[name]` |
| `src/Bellatrix.Desktop/services/ApplicationSettingsResolver.cs` | `ApplicationSettingsResolver` (instance, stateless; registered in `Main`) | `bool IsConfiguredApp(string)`, `AppInitializationInfo Resolve(string nameOrPath, Lifecycle, Size)`: merges timeouts and normalizes the path |
| `src/Bellatrix.Desktop/services/AppSession.cs` | `AppSession` | `AppName`, `WindowsDriver Driver`, `AppInitializationInfo`, `TimeoutSettings`, `bool EnsureExited(TimeSpan)`, `Dispose()` that catches a failed `Quit` |
| `src/Bellatrix.Desktop/services/AppSessionService.cs` | `AppSessionService` (one **instance per test-class child container**, registered by `TestExecutionEngine.StartApp`) | `PrimaryAppName`, `ActiveAppName`, `Sessions`, `Register`, `Activate(name)`, `DisposeSecondary()`, `DisposeAll()`. `Activate` does the three registrations below. Uses its own lock. |

What `Activate(name)` does on the owning container:

```csharp
container.RegisterInstance(session.Driver);                                  // default WindowsDriver: unchanged contract
container.RegisterInstance(session.TimeoutSettings, "_activeAppTimeoutSettings");
container.RegisterInstance<IViewSnapshotProvider>(new AppService(session.Driver));
```

> Gotcha: never `Resolve<TimeoutSettings>()` without a name. Unity will happily construct a **new, all-zero** `TimeoutSettings`. Always use the named `Resolve<T>(name)` overload, which checks `IsRegistered`, and fall back to `ConfigurationService`. Wrap this in one internal helper, `DesktopTimeouts.Current` (a static method with no state, reading `ServicesCollection.Current`), so the 8 call sites stay one-liners.

Flow:

* `TestExecutionEngine.StartApp` registers the driver exactly as today, then gets or creates the container's `AppSessionService`, registers the session as **primary** (key = `AppName ?? AppPath`) and calls `Activate`. For legacy users, the `WindowsDriver` registration is byte-for-byte what it was.
* `App.SwitchApp(name)`: if the session exists, `Activate`. Otherwise `ApplicationSettingsResolver.Resolve(name)`, then `WrappedWebDriverCreateService.Create`, then `Register` as secondary, then `Activate`. Logs `Logger.LogInformation("Switched to app '{0}'", name)`; Allure picks this up through the existing logger and step integration.
* `App.CloseOrKill(name, Action closeThroughUi)`: closes through the UI (exceptions logged as warnings), then `session.EnsureExited(App.AppExitTimeout)`, then disposes the session and removes it. If it was the active session, re-activates the primary when one is alive. Never throws.
* `App.ActiveAppName` (read-only).
* `AppLifecyclePlugin.PostTestCleanup`: **first** `sessions.Activate(primary)` and `sessions.DisposeSecondary()`, then the existing logic unchanged. Without the re-activation, `DisposeDriverService.Dispose` would quit the *secondary* driver and leak the primary.
* `DisposeDriverService.Dispose(container)` / `DisposeAll()`: also dispose every session in the container's `AppSessionService`; existing unregister calls stay.
* Optional and opt-in: `plugins/execution/AppExitVerificationPlugin.cs`, registered through `DesktopPluginsConfiguration.AddAppExitVerification()` with `ServicesCollection.Current.RegisterType<Plugin, AppExitVerificationPlugin>(Guid.NewGuid().ToString())`. On `PostTestCleanup` it calls `EnsureExited` for each app the lifecycle just shut down. It is **not** added to `DesktopTest.Configure()` by default, so existing behavior is unchanged.

### 5.2 Design (b): an `IDriverProvider` abstraction injected everywhere

`IDriverProvider { WindowsDriver Driver; TimeoutSettings Timeouts; string AppName; }` is registered per child container, and every consumer resolves `IDriverProvider` instead of `WindowsDriver`.

* Must also change `Component`, `ComponentsList`, `WaitStrategy`, `ValidateControlExtensions`, `FindByPrompt`, `VanillaWebDriverScreenshotEngine`, `AppService` and `DesktopService`. The `DesktopService(WindowsDriver)` **constructor is public and protected**, so changing it **breaks every user-derived service**.
* Third-party and user code that calls `ServicesCollection.Current.Resolve<WindowsDriver>()` still exists (templates, `Bellatrix.Desktop.Mcp.Server`, custom components). To keep it working we must **still** register the active `WindowsDriver` on switch, at which point (b) is a superset of (a).
* The one real advantage is lazy resolution at each call, but §2.3 shows components *should* stay bound to their creating session, so lazy resolution is actively wrong for them.

### 5.3 Comparison

| | (a) re-register active driver | (b) `IDriverProvider` everywhere |
|---|---|---|
| New files | 4 (`ApplicationSettings`, `ApplicationSettingsResolver`, `AppSession`, `AppSessionService`), +1 optional plugin | 5 or more (the same + `IDriverProvider` + implementation) |
| Modified files (functional) | 10: `App`, `TestExecutionEngine`, `AppLifecyclePlugin`, `WrappedWebDriverCreateService`, `DisposeDriverService`, `AppInitializationInfo`, `DesktopSettings`, `TimeoutSettings`, `ConfigurationService` (Core), `DesktopPluginsConfiguration` (optional plugin only) | the 10 of (a) + `Component`, `ComponentsList`, `WaitStrategy`, `ValidateControlExtensions`, `FindByPrompt`, `VanillaWebDriverScreenshotEngine`, `AppService`, `DesktopService` = **18** |
| Mechanical edits | 8 timeout sites → `DesktopTimeouts.Current`; `WaitStrategy` + `ValidateControlExtensions` driver fix (§2.3) | the same 8 + all driver sites |
| Public API break | **none** | `DesktopService` constructor; anything resolving `WindowsDriver` unless it is kept registered |
| Risk to single-app users | Very low: the same registration on the same container; new code only runs after `SwitchApp` | Medium: every element, wait and validation path changes |
| Parallel-safety (class level) | Safe: state in the child container | Safe: same scoping |
| Parallel-safety (methods in one class) | Unsupported, as today | Unsupported, as today |
| Stale component after switch | Correct (bound to its session) once step 4 lands | Wrong unless the provider is captured at construction, which makes it (a) |

**Recommendation: (a).** It touches roughly half the files, has no public API break, keeps DI as the single source of truth, and its parallel-safety comes from the container that already exists.

---

## 6. Implementation plan (PR-sized steps) **[Superseded 2026-11-18 by [Scope](#scope-required-items-only): only the REQUIRED subsets of steps 1, 3, 4, 5 and the maximize part of 7 are in this migration]**

Each step builds, passes the existing `tests/Bellatrix.Desktop.Tests` suite unchanged, and is shippable alone.

| # | PR | Files | Behavior change for existing users |
|---|---|---|---|
| 1 | **Settings and merge.** `ApplicationSettings`, the `DesktopSettings.ApplicationsSettings` map, `TimeoutSettings.CommandTimeout` and `NewCommandTimeout`, the `ConfigurationService.GetSection<T>(path, target)` overload, `ApplicationSettingsResolver` (registered in `Main` on first use) | 4 new or modified + Core | none (nothing reads it yet) |
| 2 | **Fix `IViewSnapshotProvider` scoping.** Stop registering it into `Main` from `App`'s constructor; register `new AppService(driver)` in the child container in `TestExecutionEngine.StartApp`. Keep the `Main` registration as a fallback for code that runs with no test class on the stack. | `App.cs`, `TestExecutionEngine.cs` | none visible (bug fix for parallel classes using LLM locators) |
| 3 | **Per-app timeouts plumbing.** `AppInitializationInfo.AppName` and `.TimeoutSettings`; the `DesktopTimeouts.Current` helper; switch the 8 sites in §1.2. `AppLifecyclePlugin.GetCurrentAppConfiguration` resolves `[App(x)]` and `defaultAppPath` through the resolver (§4.1 rule 3). `WrappedWebDriverCreateService.Create` uses the effective timeouts, `workingDirectory`, `commandTimeout` (only when > 0) and `newCommandTimeout` (only when set), and logs a warning when `commandTimeout` is 0. | ~13 | none unless `applicationsSettings` or the new timeout keys are configured. **Flag:** the new warning appears in logs. |
| 4 | **Sessions and switching.** `AppSession`, `AppSessionService`; `TestExecutionEngine.StartApp` registers the primary; `App.SwitchApp`, `App.ActiveAppName`; `AppLifecyclePlugin.PostTestCleanup` re-activates the primary and disposes secondaries; `DisposeDriverService` disposes all sessions; `WaitStrategy` and `ValidateControlExtensions` use the owning component's driver and timeouts (§2.3). | 2 new + ~6 | none unless `SwitchApp` is called |
| 5 | **CloseOrKill and EnsureExited.** `AppSession.EnsureExited` (the `powerShell` script, invoked through the driver profile. **[Superseded: the argument shape is verified from source for both drivers in §11.4 P1–P3; only the runtime rows P4/P5 remain.]**), `App.CloseOrKill`, `App.AppExitTimeout`; the "power_shell not allowed" error logged once, using a named `bool` in `Main`; optional `AppExitVerificationPlugin` + `DesktopPluginsConfiguration.AddAppExitVerification()` | 3 to 4 | none (opt-in) |
| 6 | **Appium server checks.** In `App.StartAppiumServer`: if `{url}/status` already answers, log it and reuse the server instead of starting a second one; if the port never opens, throw with a clear message. No per-class server. | `App.cs` | **Flag:** today a second server start fails or hangs in odd ways; after this PR it is reused. Clearer, but different. |
| 7 | **(Separate, optional) apply window size.** Call the dead `ChangeWindowSize` behind `executionSettings.applyWindowSize` (default `false`). | `WrappedWebDriverCreateService.cs`, `ExecutionSettings.cs` | none by default. **Flag:** turning it on maximizes or resizes windows that were not resized before. |

Not part of this work, but tracked: porting BiFrost's `PerformWithFallbacks` (click, hover, set text) into `Component.DefaultActions.cs` as its own PR.

NUnit and MSTest: every piece above lives in `Bellatrix.Desktop` services and plugins that both `infrastructure/nunit/DesktopTest.cs` and `infrastructure/mstest/DesktopTest.cs` already wire through `DesktopPluginsConfiguration.AddLifecycle()`. Neither base class needs a code change. The optional plugin is registered by the user's `Configure()` override in either framework.

---

## 7. Test cases for `tests/Bellatrix.Desktop.Tests`

The test project is MSTest and already ships the WPF and WinForms demo apps (`Constants.WpfAppPath`, `Constants.WinFormsAppPath`), which are enough for two-app scenarios. Add to `testFrameworkSettings.Debug.json`:

```json
"applicationsSettings": {
  "WpfSample":      { "appPath": "AssemblyFolder\\Demos\\Wpf\\WPFSampleApp.exe" },
  "WinFormsSample": { "appPath": "AssemblyFolder\\Demos\\WinForms\\WindowsFormsSampleApp.exe",
                      "timeoutSettings": { "elementToExistTimeout": "3" } }
}
```

New folder `tests/Bellatrix.Desktop.Tests/MultiApp/`, category `Categories.Desktop` (plus a new `MultiApp` category):

| # | Class / test | Proves |
|---|---|---|
| T1 | *existing suite, unchanged* | Legacy `[App(path)]` + global `timeoutSettings` still work (regression gate for every step) |
| T2 | `NamedAppTests.AppStarts_When_AttributeUsesConfiguredName` with `[App("WpfSample", Lifecycle.RestartEveryTime)]` | Name lookup resolves the path (§4.1 rule 3) |
| T3 | `NamedAppTests.LegacyPath_DoesNotPickUpPerAppTimeouts` with `[App(Constants.WinFormsAppPath)]`: a missing element fails after about 30s, not 3s | The legacy path is not looked up in `applicationsSettings` |
| T4 | `SwitchAppTests.SwitchApp_StartsSecondApp_And_FindsItsComponents` with `[App(Constants.WpfAppPath)]`: `App.SwitchApp("WinFormsSample")`, click a WinForms button, `App.ActiveAppName == "WinFormsSample"` | Start, register and activate; components created after the switch use the new driver |
| T5 | `SwitchAppTests.ComponentCreatedBeforeSwitch_StillTargetsOriginalApp`: create a WPF button, switch to WinForms, then click the WPF button and validate WPF state, including `ToExist()` | Constructor-captured driver + the step 4 wait fix |
| T6 | `SwitchAppTests.ComponentOfClosedApp_ThrowsClearError` | Disposed session behavior |
| T7 | `SwitchAppTests.SwitchBackToPrimary_ReusesSession`: switch to WinForms and back to the primary key; the primary's `WindowsDriver.SessionId` is unchanged | `Activate` without a restart |
| T8 | `SwitchAppTests.PerAppTimeout_IsApplied`: in WinForms, `ToExist()` on a missing element fails in under 10s (3s configured vs 30s default) | Merge plus the `DesktopTimeouts.Current` switch |
| T9 | `SwitchAppTests.UnknownApp_Throws` | A clear error that names `desktopSettings.applicationsSettings.<name>` |
| T10 | `LifecycleTests.SecondaryApp_IsClosedAfterTest` (two ordered tests: the first switches to WinForms, the second asserts no `WindowsFormsSampleApp` process) | `PostTestCleanup` disposes secondaries |
| T11 | `LifecycleTests.PrimaryReuseIfStarted_SurvivesSecondaryCleanup` with `[App("WpfSample", Lifecycle.ReuseIfStarted)]` | Re-activating the primary before cleanup does not quit the primary |
| T12 | `CloseOrKillTests.CloseOrKill_KillsApp_WhenUiCloseDoesNothing`: pass a no-op `closeThroughUi`; the process is gone and nothing is thrown | `EnsureExited` (needs `shouldStartLocalService` or a server started with `power_shell` allowed; mark `[TestCategory("RequiresPowerShellInsecure")]`) |
| T13 | `CloseOrKillTests.CloseOrKill_NeverThrows_WhenUiCloseThrows` | Failure isolation |
| T14 | `ScreenshotTests.FailedTest_WithSecondaryActive_ScreenshotsSecondary` (expected failure, then assert the screenshot file exists; manual visual check) | `ScreenshotPlugin` runs before cleanup and uses the active driver |
| T15 | `SettingsMergeTests` (**no app, no Appium**): unit tests of `ApplicationSettingsResolver` for a missing app key, partial override, case-insensitive key, `AssemblyFolder` normalization, lifecycle and resolution precedence (§4.2 table) | Config logic, fast |
| T16 | `ParallelSwitchTests` (two classes; one switches WPF→WinForms, the other WinForms→WPF; each asserts `ActiveAppName` and the window title after every step) | Class-level isolation of active app state. MSTest parallelism is assembly-wide, so run these in a small separate project `tests/Bellatrix.Desktop.Parallel.Tests` with `[assembly: Parallelize(Scope = ExecutionScope.ClassLevel, Workers = 2)]`. Adding it to the main project would parallelize the whole suite. |

NUnit coverage: there is no NUnit desktop test project. Add a smoke copy of T4 and T10 to `templates/Bellatrix.Desktop.GettingStarted` (or a minimal NUnit test project) so the NUnit `DesktopTest` path is exercised once.

---

## 8. Breaking-change register

| # | Change | Breaking? | Mitigation |
|---|---|---|---|
| B1 | New optional `applicationsSettings`, `commandTimeout`, `newCommandTimeout` keys | No | Defaults reproduce today's behavior |
| B2 | `[App(x)]` resolves `x` as a name when it is a configured key | No for existing configs (no keys exist) | Documented precedence; names win over paths |
| B3 | `IViewSnapshotProvider` moves from `Main` to the child container (step 2) | No for test code; code that resolves it **from `Main` outside a test** still gets the fallback | Keep the `Main` fallback registration |
| B4 | Warning logged when `commandTimeout` is 0 (step 3) | Log noise only | Could be downgraded to `LogInformation` if unwanted |
| B5 | `PostTestCleanup` disposes secondary sessions | No (only after `SwitchApp`) | n/a |
| B6 | `StartAppiumServer` reuses an already-running server (step 6) | **Behavior change** | Logged; documented in release notes |
| B7 | Window size applied after start (step 7) | **Behavior change if enabled** | Off by default (`applyWindowSize: false`) |
| B8 | `DesktopService` constructor | Unchanged in (a). It **would break** in (b), one more reason for (a). | n/a |
| B9 | No new `static`/`ThreadLocal` state holders | n/a | The existing statics (`App._appiumServerProcess`, `WrappedWebDriverCreateService.ServiceUrl`) stay as they are |

## 9. BiFrost choices that exist only because DI was removed, and are dropped

* The static `AppSessionManager` with `ThreadLocal` sessions and active name: replaced by an `AppSessionService` instance in the test-class container.
* The static `AppSessionManager.AppiumLocalService` and per-class `AppiumServerPlugin`: Bellatrix keeps the assembly-level `App.StartAppiumServer` and ports only its checks.
* `App.Components => new ComponentCreateService()` and similar newed-up facade services: Bellatrix keeps container resolution.
* The static `_reportedPowerShellDisabled` flag on `App`: becomes a named instance in `ServicesCollection.Main`.
* `App.Create<TPage>() => new TPage()`: Bellatrix keeps `ServicesCollection.Current.Resolve<TPage>()`.

---

## 10. Async context

Status: verified. The code was read, and the matching algorithm was run in a throwaway console app against .NET 10 in both Release and Debug (§10.3). Unity 5.11.10 parent and child behavior was checked the same way. No repository code was changed.

### 10.1 How `ServicesCollection.Current` picks a container

`src/Bellatrix.Core/infrastructure/ioc/ServicesCollection.cs:51-77`:

```csharp
var stackTrace = new StackTrace();                                   // :62  synchronous stack of the CURRENT thread only
foreach (var currentFrame in stackTrace.GetFrames().Reverse())       // :63  outermost frame (thread start) first
{
    if (currentFrame.GetMethod()?.DeclaringType != null)             // :65
    {
        var methodType = currentFrame.GetMethod().DeclaringType;     // :67  type that DECLARES the method
        if (_serviceProvider.IsPresentServicesCollection(methodType.FullName))   // :68  exact, case-sensitive key match
            return _serviceProvider.FindCollection(methodType.FullName);         // :70
    }
}
return _serviceProvider;                                             // :75  no match => Main
```

* Child containers are keyed by `testClassType.FullName`: `NUnitBaseTest.cs:75`, `MSTestBaseTest.cs:237`, `BenchmarkTest.cs:125`.
* A frame matches only when the **declaring** type of its method has exactly that full name. Nested types never match: an async state machine's `FullName` is `Ns.MyTests+<TestAsync>d__0`, and a lambda's closure is `Ns.MyTests+<>c__DisplayClass…`. Methods inherited from a base class do not match either, because `NUnitBaseTest.CoreTestInit` has `DeclaringType == NUnitBaseTest`.
* The walk sees only the **synchronous** stack of the current thread. There is no ambient, thread or async-local state, so whatever was true for the caller is lost once an `await` resumes on another stack.
* There is one wrong-container case that is not `Main`: if concrete test class `A` derives from concrete test class `B`, methods declared on `B` match `B`'s container while `A` runs. That is outside the scope of async, but worth knowing.
* Wherever no match is found, `Main` is returned **silently**. `Resolve<T>` then either auto-constructs a fresh, unregistered instance for a concrete class, or returns `null` for an interface, because the swallowed `ResolutionFailedException` becomes `default` (`ServicesCollection.cs:113-131`). Verified with Unity 5.11.10: `Main.Resolve<ApiClientService-like>()` returns a new object, and `Main.Resolve<IAuthenticator-like>()` throws, which the wrapper swallows.

### 10.2 What `Current` returns in each case

| # | Case | Frames on the stack | `Current` returns | Correct? |
|---|---|---|---|---|
| 1 | Inside an async `[Test]`, **before the first `await`** that actually suspends | `…InvokeMethod → MyTests.TestAsync` (the kickoff stub, declaring type `MyTests`) `→ AsyncTaskMethodBuilder.Start → <TestAsync>d__0.MoveNext` | **the test class's child** (it matches the stub frame) | ✅ |
| 1b | Same, after an `await` of an **already-completed** task (`Task.CompletedTask`, a cached result) | unchanged; no suspension | child | ✅ |
| 2 | **After an `await` that completes asynchronously** (`Task.Delay`, real I/O, any `ConfigureAwait(false)` continuation) | `ThreadPool… → AsyncStateMachineBox.MoveNext → ExecutionContext.RunInternal → <TestAsync>d__0.MoveNext`. The stub frame is gone; it returned the `Task` long ago. | **`Main`** | ❌ |
| 3a | Async helper in another class (`HttpRepository.GetAsync`) called **while the test is still in its first synchronous segment**, before the helper's own first suspending `await` | the test stub is still below the helper | child | ✅, by luck |
| 3b | Same helper **after its own first `await`**, or called **after the test has already awaited** | no frame declared by the test class | **`Main`** | ❌ |
| 4a | `NUnitBaseTest`'s own `[OneTimeSetUp]`, `[SetUp]`, `[TearDown]` and `[OneTimeTearDown]` (sync, declared on the base); same for `MSTestBaseTest` | `NUnit… → NUnitBaseTest.OneTimeArrangeAct` / `CoreTestInit` / … | **`Main`** for any direct `Current` use, e.g. `InitializeTestExecutionBehaviorObservers` (`NUnitBaseTest.cs:291`, `MSTestBaseTest.cs:223`) and `MSTestBaseTest.cs:70` `Current.RegisterInstance(TestContext)` | ⚠️ Works today only because plugins are registered in `Main` (by `Configure()`, which runs in the constructor before the child exists). `PluginEventArgs.Container` is correct because it uses `FindCollection(testClassType.FullName)` (`PluginEventArgs.cs:93, 111`). **`MSTestBaseTest.cs:70` puts every class's `TestContext` into `Main`**, and `MSTestResultsWorkflowPlugin.cs:41` reads it back from there, which is wrong under class-level parallelism. |
| 4b | Overrides of `TestsArrange`, `TestInit` and similar declared on the test class (called from 4a) | the override frame is declared by the test class | child | ✅ |
| 4c | The user's own **async** `[OneTimeSetUp]` or `[SetUp]` on the test class | as in cases 1 and 2 | child until the first suspending `await`, **`Main`** after it | ❌ after the await |
| 4d | Same on an **intermediate base class** (e.g. a project `ApiTestBase : APITest`) | declaring type is the base, which is not a container key | **`Main`** even before the await | ❌ |

NUnit's `AsyncToSyncAdapter` blocks on the returned task. Whether or not it installs a single-threaded `SynchronizationContext` (it does so only for STA tests), the continuation runs from a pump or a thread-pool frame, never from the test stub, so cases 2, 3b and 4c hold in both NUnit and MSTest. MSTest installs no `SynchronizationContext`.

`APITest.App` itself resolves correctly after an await, because it does not depend on the stack walk: `ServicesCollection.Current.FindCollection(TestContext.Test.ClassName)` (`src/Bellatrix.Api/infrastructure/nunit/APITest.cs:23`; MSTest `:24` uses the instance `TestContext`). `Main.FindCollection(name)` returns the right child, and NUnit's `TestContext` is async-flowed. **But Unity then builds `App`, and `App`'s constructor calls `ServicesCollection.Current` again, which is `Main` in case 2.**

### 10.3 Experiment (throwaway, scratchpad only)

A console app copied the `Current` algorithm, ran a setup method declared on a base class and an async test through reflection, and blocked on the task. That mimics both frameworks: MTA, no synchronization context. The output was the same in Release and Debug:

```
base CoreSetUp (declared on base): Current=Main
1. test before await: Demo.MyTests
   frames: <TestAsync>d__0.MoveNext <- AsyncMethodBuilderCore.Start <- AsyncTaskMethodBuilder.Start <- MyTests.TestAsync <- ...
3a. helper called before test's first await: helper-before-await=Demo.MyTests, helper-after-await=Main
2. test after await: Main (thread 5); ambient=AMBIENT:Demo.MyTests
   frames: <TestAsync>d__0.MoveNext <- AsyncStateMachineBox`1.ExecutionContextCallback <- ExecutionContext.RunInternal <- ...
3b. helper called after test awaited: helper-before-await=Main, helper-after-await=Main
```

The `ambient=` value in line 2 shows that an `AsyncLocal` set **synchronously in the setup method** survives into the test's thread-pool continuation. That is the basis of the fix in §10.5.

Unity 5.11.10 check: after `Main.RegisterInstance(x)`, a child returns `IsRegistered<T>() == true` and `Resolve<T>()` returns the same `x`. **A registration that leaks into `Main` is therefore visible to every test class that has not registered its own.**

### 10.4 Bellatrix.API: every `Current` Resolve or Register that runs in case 2 or 3

Reached when test code, or a helper called by it, touches `App` or creates an `ApiClientService` after a suspending `await`:

| File:line | Call | Effect in case 2/3 (`Current == Main`) |
|---|---|---|
| `src/Bellatrix.Api/App.cs:30` (constructor) → `GetApiClientService()` | runs on every `App` resolve: `APITest.App` is resolved per access and `App` is transient | everything below |
| `App.cs:78` | `Current.UnregisterSingleInstance<ApiClientService>()` (when `ShouldReuseRestClient == false`) | clears **`Main`**, not the class |
| `App.cs:81` | `Current.IsRegistered<ApiClientService>()` | asks **`Main`**: `false` the first time, then `true` for everyone |
| `App.cs:82` | `Current.Resolve<ApiClientService>()` | the first time, Unity **auto-builds** an unregistered client from `Main`. Later, it returns the **shared `Main` client**. |
| `App.cs:110` | `Current.RegisterInstance(client)` | **registers the client in `Main`**. From then on, every class without its own child registration (§10.3) receives this one client, **with the cookies, retry settings and authenticator of the class that created it**. |
| `ApiClientService.cs:335` (constructor → `InitializeExecutionExtensions`) | `Current.ResolveAll<ApiClientExecutionPlugin>()` | `Main`. This is harmless today, because `APIPluginsConfiguration` (`:27`) registers into `Main` from `Configure()`. Plugins added in the test with `App.AddApiClientExecutionPlugin` (`App.cs:51`, child) are **missed**. |
| `ApiClientService.cs:365` (constructor → `InitializeNewRestClient`) | `Current.Resolve<IAuthenticator>()` | **`null`**. `ApiAuthenticationWorkflowPlugin.cs:28` registered the authenticator in the **child** (`e.Container`), so **a client first created after an await sends requests without the class's `[JwtAuthenticationStrategy]` or `[HttpBasicAuthenticationStrategy]` credentials.** |
| `App.cs:46` | `Current.Resolve<DynamicTestCasesService>()` | an auto-built instance from `Main`, not the class's |
| `App.cs:51`, `:57` | `Current.RegisterType<ApiClientExecutionPlugin/Plugin, T>(guid)` when called after an await | registered in **`Main`**, so it applies to **all** classes |
| `assertions/BugReportingAssertExtensions.cs:20`, `assertions/DynamicTestCasesAssertExtensions.cs:20` | `Current.Resolve<…Service>()` inside assertion event handlers raised after an await | an auto-built instance from `Main`, so the per-class bug-reporting and dynamic-test-case context is lost |

Not affected: the request pipeline itself. `ExecuteMeasuredRequestAsync` (`ApiClientService.cs:196-270`) never calls `Current`, so an existing client keeps working after `ConfigureAwait(false)`. Also unaffected: `RetryFailedRequestsWorkflowPlugin.cs:30` and `ApiAuthenticationWorkflowPlugin.cs:28`, which use `e.Container`. Still, the retry settings that `RetryFailedRequestsWorkflowPlugin` writes to the **child's** client never reach a client created in `Main` after an await.

Why the existing suite passes: `ApiClientServiceAsyncRequestTests.TestInit()` (`tests/Bellatrix.API.Tests/Tests/ApiClientServiceAsyncRequestTests.cs:35-38`) takes the client **synchronously, in an override declared on the test class** (case 4b), before any await. The bug appears as soon as a test or a repository helper calls `App.GetApiClientService()` or `App.ApiClient` after an `await`.

Desktop impact, for the multi-app work: every `Resolve<WindowsDriver>()` in §1.1 returns `null` in cases 2 and 3, and `App.SwitchApp` would register into `Main`. **This must be fixed before, or together with, step 4 of §6.**

### 10.5 Minimal Core fix: an ambient pointer as a fallback, keeping the DI model

The idea: the base test classes already know the active child container. Publish it in an `AsyncLocal<ServicesCollection>`, which flows across `await`, `Task.Run` and `new Thread` through `ExecutionContext`, and use it **only when the stack walk finds nothing**. No state moves out of the containers; the `AsyncLocal` holds only a *pointer* to the per-class container.

```csharp
// ServicesCollection.cs
private static readonly AsyncLocal<ServicesCollection> _ambientTestClassCollection = new();

// Called only by the base test classes. Public so that third-party bases (e.g. BenchmarkTest, custom runners) can use it too.
public static void SetAmbientTestClassCollection(ServicesCollection collection) => _ambientTestClassCollection.Value = collection;

public static ServicesCollection Current
{
    get
    {
        // ... existing lazy Main init and stack walk, unchanged ...
        return _ambientTestClassCollection.Value ?? _serviceProvider;   // was: return _serviceProvider;
    }
}
```

Using the ambient value as a **fallback**, not as the first choice, makes the change strictly additive: every call that resolves a child today resolves the same child, and only calls that return `Main` today change. Making ambient-first a later performance optimization (it skips the `StackTrace`) is possible, but that would change case 4d and inherited-class semantics, so it is not part of this fix.

Set and clear points. An `AsyncLocal` set in a **synchronous** method stays in the thread's execution context, so it must be cleared, or a later class on the same worker thread would inherit a stale pointer.

| File | Where | Action |
|---|---|---|
| `src/Bellatrix.Core/infrastructure/ioc/ServicesCollection.cs` | `:75` + new field and setter | fallback as above |
| `src/Bellatrix.Core/infrastructure/NUnitBaseTest.cs` | constructor `:37`, **before** `Configure()` | `Set(null)`. `Configure()` must keep registering plugins in `Main`, so it must not inherit a stale pointer. |
| | `OneTimeArrangeAct` `:75`, after `CreateChildServicesCollection` | `Set(_container)`; `Set(null)` in a `finally` |
| | `CoreTestInit` `:128` | `Set(_container)`. **Never clear it here**, because it must flow into the test method. |
| | `CoreTestCleanup` `:154` | `Set(_container)` at the start; `Set(null)` in a `finally` at the end |
| | `ClassCleanup` `:96` | `Set(FindCollection(...))` at the start; `Set(null)` in a `finally` |
| `src/Bellatrix.Core/infrastructure/MSTestBaseTest.cs` | `CoreTestInit` `:68`, after `ExecuteActArrangePhases()` (which creates the child on first use, `:237`) | `Set(_container)`; never clear here |
| | `CoreTestCleanup` `:99` | `Set(_container)` at the start; `Set(null)` in a `finally` |
| `src/Bellatrix.Core/benchmark/BenchmarkTest.cs` | after `:125` | optional, same pattern |

Three files are required. No `Bellatrix.Api`, `Bellatrix.Desktop` or plugin code changes: `App.cs:81-110` and `ApiClientService.cs:335, 365` start resolving the child automatically.

Behavior changes to flag, per the §8 register:

* **B10:** `MSTestBaseTest.cs:70` still runs before the pointer is set, so `TestContext` keeps going to `Main`. That is no change, but it remains a parallel-safety bug. The separate one-line fix is to register it on `_container` after `ExecuteActArrangePhases()`.
* **B11:** inside `NUnitBaseTest` and `MSTestBaseTest` lifecycle methods, `InitializeTestExecutionBehaviorObservers` (`ResolveAll<Plugin>()`) now resolves from the **child**. The child sees all `Main` plugins **plus** plugins added per class with `App.AddPlugin<T>()` (`Bellatrix.Api/App.cs:57`, `Bellatrix.Desktop/infrastructure/App.cs:172`). Today those per-class plugins are silently never subscribed. **This is a behavior change: user plugins that never ran will start running.** It is arguably a fix, but it belongs in the release notes.
* Code that runs with **no** test on the logical call chain (`[AssemblyInitialize]`, `[AssemblyCleanup]`, static constructors, `ThreadPool.UnsafeQueueUserWorkItem`) still gets `Main`, as today.

What to verify in the PR: that NUnit 4.4 and MSTest 4.0.2 flow the execution context from `[SetUp]`/`[TestInitialize]` into the test method. The experiment shows that the `AsyncLocal` semantics allow it, and test A1 below fails if either runner restores the context in between. Fallback if MSTest does not flow it: set the pointer from the test-class constructor as well (MSTest creates one instance per test, after the child exists for every test but the first). For NUnit, `TestContext.CurrentContext.Test.ClassName` → `Main.FindCollection(...)` is a second fallback inside `Current`, since NUnit flows its own context.

Rejected alternatives:

* Making `Current` ambient-only: this breaks case 4d and changes the inherited-class semantics.
* Changing every `ServicesCollection.Current` call in `Bellatrix.Api`/`Bellatrix.Desktop` to `FindCollection(TestContext...)`: about 60 call sites, framework-specific, and the same bug in every new module.
* Teaching the stack walk to match nested `+<...>d__N` types: it still fails in case 3b, because a helper class's state machine is not nested in the test class.

### 10.6 Tests in `tests/Bellatrix.API.Tests` that fail today and pass after the fix

The project is MSTest 4.0.2, and none of the tests below make HTTP calls. Building an `ApiClientService` does not connect, so they don't need the demo API.

New files:

* `tests/Bellatrix.API.Tests/Tests/AsyncContext/AsyncContextTests.cs`
* `tests/Bellatrix.API.Tests/Tests/AsyncContext/ParallelAsyncContextTestsA.cs` and `...B.cs`
* `tests/Bellatrix.API.Tests/Helpers/AsyncContextProbe.cs` (an async helper in another class, for case 3)
* `tests/Bellatrix.API.Tests/Helpers/AsyncContextRendezvous.cs`
* `tests/Bellatrix.API.Tests/asynccontext.runsettings`

**A1: deterministic, cases 2 and 3 (no parallelism needed)**

```csharp
[TestClass]
[TestCategory("AsyncContext")]
public class AsyncContextTests : MSTest.APITest
{
    [TestMethod]
    public async Task CurrentIsTestClassContainer_When_AfterAsynchronousAwait()
    {
        var expected = ServicesCollection.Main.FindCollection(GetType().FullName);
        Assert.AreSame(expected, ServicesCollection.Current);          // passes today (case 1)

        await Task.Delay(50).ConfigureAwait(false);                    // always suspends

        Assert.AreSame(expected, ServicesCollection.Current);          // FAILS today: Main
    }

    [TestMethod]
    public async Task HelperSeesTestClassContainer_When_CalledAfterAwait()           // case 3b
    {
        await Task.Delay(50).ConfigureAwait(false);
        var seen = await new AsyncContextProbe().CurrentAfterAwaitAsync();          // Task.Delay inside, then returns ServicesCollection.Current
        Assert.AreSame(ServicesCollection.Main.FindCollection(GetType().FullName), seen);   // FAILS today
    }

    [TestMethod]
    public async Task ApiClientIsNotRegisteredInMain_When_ResolvedAfterAwait()
    {
        await Task.Delay(50).ConfigureAwait(false);
        var client = App.GetApiClientService();
        Assert.AreSame(client, ServicesCollection.Main.FindCollection(GetType().FullName).Resolve<ApiClientService>());
        Assert.IsFalse(ServicesCollection.Main.IsRegistered<ApiClientService>(),    // root-only check; FAILS today (App.cs:110 registered into Main)
            "ApiClientService leaked into ServicesCollection.Main");
    }
}
```

Note: Unity's `IsRegistered` on the **root** container checks only the root, so the last assertion is a clean signal. Once the bug has fired anywhere in the run, it stays `true`, which is acceptable for a regression test.

**A2: the requested pair of parallel classes, case 2 under concurrency, plus authentication isolation**

```csharp
[TestClass, TestCategory("AsyncContextParallel")]
[HttpBasicAuthenticationStrategy("userA", "pwdA")]
public class ParallelAsyncContextTestsA : MSTest.APITest
{
    [TestMethod]
    public Task EachClassGetsOwnClientAndAuthenticator_When_ResolvedAfterAwait()
        => AsyncContextRendezvous.RunAsync(GetType().FullName, () => App.GetApiClientService());
}
// ParallelAsyncContextTestsB: identical, [HttpBasicAuthenticationStrategy("userB", "pwdB")]
```

`AsyncContextRendezvous.RunAsync` (a test helper):

1. `await Task.Delay(100).ConfigureAwait(false);` makes sure the client is created on a thread-pool continuation.
2. `var client = create();` then publish `(className, client)` to a static `ConcurrentDictionary`, and signal this class's `TaskCompletionSource`.
3. Wait for the **other** class's signal with a 30s timeout. On timeout, `Assert.Inconclusive("classes did not run in parallel; run with asynccontext.runsettings")`. That way a run without parallelism never shows a false pass or a false failure.
4. Assertions, all of which fail today:
   * `Assert.AreNotSame(otherClient, client)`: today both receive the single `Main`-registered client.
   * `Assert.AreSame(ServicesCollection.Main.FindCollection(className).Resolve<IAuthenticator>(), client.WrappedClient.Options.Authenticator)`: today it is `null`, because `ApiClientService.cs:365` resolved from `Main`.
   * `Assert.AreSame(ServicesCollection.Main.FindCollection(className), ServicesCollection.Current)`.

Parallelism without changing the rest of the suite: MSTest parallelization is assembly-wide, so **do not** add `[assembly: Parallelize]`, which would run the existing HTTP tests concurrently against one demo API. Add `asynccontext.runsettings` instead:

```xml
<RunSettings>
  <MSTest>
    <Parallelize><Workers>2</Workers><Scope>ClassLevel</Scope></Parallelize>
  </MSTest>
</RunSettings>
```

Run it with `dotnet test tests/Bellatrix.API.Tests --settings tests/Bellatrix.API.Tests/asynccontext.runsettings --filter TestCategory=AsyncContextParallel`. In the normal sequential run, A2 reports *Inconclusive*. A1 does not need parallelism and runs in the normal CI pass.

Expected results:

| Test | Today | After the §10.5 fix |
|---|---|---|
| A1 `CurrentIsTestClassContainer_When_AfterAsynchronousAwait` | ❌ `Main` | ✅ |
| A1 `HelperSeesTestClassContainer_When_CalledAfterAwait` | ❌ `Main` | ✅ |
| A1 `ApiClientIsNotRegisteredInMain_When_ResolvedAfterAwait` | ❌ registered in `Main` | ✅ |
| A2 (with runsettings) | ❌ shared client, `null` authenticator | ✅ |
| Existing `Bellatrix.API.Tests` and `Bellatrix.Desktop.Tests` suites | ✅ | ✅ (the change is additive; watch for B11) |

NUnit coverage gap: `tests/` has no NUnit API or Desktop project, but `NUnitBaseTest` gets its own set and clear points. Mirror A1 in a minimal NUnit project, or in an NUnit template project, so the NUnit path is proven too, particularly the `[OneTimeSetUp]` → `[SetUp]` → `[Test]` context flow.

### 10.7 Order relative to the multi-app plan

Ship §10.5 and the A1 tests as **step 0**, before step 1 of §6. Multi-app `App.SwitchApp` re-registers into `Current`, and in any async test that would silently be `Main`, which would hand one class's active app to every class.

### 10.8 Decisions (Anton, 2026-11-17) **[2026-11-18: deferred for the RMH migration; see Scope and Deferred backlog M-1/M-2]**

* **Step 0 ships first, as its own patch release.** It fixes the credentials leak in §10.4 for current Bellatrix.API users, independently of multi-app. Contents: the §10.5 fallback in `ServicesCollection.Current`, the set and clear points in `NUnitBaseTest`/`MSTestBaseTest`, and tests A1 (and A2 behind its runsettings). Release notes: B11 (per-class `App.AddPlugin<T>()` plugins start running) and B10 (still open).
* **Allure static `ThreadLocal` state (migration plan E7) is fixed with the same mechanism, in the same PR or the next one.** `AllureWorkflowPlugin.cs:31-34` keeps `_allureLifecycle`, `_testContainerId`, `_testResultId` and `_hasStarted` in static `ThreadLocal` fields. They are lost when an `await` resumes on another thread, so screenshot, video and API attachments added after an await go to the wrong test or throw. Fix:
  * Move the per-test values into one `AllureTestState` object.
  * Register it in the test-class child container as a named instance at `PreTestInit`, and read it through `e.Container` in lifecycle events. In the attachment callbacks (`OnScreenshotGenerated`, `OnVideoGenerated`, A3), read it through `ServicesCollection.Current`, which resolves the right container after an await thanks to step 0.
  * `AllureLifecycle.Instance` is already a process-wide singleton, so keep calling it directly.
  * This **removes** four static `ThreadLocal` fields and adds no new static state, so it complies with porting rule 1 without an exception.
  * Test: an async MSTest test awaits `Task.Delay(…).ConfigureAwait(false)`, then adds an attachment through the plugin's public path; assert the attachment is in that test's `*-result.json`. It fails today.
  * Limit: parallel test **methods** inside one class are still unsupported, the same limit as §2.2.

---

## 11. Driver support

Status: design. Decision by Anton, 2026-11-17: **BELLATRIX officially supports both Appium Windows drivers, `Windows` (appium-windows-driver, backed by WinAppDriver) and `NovaWindows`, as first-class options chosen in config.** No code was changed. The feature matrix (§11.4) was built from the driver sources installed on this machine.

Superseded by this section:

* §3, the "`automationName`: keep `NovaWindows`" row.
* §6 step 5, "verify the argument shape against NovaWindows". The shape is now verified from source for both drivers (§11.4, rows P1 and P2), and invocation moves into the profile.

### 11.1 Config and precedence

```jsonc
"desktopSettings": {
  "executionSettings": {
    "automationName": "NovaWindows",       // NEW, optional. Default when missing = "NovaWindows" (today's hard-coded value)
    "url": "http://127.0.0.1:4723"          // the base path, if any, is part of the url (e.g. ".../wd/hub")
  },
  "applicationsSettings": {
    "POS": { "appPath": "...", "automationName": "Windows" }   // NEW, optional per-app override
  }
}
```

The effective `automationName` for a session, highest first:

| # | Source | Applies to |
|---|---|---|
| 1 | `applicationsSettings.<App>.automationName` | named apps only (`[App("POS")]`, `App.SwitchApp("POS")`, `defaultAppPath` that is a key) |
| 2 | `executionSettings.automationName` | every app, including legacy `[App(path)]` |
| 3 | `"NovaWindows"` | the built-in default, equal to today's `WrappedWebDriverCreateService.cs:49` |

* Names are matched case-insensitively against the registered profiles (§11.2).
* An **unknown name is a configuration error**. It fails at session start with the list of registered profile names. This is the only place a driver choice throws; missing *features* never throw (§11.6).
* The effective value is stored on `AppInitializationInfo.AutomationName` and included in `Equals`/`GetHashCode`, so changing it between tests restarts the app. Legacy users have one value, so nothing changes for them.
* There is **no separate `appiumBasePath` key**. The server's base path is taken from `executionSettings.url` (`new Uri(url).AbsolutePath`), as BiFrost's `AppiumServerPlugin` already does. This supersedes the `AppiumBasePath` key in the migration plan's P8 (revised).
* Per-test override from CI: `ConfigurationService` already calls `AddEnvironmentVariables()` (`ConfigurationService.cs`, `InitializeConfiguration`), so `desktopSettings__executionSettings__automationName=Windows` overrides the JSON. The dual-driver test runs in §11.8 rely on this.

### 11.2 `IDesktopDriverProfile`

There is one profile per Appium driver. Profiles are registered **by name in `ServicesCollection.Main`**: they are stateless and global, so per porting rule 1 they belong in `Main`. A profile is resolved per session, and the active one is registered next to the active `WindowsDriver` in the test-class container. **No other code compares `automationName` strings.**

```csharp
namespace Bellatrix.Desktop.Services;   // src/Bellatrix.Desktop/services/drivers/

public interface IDesktopDriverProfile
{
    string AutomationName { get; }                     // "Windows" | "NovaWindows"; also the registration name
    string AppiumDriverName { get; }                   // "windows" | "novawindows": --use-drivers and the --allow-insecure prefix
    IReadOnlyCollection<string> InsecureFeatures { get; } // e.g. { "power_shell" }; empty = none needed

    // Server preparation (used by the Appium server plugin / App.StartAppiumServer)
    AppiumDriverInstall Install { get; }               // npm source, update-on-every-run flag, minimum Appium version, pre-checks

    // Session
    void ApplyCapabilities(IDictionary<string, object> appiumOptions, AppInitializationInfo app, TimeoutSettings timeouts);

    // Features used by components and AppSession
    string GetScript(DesktopDriverFeature feature);    // null => not supported by this driver
    object[] BuildScriptArgs(DesktopDriverFeature feature, params object[] args); // driver-specific argument shape
}

public enum DesktopDriverFeature
{
    Invoke, Hover, SetValue, Toggle, Select, Expand, Collapse, SelectedItem,
    AddToSelection, RemoveFromSelection, ScrollIntoView, PowerShell,
}
```

| Piece | Location | Notes |
|---|---|---|
| `WindowsDriverProfile`, `NovaWindowsDriverProfile` | `src/Bellatrix.Desktop/services/drivers/` | Values come straight from the §11.4 matrix. |
| `DesktopDriverProfileResolver` | same folder, instance registered in `Main` | `Resolve(AppInitializationInfo)` applies the §11.1 precedence and does the case-insensitive name lookup. |
| `DesktopDriverFeatureNotices` | same folder, instance registered in `Main` | Remembers which "feature not supported" messages were already logged, so each one is logged once (§11.6). This is an instance in the container, not a static. |
| Registration | `DesktopPluginsConfiguration.AddDriverProfiles()`, **called from the existing `AddLifecycle()`** | Existing NUnit and MSTest `DesktopTest.Configure()` overrides get the profiles without code changes. Users add their own with `DesktopPluginsConfiguration.AddDriverProfile<TProfile>(name)`. |
| Active profile | `AppSessionService.Activate` (§5.1) adds `container.RegisterInstance(session.Profile, "_activeDriverProfile")`. Legacy single-app: `TestExecutionEngine.StartApp`. | Components capture it in the constructor, like `WrappedDriver` (§2.3), so a component keeps its own session's profile after `SwitchApp`. |

How the profile replaces the current `automationName`-specific code:

| Today | With profiles |
|---|---|
| `WrappedWebDriverCreateService.cs:44-49` writes `createSessionTimeout`, `ms:waitForAppLaunch`, `ms:experimental-webdriver` and `"automationName": "NovaWindows"` | `profile.ApplyCapabilities(...)` |
| `WrappedWebDriverCreateService.cs:90` close-button `windows: hover` | `profile.GetScript(Hover)`; skipped and logged once if null |
| `App.StartAppiumServer` (`App.cs:91-137`): Appium ≥ 3 check, NovaWindows install/update, `--allow-insecure=novawindows:power_shell` | loops over the profiles in use (§11.5): `profile.Install`, `AppiumDriverName`, `InsecureFeatures` |
| `Component.DefaultActions.cs:33, 72, 104` (`invoke`, `hover`, `setValue`) | `GetScript(Invoke/Hover/SetValue)`; null means go straight to the existing fallback |
| `CheckBox.cs:37, 54` (`toggle`), `ComboBox.cs:45, 68-71` (`select`, `expand`, `selectedItem`, `collapse`), `ListItem.cs:37-47` (`select`, `addToSelection`, `removeFromSelection`), `Component.cs:193` (`scrollToVisible`) | `GetScript(...)` plus the degrade paths in §11.6 |
| `AppSession.EnsureExited` (planned, §5.1) runs `powerShell` with `{ command }` | `GetScript(PowerShell)` + `BuildScriptArgs(PowerShell, command)`; null means degrade (§11.6) |

### 11.3 What BiFrost and BELLATRIX send to the driver today

| Script / capability | BiFrost | BELLATRIX |
|---|---|---|
| `windows: invoke` | `Component.DefaultActions.cs:21` (first strategy) | `Component.DefaultActions.cs:33` |
| `windows: hover` | `Component.DefaultActions.cs:36`, `DriverFactory.cs:86` | `Component.DefaultActions.cs:72`, `WrappedWebDriverCreateService.cs:90` |
| `windows: setValue` | `Component.DefaultActions.cs:64` | `Component.DefaultActions.cs:104` |
| `windows: toggle` | `CheckBox.cs:24, 41` (click on failure) | `CheckBox.cs:37, 54` |
| `windows: select` | `ComboBox.cs:30` (`SendKeys` on failure), `ListItem.cs:24` (no fallback) | `ComboBox.cs:45`, `ListItem.cs:37` |
| `windows: expand` / `selectedItem` / `collapse` | `ComboBox.cs:53-56` (3 retries, then ignored) | `ComboBox.cs:68-71` |
| `windows: addToSelection` / `removeFromSelection` | `ListItem.cs:29, 34` (no fallback) | `ListItem.cs:42, 47` |
| `windows: scrollToVisible` | `Component.cs:166` (errors swallowed) | `Component.cs:193` (errors swallowed) |
| `powerShell` `{ command }` | `AppSession.cs:34` | not yet (planned §6 step 5) |
| `automationName` | `DriverFactory.cs:38` `"Windows"` | `WrappedWebDriverCreateService.cs:49` `"NovaWindows"` |
| caps `app` / `appTopLevelWindow` (`Root`), `appWorkingDir`, `createSessionTimeout`, `ms:waitForAppLaunch`, `ms:experimental-webdriver`, `newCommandTimeout` | `DriverFactory.cs:24-54` | `WrappedWebDriverCreateService.cs:41-65` |
| Appium server args | `AppiumServerPlugin.cs:36-41`: base path, `--use-drivers windows`, `--allow-insecure windows:power_shell` | `App.cs:136`: `--allow-insecure=novawindows:power_shell` |

### 11.4 Feature matrix: Windows vs NovaWindows

Sources, all installed locally:

* **W:** `%USERPROFILE%\.appium\node_modules\appium-windows-driver` **5.1.9** (`build/lib/…`)
* **N:** `%USERPROFILE%\.appium\node_modules\appium-novawindows-driver` **1.4.0-dev.9** (`lib/…`, `csharp/…`). This is a **dev build**; re-check the matrix against the version that gets pinned.
* **A:** Appium server **3.2.2** at `%USERPROFILE%\.npm-global\node_modules\appium` (and its `@appium/base-driver`, `@appium/schema`)

✅ = verified supported, ❌ = verified **not** supported, **verify** = not provable from the local sources and needs a runtime check. Every ✅/❌ cell cites its source.

**Driver identity and server**

| # | Item | Windows | NovaWindows |
|---|---|---|---|
| S1 | `automationName` | ✅ `Windows` (W `package.json` → `appium.automationName`) | ✅ `NovaWindows` (N `package.json:53`) |
| S2 | Appium driver name for `--use-drivers` and the insecure prefix | ✅ `windows` (W `package.json` → `appium.driverName`) | ✅ `novawindows` (N `package.json:52`) |
| S3 | Required Appium server | ✅ `^3.0.0-rc.2` for **this** version (W `package.json:75-76` peerDependencies) | ✅ `^3.0.0-rc.2` (N `package.json:41-42`) |
| S4 | `--use-drivers` accepts a list | ✅ (A `@appium/schema/build/lib/appium-config-schema.js:294-300`: comma-delimited list; "Windows environments may require wrapping the comma-delimited string with quotes") | same |
| S5 | `--allow-insecure` needs a `<driver>:` or `*:` prefix | ✅ (A `build/lib/insecure-features.js:76-90` `validateFeatures`; matched against the driver name at `:105-112`) | same |
| S6 | Backend | ✅ proxies to **WinAppDriver.exe** (W `driver.js:164-175` `canProxy` "we can always proxy to the WinAppDriver server"; `installer.js:20`, `install-wad` hint `:100`) | ✅ own UIA server, no WinAppDriver (N `csharp/NovaUIAutomationServer`) |
| S7 | Driver version on the **RMH machines** (Appium 2 + `/wd/hub`) | **verify**: 5.1.9 needs Appium 3 (S3), so RMH must run an older major. Rows below are verified for 5.1.9 only. | n/a |
| S8 | WinAppDriver prerequisites on CI (WAD installed, Windows Developer Mode) | **verify** on the CI image | n/a |

**Capabilities**

| # | Capability | Windows | NovaWindows |
|---|---|---|---|
| C1 | `app` (exe path, UWP AUMID) | ✅ declared (W `desired-caps.js` `app`); executed by WinAppDriver | ✅ (N `driver.ts:232`) |
| C2 | `app = "Root"` (desktop session) | **verify**: not handled in W's source; forwarded to WinAppDriver | ✅ (N `commands/app.ts:158`, `driver.ts:303`, case-insensitive `root`) |
| C3 | `appTopLevelWindow` | ✅ declared (W `desired-caps.js`) | ✅ (N `constraints.ts:18`; a numeric handle is converted to a string at `driver.ts:221-226`; mutually exclusive with `app` at `:260`) |
| C4 | `appWorkingDir` | ✅ declared (W `desired-caps.js`) | ✅ (N `constraints.ts:27`; also used as the PowerShell working directory, `extension.ts:288`) |
| C5 | `createSessionTimeout` | ✅ declared (W `desired-caps.js`) | **verify**: **not declared** in N `constraints.ts`; expected to be ignored |
| C6 | `ms:waitForAppLaunch` | ✅ declared (W `desired-caps.js:32`) | ✅ (N `constraints.ts:45`; used as the launch deadline, `commands/app.ts:181-183`) |
| C7 | `ms:experimental-webdriver` | ✅ declared (W `desired-caps.js:38`) | ✅ declared (N `constraints.ts:48`); **verify** whether N changes any behavior |
| C8 | `newCommandTimeout` | **verify** (an Appium base-driver capability; not checked) | **verify** |
| C9 | Client-side `commandTimeout` (`new WindowsDriver(uri, opts, TimeSpan)`) | ✅ client-only, so the driver doesn't matter (BiFrost `DriverFactory.cs:63-65`) | same |
| C10 | `ImplicitWait` (`Manage().Timeouts()`) | **verify** (proxied to WAD) | **verify** |

**Extension scripts.** An unknown script in Windows hits `@appium/base-driver` `executeMethod`, which throws `UnsupportedOperationError("Unsupported execute method …")` (A `…/base-driver/build/lib/basedriver/commands/execute.js`). In NovaWindows it throws `UnknownCommandError` (N `extension.ts:80-87`).

| # | Script | Windows | NovaWindows |
|---|---|---|---|
| X1 | `windows: invoke` (element) | ❌ not in W `execute-method-map.js`. **BiFrost's first click strategy always fails on `Windows` and falls through to the native click.** | ✅ `invoke → patternInvoke` (N `extension.ts:31, 148-150`) |
| X2 | `windows: hover` `{startElementId,endElementId,durationMs}` | ✅ (W `execute-method-map.js:81-93`) | ✅ (N `extension.ts:52, 442-451`) |
| X3 | `windows: setValue` (element, value) | ❌ not in the map; **BiFrost always falls back to clear + `SendKeys`** | ✅ (N `extension.ts:42, 201-207`; falls back to RangeValue) |
| X4 | `windows: toggle` | ❌ not in the map; BiFrost falls back to click | ✅ (N `extension.ts:41`) |
| X5 | `windows: select` | ❌ not in the map; ComboBox falls back to `SendKeys`; **`ListItem.Select()` throws** | ✅ (N `extension.ts:40`) |
| X6 | `windows: expand` / `collapse` | ❌ not in the map | ✅ (N `extension.ts:32-33`) |
| X7 | `windows: selectedItem` | ❌ not in the map; **`ComboBox.SelectedItem` exhausts its 3 retries** | ✅ (N `extension.ts:35`) |
| X8 | `windows: addToSelection` / `removeFromSelection` | ❌ not in the map; **throws** | ✅ (N `extension.ts:38-39`) |
| X9 | `windows: scrollToVisible` (the name **both** frameworks send) | ❌ not in the map | ❌ **not in N's command table either**. N names it `scrollIntoView` (N `extension.ts:34`). **`ScrollToVisible()` is a silent no-op on both drivers today**, because the error is swallowed (`Component.cs:193`, BiFrost `Component.cs:166`). |
| X10 | `windows: scrollIntoView` | ❌ not in the map | ✅ (N `extension.ts:34`) |
| X11 | Extra scripts not used today | ✅ `click`, `clickAndDrag`, `scroll`, `keys`, `launchApp`, `closeApp`, clipboard, file, recording (W `execute-method-map.js`) | ✅ the same set plus `setFocus`, `maximize`/`minimize`/`restore`/`close`, `getValue`, `isMultiple`, `allSelectedItems`, `getWindowElement`, `cacheRequest` (N `extension.ts:29-63`) |

**PowerShell, for `EnsureExited`**

| # | Item | Windows | NovaWindows |
|---|---|---|---|
| P1 | Script name `powerShell`, gated by the `power_shell` insecure feature | ✅ (W `commands/execute.js:9, 19-21`; `constants.js:4`) | ✅ (N `extension.ts:103-105`; `constants.ts:1`) |
| P2 | Argument shape `{ "command": "<one line>" }`, which is what BiFrost sends | ✅ `command` takes priority over `script`; **line breaks are rejected** (W `commands/powershell.js:20-62`) | ✅ accepts `{command}` or `{script}` or a string (N `extension.ts:275-284`) |
| P3 | How it runs | ✅ `powershell.exe -command …`, returns stdout, throws on a non-zero exit code (W `powershell.js:56-86`) | ✅ `powershell.exe -NoProfile -NonInteractive -Command` from a base64-encoded script, stdout redirected (N `csharp/…/Commands/ProcessCommands.cs:123-145`) |
| P4 | `Wait-Process …; 'killed'/'exited'` returns exactly that token (after `Trim()`) | **verify** at runtime | **verify** at runtime |
| P5 | Can kill an elevated app | **verify**: only if Appium runs elevated (BiFrost `AppSession.cs` comment) | **verify** |

Q1 ("does NovaWindows support the `powerShell` script for `EnsureExited`?") is **answered from source: yes** (P1–P3). Only the runtime rows P4 and P5 remain.

**Session commands and locators used by BELLATRIX**

| # | Item | Windows | NovaWindows |
|---|---|---|---|
| L1 | Screenshot (`VanillaWebDriverScreenshotEngine`) | **verify** (proxied to WAD) | ✅ (N `commands/app.ts:66-79`) |
| L2 | Page source (`AppService.GetCurrentViewSnapshot`, LLM locators) | **verify**: proxied to WAD; the XML shape differs from N | ✅ (N `commands/app.ts:62-63`); **verify** that the XML attributes match what `AppService` parses |
| L3 | `AutomationId`, `AccessibilityId`, `Name`, `ClassName`, `TagName` locators | **verify** (WAD semantics) | **verify** (N's own UIA mapping) |
| L4 | XPath locators | **verify** (WAD XPath) | **verify** (N's own engine, `lib/xpath`) |
| L5 | Window maximize/resize (`AppService.Maximize`, `ChangeWindowSize`, D8) | **verify** | **verify** |
| L6 | `Navigate().Back()/Forward()` (`AppService`) | **verify** | **verify** |
| L7 | UWP app (`Constants.UniversalAppPath`) | **verify** | **verify** |

### 11.5 Appium server: drivers and insecure flags derived from config

The server plugin (`AppiumServerPlugin` in the migration plan's D10, which `App.StartAppiumServer()` delegates to) builds its arguments from the **set of profiles in use**. No driver names are hard-coded in it.

1. Collect the names: `executionSettings.automationName` (or the default `NovaWindows`) plus every `applicationsSettings.*.automationName`. Resolve each through `DesktopDriverProfileResolver`; an unknown name fails here, before any test runs.
2. `--use-drivers` = the distinct `profile.AppiumDriverName` values, comma-joined and quoted (S4). Examples: `"windows"`, `"novawindows"`, `"windows,novawindows"`.
3. `--allow-insecure` = for each profile, `"{AppiumDriverName}:{feature}"` for each of its `InsecureFeatures`, comma-joined. It is omitted entirely when empty. Both built-in profiles declare `power_shell`, because both support `EnsureExited` (P1). A user profile without PowerShell declares nothing.
4. `--base-path` = `new Uri(url).AbsolutePath` when it isn't `/`.
5. For each profile, run `profile.Install` (install if missing; NovaWindows keeps today's update-on-every-run and the Appium ≥ 3 check, so existing users see no change). The Windows profile's pre-check reports a missing WinAppDriver (S6) with a clear message and the `appium driver run windows install-wad` hint.
6. With `shouldStartLocalService: false` (RMH today), nothing is started. The profile still drives the session; the user is responsible for starting the external server with matching `--use-drivers` and `--allow-insecure`. A missing `power_shell` flag is detected by the degrade path (§11.6), as BiFrost does today.

### 11.6 When a profile lacks a feature: log once, degrade, never throw

The rule: **a feature the driver doesn't support never causes an exception by itself.** The component logs once per (profile, feature) through `DesktopDriverFeatureNotices` (`Logger.LogWarning`) and then takes the degrade path. If the degrade path itself fails against the app, that failure is a normal test failure.

| Feature missing | Degrade path | Today on `Windows` |
|---|---|---|
| Invoke | native click, then the Actions click (the existing chain, minus the first hop) | same outcome, one failed round trip saved |
| SetValue | clear + `SendKeys` | same |
| Toggle | `Click()` | same |
| Select (ComboBox) | `SendKeys(value)` when the text differs | same |
| Select (ListItem) | `Click()` | **throws today**, so this is a behavior change (B13) |
| AddToSelection / RemoveFromSelection | Ctrl+click through Actions (RemoveFromSelection only when the item is selected); **verify** on WinForms and WPF list boxes | **throws today** (B13) |
| Expand / SelectedItem / Collapse | keep the 3 retries, then return the item whose `SelectionItem.IsSelected` attribute is true (**verify** that WAD exposes that attribute through `GetAttribute`); `null` if none | exhausts retries |
| ScrollIntoView | no-op, logged once | silent no-op on **both** drivers today (X9) |
| Hover (close-button workaround at session start) | skipped | n/a (both support it) |
| PowerShell (`EnsureExited`) | skip the exit check, log once (the BiFrost message about `--allow-insecure=<driver>:power_shell`), return "not killed" | BiFrost's log-once already does this |

Two situations look the same to the user and are handled the same way: the server was started **without** `power_shell` (the driver throws an "insecure feature" error), and the profile declares no PowerShell. Both take the log-once path; neither throws (BiFrost `App.cs:67-75` behavior, ported).

### 11.7 Breaking-change register additions

| # | Change | Breaking? | Mitigation |
|---|---|---|---|
| B12 | `ScrollToVisible()` sends `windows: scrollIntoView` on NovaWindows (X9/X10); it really scrolls now instead of being a no-op | **Behavior change** for NovaWindows users: elements scroll that didn't before | Release note. It can be disabled per profile (`GetScript(ScrollIntoView)` returns null) if it causes problems. |
| B13 | On a profile without `select` / `addToSelection` / `removeFromSelection`, `ListItem` degrades to a click or Ctrl+click instead of throwing | Behavior change only for the `Windows` driver, where these always threw | Release note |
| B14 | Unknown `automationName` fails at server and session start | No: today the value can't be set | n/a |
| B15 | `executionSettings.automationName` default `NovaWindows`, NovaWindows update-on-every-run kept | No | n/a |

### 11.8 Tests: running the desktop smoke tests on both drivers

Mechanism: two runsettings files in `tests/Bellatrix.Desktop.Tests/` set the config through environment variables (VSTest `RunConfiguration/EnvironmentVariables`; `ConfigurationService` reads environment variables, §11.1):

```xml
<!-- desktop.windows.runsettings -->
<RunSettings><RunConfiguration><EnvironmentVariables>
  <desktopSettings__executionSettings__automationName>Windows</desktopSettings__executionSettings__automationName>
</EnvironmentVariables></RunConfiguration></RunSettings>
<!-- desktop.novawindows.runsettings: same with NovaWindows -->
```

CI runs `dotnet test … --settings desktop.windows.runsettings --filter TestCategory=DriverSmoke`, then the same with `desktop.novawindows.runsettings`. The Appium server is started by the plugin with `--use-drivers "windows,novawindows"` (§11.5), so a single server serves both runs and the mixed test D12.

New category `DriverSmoke`, applied to existing tests where possible and to new tests in `tests/Bellatrix.Desktop.Tests/Drivers/`:

| # | Case | Proves (matrix row) | Expected on Windows / NovaWindows |
|---|---|---|---|
| D1 | `ButtonControlTestsWpf` / `…WinForms` (existing): click | X1 + the degrade path | native click / invoke; both pass |
| D2 | TextField `SetText` then validate text (WPF, WinForms) | X3 | clear + `SendKeys` / setValue |
| D3 | CheckBox `Check`/`Uncheck` (existing CheckBox tests) | X4 | click / toggle |
| D4 | ComboBox `SelectByText` + `SelectedItem` | X5–X7 | degrade (`SendKeys`, `IsSelected`) / patterns |
| D5 | ListBox `ListItem.Select`, `AddToSelection`, `RemoveFromSelection` | X5, X8, B13 | Ctrl+click / patterns; **no exception** on either |
| D6 | `Hover` on a button | X2 | both scripts |
| D7 | `ScrollToVisible` on an off-screen element, then a click succeeds | X9/X10, B12 | logged no-op (pick an element that is clickable without scrolling on Windows) / real scroll |
| D8 | Locators: one element each by AutomationId, AccessibilityId (`Name`), ClassName, XPath | L3, L4 | both find the same element |
| D9 | A failing test produces a screenshot file | L1 | both |
| D10 | `AppService.GetCurrentViewSnapshot()` returns a non-empty JSON array that contains a known AutomationId | L2 | both |
| D11 | `CloseOrKill` with a no-op UI close: the process is gone and nothing throws | P1–P4 | both (needs `power_shell`) |
| D12 | **Mixed drivers in one test:** `[App("WpfSample")]` with `automationName: NovaWindows`, `App.SwitchApp("WinFormsSample")` with an app override `Windows`; click in both; switch back | §11.1 precedence, `_activeDriverProfile` capture | runs once (not in both runsettings) |
| D13 | Feature notice logged once: call `ScrollToVisible` 3× on Windows; `DesktopDriverFeatureNotices` reports one notice | §11.6 | Windows only |
| D14 | Unknown `automationName` (`"Foo"`) fails with a message listing `Windows, NovaWindows` | B14 | unit-level (no Appium); resolver test |
| D15 | Precedence unit tests: app override > execution value > default; legacy path ignores app overrides | §11.1 | unit-level (no Appium) |
| D16 | Server argument builder unit tests: `{NovaWindows}` → `--use-drivers "novawindows"` and `novawindows:power_shell`; both drivers → both; a profile without insecure features → no `--allow-insecure` | §11.5 | unit-level |
| D17 | Root session (`app: Root`) can find the WPF app's main window by Name | C2 | **verify** row for Windows |
| D18 | UWP app (`Constants.UniversalAppPath`) starts | L7 | both, or mark the test per driver if one can't |

D1–D11 run in both runsettings. D12 needs both drivers installed and loaded. D14–D16 need no app or Appium and run in the normal CI pass.

### 11.9 Implementation placement in the §6 plan **[Superseded 2026-11-18: only the `automationName` switch is REQUIRED (Scope item 1); 3a/3b and the rest are Deferred backlog M-12 to M-15]**

| New step | Content | Depends on |
|---|---|---|
| **3a** (after step 3) | `IDesktopDriverProfile`, the two profiles, resolver, notices, `AddDriverProfiles()` from `AddLifecycle()`; `executionSettings.automationName` and `applicationsSettings.*.automationName`; `WrappedWebDriverCreateService` uses `ApplyCapabilities`. Behavior equals today when nothing is configured. | step 1 (settings) |
| **3b** | Components switch to `GetScript(...)` and the §11.6 degrade paths (B12, B13). Smoke tests D1–D10, D13–D16. | 3a |
| step 5 | `EnsureExited` uses `GetScript(PowerShell)`/`BuildScriptArgs`; D11. | 3a, 4 |
| step 6 | The server argument builder (§11.5); D16, D12. | 3a |

Step 0 (§10) is now its own **patch release**, shipped first (decision 2026-11-17). See §10.8.

---

## Deferred backlog

Added 2026-11-18. Not done as part of the RMH migration (see [Scope: REQUIRED items only](#scope-required-items-only)). The design text stays where it is; this is the index.

| # | Item (section) | Why it is deferred | Evidence that would move it to REQUIRED |
|---|---|---|---|
| M-1 | Step 0: async-safe `ServicesCollection.Current` (§10.5) and tests A1/A2 (§10.6). Branch `fix/async-container-context`. | RMH never resolves services after an `await` (see Scope) | RMH code awaits before UI calls or `SwitchApp`, or uses `App.ApiClient`/`GetApiClientService`/in-test plugins/auth attributes |
| M-2 | Allure `ThreadLocal` state moved into the container (§10.8, E7) | Only attachments added after an `await` are affected; RMH's API attachments are deferred | RMH needs API attachments in Allure |
| M-3 | `IViewSnapshotProvider` scoping (§6 step 2, B3) | LLM/snapshot features only | A non-LLM plugin used in RMH runs depends on it after `SwitchApp` |
| M-4 | Per-app timeouts: `AppInitializationInfo.TimeoutSettings`, `DesktopTimeouts.Current`, the 8 call sites in §1.2, `GetSection<T>(path, target)` merge (§6 steps 1 and 3) | RMH config has no per-app `timeoutSettings` | Per-app timeouts are added to RMH config |
| M-5 | `commandTimeout` / `newCommandTimeout` and the "commandTimeout is 0" warning (§3, §6 steps 1 and 3, B1 part, B4) | Differs only for a hung driver command | The baseline or a comparison shows command-timeout failures or hangs |
| M-6 | Explicit `workingDirectory` (§3) | It equals `GetDirectoryName(appPath)` for all RMH apps | An app with a different working directory |
| M-7 | Waits and validators use the owning component's driver and timeouts; `App.ActiveAppName` (§2.3, §6 step 4 subset) | RMH creates components on access (`=>` properties), so they bind to the active app, as in BiFrost | A component cached across a `SwitchApp` |
| M-8 | "power_shell not allowed" log-once flag in `Main`; `AppExitVerificationPlugin` (§6 step 5) | Log noise / optional | — |
| M-9 | Appium server checks in `StartAppiumServer` (§3, §6 step 6, B6) | RMH runs with `shouldStartLocalService: false` | The RMH test machine lets the tests start Appium |
| M-10 | Applying `[App(path, w, h)]` sizes, `applyWindowSize` (§6 step 7, B7) | RMH sets no sizes; maximize is REQUIRED (Scope item 2) | — |
| M-11 | `PerformWithFallbacks` for click, hover and set text (§3 "Fallback clicks") | On `Windows` both frameworks end at the native click (§11.4 X1) | Click-related differences in an RMH comparison |
| M-12 | `IDesktopDriverProfile`, the two profiles, resolver, `DesktopDriverFeatureNotices`, `AddDriverProfiles()`; per-app `automationName` (§11.1 row 1); unknown-name validation (B14) (§11.2, §11.9 3a) | The config switch (Scope item 1) is enough, because the capability set is identical | RMH needs different drivers per app, or a driver needs different start capabilities |
| M-13 | Component script routing and degrade paths (§11.6, §11.9 3b, B12, B13) | BELLATRIX already falls back the way BiFrost does for every action RMH uses; `ScrollToVisible` is a no-op in both; RMH has no `ListItem.Select`/`AddToSelection` or `ComboBox.SelectedItem` calls | An RMH test uses one of those, or a comparison shows a component-action difference |
| M-14 | Server argument builder from config (§11.5) | Applies only when BELLATRIX starts Appium | As for M-9 |
| M-15 | Dual-driver smoke tests D1–D18 and the runsettings (§11.8) | Not needed for parity; RMH stays on `Windows` | A decision to move RMH to NovaWindows |
| M-16 | Tests T3, T5, T6, T8, T9, T11, T13, T14, T15 (§7) and the NUnit template smoke copy | They cover deferred items or non-RMH paths; RMH's NUnit runs exercise the NUnit path | The matching item becomes REQUIRED |
| M-17 | Parallel class isolation (§2.2), test T16, and the `TestContext`-in-`Main` issue (§10.2 case 4a) | RMH runs sequentially | RMH turns on parallel runs |

**Count: 17 deferred items in this doc.** Most overlap with the RMH plan's backlog:

| This doc | RMH plan backlog |
|---|---|
| M-1 | D-1 |
| M-2 | D-2 |
| M-3 | D-41 |
| M-4 | D-18 |
| M-5 | D-3 |
| M-6 | D-38 |
| M-7 | D-40 |
| M-8 | D-42 |
| M-9 | D-8 |
| M-10 | D-37 |
| M-11 | D-5 |
| M-12 | D-9 |
| M-13 | D-10 |
| M-14 | D-11 |
| M-15 | D-12 |
| M-17 | D-44 |

M-16 (tests) is the only item that appears in this doc alone.

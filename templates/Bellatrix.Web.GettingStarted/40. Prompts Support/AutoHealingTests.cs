using Bellatrix.Web.LLM;
using Bellatrix.Web.NUnit;
using NUnit.Framework;
using System;
using System.Linq;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class AutoHealingTests : WebTest
{
    // 🔍 SELF-HEALING TRIGGER CONDITIONS AND HTML DIFFERENCES:
    //
    // The BELLATRIX self-healing logic triggers automatically when:
    // 1. `EnableSelfHealing` is set to true in your LargeLanguageModelsSettings.
    // 2. A call to `WrappedElement` throws due to a missing or changed locator.
    // 3. The framework attempts to heal by comparing the previously stored page summary
    //    and the current view via `GetPageSummaryJson()`.
    // 4. An AI-generated XPath is inferred from the DOM diff and tried automatically.
    //
    // ✅ Only original working locators are saved in the DB.
    // ❌ The healed fallback is NOT saved — it’s used once and logged.
    //
    // 🧩 PRACTICAL HTML DIFFERENCES THAT WILL TRIGGER HEALING:
    //
    // 1. <input id="emailUpdated" ...> changed to <input id="email" ...>
    //    → Any POM using "emailUpdated" will fail and heal.
    //
    // 2. <input id="cc-cvv222"> is completely removed in `index.new.html`.
    //    → Healing activates if you're using that CVV field.
    //
    // 3. Submit buttons change:
    //    - `index.html`: "Order 2" and "Proceed to checkout"
    //    - `index.new.html`: "Checkout"
    //    → XPath using `text()='Order 2'` will fail and heal.
    //
    // 4. Some missing <label> tags are added in `index.new.html`
    //    → XPath relying on `following-sibling::label` may break.
    //
    // ✅ Use these differences intentionally in tests to validate the self-healing logic.
    //
    [Test]
    public void FillCheckoutFormAndSubmit_SelfHealingEnabled()
    {
        // uncomment if you want to run against empty locator cache DB.
       // LocatorSelfHealingService.ClearProjectEntries();

        // Execute once for index.html, then copy the content of index.new.html to index.html, rerun the test
        App.Navigation.NavigateToLocalPage("TestPages\\Checkout\\index.html");

        var info = new ClientInfo
        {
            FirstName = "Anton",
            LastName = "Angelov",
            Username = "aangelov",
            Email = "info@berlinspaceflowers.com",
            Address1 = "1 Willi Brandt Avenue Tiergarten",
            Address2 = "Lützowplatz 17",
            CountryIndex = 1,
            StateIndex = 1,
            Zip = "10115",
            CardName = "Anton Angelov",
            CardNumber = "1234567890123456",
            CardExpiration = "12/23",
            CardCVV = "123"
        };

        var localCheckoutPage = App.Create<LocalCheckoutPage>();
        localCheckoutPage.FillBillingInfo(info);
        localCheckoutPage.SubmitOrder();

        //Assert.Fail("Something wrong happened.");
    }
}

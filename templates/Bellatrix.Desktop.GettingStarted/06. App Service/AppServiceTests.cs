using Bellatrix.Desktop.NUnit;
using NUnit.Framework;
using System.Diagnostics;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]

// 1. With the BELLATRIX desktop library, you can test various Windows applications written in different technologies such as- WPF, WinForms or UWP (Universal Windows Platform).
// For the first two, you need to pass the path to your application's executable.
[App(Constants.WpfAppPath, Lifecycle.RestartEveryTime)]

// WinForms
// [App(@"C:\demo-apps\WindowsFormsSampleApp.exe", Lifecycle.RestartEveryTime)]
//
// For UWP applications you need to set the application's installation GUID
// [App("369ede42-bebe-41ea-a02a-0da04991478e_q6s448gyj2xsw!App", Lifecycle.RestartEveryTime)]
public class AppServiceTests : DesktopTest
{
    [Test]
    [Category(Categories.CI)]
    public void MessageChanged_When_ButtonHovered_Wpf()
    {
        var button = App.Components.CreateByName<Button>("E Button");

        button.Click();

        // 2. Through AppService you can control the certain aspects of your application such as getting its title,
        // maximize it or going backwards or forward
        Debug.WriteLine(App.AppService.Title);

        App.AppService.Maximize();
        App.AppService.Forward();
        App.AppService.Back();
    }
}
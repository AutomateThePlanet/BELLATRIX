using Bellatrix.Mobile.Services.IOS;

namespace Bellatrix.Mobile.IOS.GettingStarted.CommonServicesExtensions;

public static class NavigationServiceExtensions
{
    // 1. One way to extend the BELLATRIX common services is to create an extension method for the additional action.
    // 1.1. Place it in a static class like this one.
    // 1.2. Create a static method for the action.
    // 1.3. Pass the common service as a parameter with the keyword 'this'.
    // 1.4. Access the native driver via WrappedDriver.
    //
    // Later to use the method in your tests, add a using statement containing this class's namespace.
    public static void LoginToApp(this IOSAppService appService, string userName, string password)
    {
        var elementCreateService = new ComponentCreateService();
        var userNameField = elementCreateService.CreateById<TextField>("IntegerA");
        var passwordField = elementCreateService.CreateById<Password>("IntegerB");
        var loginButton = elementCreateService.CreateById<Button>("ComputeSumButton");

        userNameField.SetText(userName);
        passwordField.SetPassword(password);
        loginButton.Click();
    }
}
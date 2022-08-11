using Bellatrix.Mobile.Services.Android;

namespace Bellatrix.Mobile.Android.GettingStarted.CommonServicesExtensions;

public static class NavigationServiceExtensions
{
    // 1. One way to extend the BELLATRIX common services is to create an extension method for the additional action.
    // 1.1. Place it in a static class like this one.
    // 1.2. Create a static method for the action.
    // 1.3. Pass the common service as a parameter with the keyword 'this'.
    // 1.4. Access the native driver via WrappedDriver.
    //
    // Later to use the method in your tests, add a using statement containing this class's namespace.
    public static void LoginToApp(this AndroidAppService appService, string userName, string password)
    {
        var elementCreateService = new ComponentCreateService();
        var userNameField = elementCreateService.CreateByIdContaining<TextField>("textBox");
        var passwordField = elementCreateService.CreateByIdContaining<Password>("passwordBox");
        var loginButton = elementCreateService.CreateByIdContaining<Button>("loginButton");

        userNameField.SetText(userName);
        passwordField.SetPassword(password);
        loginButton.Click();
    }
}

Namespace Bellatrix.Web.NUnit.Tests
    <NUnit.Framework.TestFixtureAttribute>
    <Browser(BrowserType.Chrome, BrowserBehavior.ReuseIfStarted)>
    <VideoRecording(VideoRecordingMode.DoNotRecord)>
    <ScreenshotOnFail(True)>
    Public Class LoginTests
        Inherits WebTest

        Public Overrides Sub TestInit()
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/my-account/")
        End Sub

        <NUnit.Framework.TestAttribute>
        '''[VideoRecording(VideoRecordingMode.Ignore)]
        '''[ScreenshotOnFail(false)]
        Public Sub SuccessfullyLoginToMyAccount()
            Dim userNameField = App.ElementCreateService.CreateById(Of TextField)("username")
            Dim passwordField = App.ElementCreateService.CreateById(Of Password)("password")
            Dim loginButton = App.ElementCreateService.CreateByXpath(Of Button)("//button[@name='login']")
            userNameField.SetText("info@berlinspaceflowers.com")
            passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$")
            loginButton.Click()
            Dim myAccountContentDiv = App.ElementCreateService.CreateByClass(Of Div)("woocommerce-MyAccount-content")
            myAccountContentDiv.ValidateInnerTextContains("Hello info1")
            Dim logoutLink = App.ElementCreateService.CreateByInnerTextContaining(Of Anchor)("Log out")
            logoutLink.ValidateIsVisible()
            logoutLink.Click()
        End Sub

        <NUnit.Framework.TestAttribute>
        Public Sub SuccessfullyLoginToMyAccount1()
            Dim userNameField = App.ElementCreateService.CreateById(Of TextField)("username")
            Dim passwordField = App.ElementCreateService.CreateById(Of Password)("password")
            Dim loginButton = App.ElementCreateService.CreateByXpath(Of Button)("//button[@name='login']")
            userNameField.SetText("info@berlinspaceflowers.com")
            passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$")
            loginButton.Click()
            Dim myAccountContentDiv = App.ElementCreateService.CreateByClass(Of Div)("woocommerce-MyAccount-content")
            myAccountContentDiv.ValidateInnerTextContains("Hello info1")
            Dim logoutLink = App.ElementCreateService.CreateByInnerTextContaining(Of Anchor)("Log out")
            logoutLink.ValidateIsVisible()
            logoutLink.Click()
        End Sub
    End Class
End Namespace

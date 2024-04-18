using Bellatrix.Playwright.Enums;
using NUnit.Framework.Internal;

namespace Bellatrix.Playwright.GettingStarted;

[TestFixture]
[Browser(BrowserTypes.Chrome, Lifecycle.ReuseIfStarted)]
public class IFrameAndShadowDOMTests : NUnit.WebTest
{
    [Test]
    public void TestFindingIFramesOnThePage()
    {
        App.Navigation.Navigate("https://www.w3schools.com/tags/tryit.asp?filename=tryhtml_iframe");
        App.Components.CreateById<Button>("accept-choices").Click();

        // If you create a Frame component, it automatically will start searching for elements inside an <iframe> html element
        // You can work with iframes in bellatrix.playwright module as if they were any other component
        var parentIFrame = App.Components.CreateByXpath<Frame>("//iframe[@id='iframeResult']");

        // This <iframe> is located inside the parentIFrame
        var iframe = parentIFrame.CreateByXpath<Frame>("//iframe[@src='https://www.w3schools.com']");

        // This button is located inside iframe
        iframe.CreateByXpath<Button>("//div[@id='accept-choices']").Click();

        // In case you want to reuse the component locating logic, but stop looking inside the <iframe>, you can use the As<Component>() method;
        // It will automatically switch to using normal locating strategies and you will stop being able to pierce the <iframe>
        var iframeAsDiv = iframe.As<Div>();
        iframeAsDiv.CreateByXpath<Heading>("//preceding-sibling::h1").ValidateInnerTextContains("The iframe element");

        // Again, if you create a 'normal' component and want to reuse it, this time to search inside it, as if it is <iframe>, you use As<Component>() method again
        var iframeAsNormalComponent = parentIFrame.CreateByXpath<Div>("//iframe[@src='https://www.w3schools.com']");
        var iframeAsFrame = iframeAsNormalComponent.As<Frame>();

        Assert.That(iframeAsFrame.CreateByXpath<Div>("//div[@id='subtopnav']//a[@title='HTML Tutorial']").InnerText.Equals("HTML"));
    }

    [Test]
    public void TestFindingElementsInShadowDOM()
    {
        App.Navigation.Navigate("https://web.dev/articles/shadowdom-v1");

        // First, we create a Frame component, because the elements for this test are inside of it
        var iframe = App.Components.CreateByXpath<Frame>("//iframe[contains(@src, 'raw/fancy-tabs-demo.html')]");
        iframe.ScrollToVisible();

        var tabList = iframe.CreateByAttributesContaining<Div>("role", "tablist");

        // Be mindful that when in the Shadow DOM, Xpath doesn't work, you'll have to use CSS only
        var tabs = tabList.CreateById<Div>("tabsSlot");

        // This Xpath won't find the element, even though it searches by id as well:
        // var tabs = tabList.CreateByXpath<Div>("//*[@id='tabsSlot']");

        Assert.That(tabs.GetAttribute("name").Equals("title"));
    }
}

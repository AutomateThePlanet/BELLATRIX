using Bellatrix.Playwright.Components;
using Bellatrix.Playwright.Enums;
using Microsoft.Playwright;
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
        var iframe = App.Components.CreateByXpath<Frame>("//iframe[contains(@src, 'raw/fancy-tabs-demo.html')]");
        iframe.ScrollToVisible();

        // As you might know, when working with shadowRoot, we can use only CSS locators, which might prove difficult
        // When we want to find an element relative to another or an element by its inner text.
        // BELLATRIX offers a solution to this problem.

        // Not going into detail, this works in the following way:
        // 1. Find the shadow root
        // 2. Get its inner HTML
        // 3. Get the element using AngleSharp to navigate inside it with xpath
        // 4. Get its absolute position in the form of an absolute xpath
        // 5. Convert the absolute xpath into CSS
        // 6. Use this CSS locator to find the actual element on the page

        // Let's test a simply shadow DOM:
        // <div id="tabs">
        //   <slot id="tabsSlot" name="title"></slot>
        // </div>
        // <div id="panels">
        //   <slot id="panelsSlot"></slot>
        // </div>

        var shadowRoot = iframe.CreateByXpath<ShadowRoot>("//fancy-tabs");

        // Absolute xpath: /div[1]/slot
        // CSS locator: div:nth-child(1)/slot
        var elementInShadow = shadowRoot.CreateByXpath<Div>("//div[@id='tabs']//slot[@id='tabsSlot']");

        // What if we wanted to go back in the DOM?
        // Absolute xpath: /div[1]
        // CSS locator: div:nth-child(1)
        var parentInShadow = elementInShadow.CreateByXpath<Div>("//parent::div");

        // The same as elementInShadow...
        // Absolute xpath: /div[1]/slot
        // CSS locator: div:nth-child(1)/slot
        var slot = parentInShadow.CreateByXpath<Div>("//slot");

        Assert.That(slot.GetAttribute("name").Equals("title"));

        // More complex xpath will also work the same way.
        // Of course, the test here doesn't make much sense, but it is a showcase of
        // what you can do with BELLATRIX regarding shadow DOM:
        // - Use xpath to locate elements inside the shadow DOM
        // - Go 'back' in the DOM tree (something you cannot achieve with CSS)
        // - Locate elements by their inner text (something you cannot achieve with CSS)
    }
}

using Bellatrix.Web.Components;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class ShadowDomTests : NUnit.WebTest
{
    [Test]
    public void TestFindingElementsInShadowDOM()
    {
        App.Navigation.Navigate("https://web.dev/articles/shadowdom-v1");
        var iframe = App.Components.CreateByXpath<Frame>("//iframe[contains(@src, 'raw/fancy-tabs-demo.html')]");
        iframe.ScrollToVisible();
        App.Browser.SwitchToFrame(iframe);

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

        var shadowRoot = App.Components.CreateByXpath<ShadowRoot>("//fancy-tabs");

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

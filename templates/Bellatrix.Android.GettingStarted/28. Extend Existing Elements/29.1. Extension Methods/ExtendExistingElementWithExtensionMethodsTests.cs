// 1. To use the additional method you created, add a using statement to the extension methods' namespace.
using Bellatrix.Mobile.Android.GettingStarted.Custom;

using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

[TestFixture]
public class ExtendExistingElementWithExtensionMethodsTests : NUnit.AndroidTest
{
    [Test]
    [Ignore("API example purposes only. No need to run.")]
    public void ButtonClicked_When_CallClickMethod()
    {
        var button = App.Components.CreateByIdContaining<Button>("button");

        // 2. Use the custom added submit button  with scroll-to-visible lifecycle.
        button.SubmitButtonWithScroll();
    }
}
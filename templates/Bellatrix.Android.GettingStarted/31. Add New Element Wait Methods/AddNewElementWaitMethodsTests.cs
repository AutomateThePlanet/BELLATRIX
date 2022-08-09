// 1. You need to add a using statement to the namespace where the new wait extension methods are situated.

using Bellatrix.Mobile.Android.GettingStarted.ExtensionMethodsWaitMethods;

using NUnit.Framework;

namespace Bellatrix.Mobile.Android.GettingStarted;

[TestFixture]
public class AddNewElementWaitMethodsTests : NUnit.AndroidTest
{
    [Test]
    [Category(Categories.CI)]
    public void ButtonClicked_When_WaitToHaveSpecificContent()
    {
        // 2. After that, you can use the new wait method as it was originally part of Bellatrix.
        var button = App.Components.CreateByIdContaining<Button>("button").ToHaveSpecificContent("button");

        button.Click();
    }
}

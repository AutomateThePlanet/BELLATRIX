// 1. You need to add a using statement to the namespace where the extension methods for new locator are situated.
using Bellatrix.Web.GettingStarted.ExtensionMethodsLocators;

using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class NewFindLocatorsTests : NUnit.WebTest
{
    [Test]
    [Ignore("no need to run")]
    public void PromotionsPageOpened_When_PromotionsButtonClicked()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        // 2. After that, you can use the new locator as it was originally part of Bellatrix.
        var promotionsLink = App.Components.CreateByIdStartingWith<Anchor>("promo");

        promotionsLink.Click();
    }
}
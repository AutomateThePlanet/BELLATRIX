using Bellatrix.Mobile.Android;
////using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Bellatrix.Mobile.Tests;

// uncomment to use MSTest
////[TestClass]
[TestFixture]
[Android("pathToApk",
    "appId",
    "7.1",
    "yourTestDeviceName",
    "testActivityName",
    Lifecycle.ReuseIfStarted)]
public class AndroidSampleTests : NUnit.AndroidTest
{
    ////[TestMethod]
    [Test]
    public void CorrectTextDisplayed_When_ClickSubscribeButton()
    {
        var button = App.Components.CreateByIdContaining<Button>("button");

        button.Click();
    }
}
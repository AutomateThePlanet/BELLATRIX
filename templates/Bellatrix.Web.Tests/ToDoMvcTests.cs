using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Bellatrix.Web.Tests
{
    [TestFixture]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    public class ToDoMvcTests : NUnit.WebTest
    {
        [TestCase("Backbone.js")]
        [TestCase("AngularJS")]
        [TestCase("React")]
        [TestCase("Vue.js")]
        [TestCase("CanJS")]
        [TestCase("Ember.js")]
        [TestCase("KnockoutJS")]
        [TestCase("Marionette.js")]
        [TestCase("Polymer")]
        [TestCase("Angular 2.0")]
        [TestCase("Dart")]
        [TestCase("Elm")]
        [TestCase("Closure")]
        [TestCase("Vanilla JS")]
        [TestCase("jQuery")]
        [TestCase("cujoJS")]
        [TestCase("Spine")]
        [TestCase("Dojo")]
        [TestCase("Mithril")]
        [TestCase("Kotlin + React")]
        [TestCase("Firebase + AngularJS")]
        [TestCase("Vanilla ES6")]
        public void VerifyTodoListCreatedSuccessfully(string technology)
        {
            App.Navigation.Navigate("https://todomvc.com/");
            OpenTechnologyTodoApp(technology);
            AddNewToDoItem("Clean the car");
            AddNewToDoItem("Buy eggs");
            AddNewToDoItem("Buy ketchup");
            GetItemCheckBox("Clean the car").Check();
            AssertLeftItems(2);
        }

        private void AssertLeftItems(int expectedCount)
        {
            Span technologyLink = App.Components.CreateByXpath<Span>("//footer/span | //footer/*/span");
            if (expectedCount <= 0)
            {
                technologyLink.ValidateInnerTextIs($"{expectedCount} item left");
            }
            else
            {
                technologyLink.ValidateInnerTextIs($"{expectedCount} items left");
            }
        }

        private void OpenTechnologyTodoApp(string technologyName)
        {
            Anchor technologyLink = App.Components.CreateByLinkText<Anchor>(technologyName);
            technologyLink.Click();
        }

        private CheckBox GetItemCheckBox(string itemName)
        {
            return App.Components.CreateByXpath<CheckBox>($"//input[contains(@class, 'toggle')]/following-sibling::label[text()='{itemName}']/preceding-sibling::input");
        }

        private void AddNewToDoItem(string todoItem)
        {
            TextField todoInput = App.Components.CreateByXpath<TextField>("//input[@placeholder='What needs to be done?']");
            todoInput.SetText(todoItem);
            App.Interactions.Click(todoInput).SendKeys(Keys.Enter).Perform();
        }
    }
}
﻿namespace Bellatrix.Mobile.IOS.GettingStarted
{
    public partial class CalculatorPage
    {
        // 1. All elements are placed inside the file PageName.Elements so that the declarations of your elements to be in a single place.
        // It is convenient since if there is a change in some of the locators or elements types you can apply the fix only here.
        // All elements are implements as properties. Here we use the short syntax for declaring properties, but you can always use the old one.
        // Elements property is actually a shorter version of ComponentCreateService
        public Button Compute => Element.CreateByName<Button>("ComputeSumButton");
        public TextField NumberOne => Element.CreateById<TextField>("IntegerA");
        public TextField NumberTwo => Element.CreateById<TextField>("IntegerB");
        public Label Answer => Element.CreateByName<Label>("Answer");
    }
}
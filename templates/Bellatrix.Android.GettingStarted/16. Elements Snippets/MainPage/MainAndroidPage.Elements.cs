namespace Bellatrix.Mobile.Android.GettingStarted.Snippets
{
    public partial class MainAndroidPage
    {
        // 1. Instead of copy pasting the elements and modifying them by hand, you can use BELLATRIX code snippets to do it lots faster.
        //
        // What is a Code Snippet? - Code snippets are small blocks of reusable code that can be inserted in a code file using a context menu command
        // or a combination of hotkeys. They typically contain commonly-used code blocks such as try-finally or if-else blocks, but they can be used
        // to insert entire classes or methods. In Visual Studio there are two kinds of code snippet: expansion snippets, which are added at a specified
        // insertion point and may replace a snippet shortcut, and surround-with snippets, which are added to a selected block of code.
        // BELLATRIX gives you expansion snippets. You can read more how you can write your snippets in the following blog post- https://www.automatetheplanet.com/visual-studio-code-snippets/
        //
        // For each proxy element in Bellatrix, we give you a corresponding snippet.
        // For example to generate a TextField property, you need to type somewhere in you class atextfield and press Tab.
        // (a comes from Android since you have snippets for other BELLATRIX modules- web and desktop)
        // On the first press of tab, the below code is generated. The first placeholder that is selected is the name of the property.
        // Once you change it, you can press Tab. Then, the second placeholder is chosen- the By locator's type, by default, it is set to Id, press again Tab and change the actual locator.
        public Button Transfer => Element.CreateByIdContaining<Button>("button");
        public CheckBox PermanentTransfer => Element.CreateByIdContaining<CheckBox>("check1");
        public ComboBox Items => Element.CreateByIdContaining<ComboBox>("spinner1");
        public Button ReturnItemAfter => Element.CreateByIdContaining<Button>("toggle1");
        public Label Results => Element.CreateByText<Label>("textColorPrimary");
        public Password Password => Element.CreateByIdContaining<Password>("edit2");
        public TextField UserName => Element.CreateByIdContaining<TextField>("edit");
        public RadioButton KeepMeLogged => Element.CreateByIdContaining<RadioButton>("radio2");

        // 2. Snippets are available for Visual Studio installations only on Windows. You need to make sure you have used the BELLATRIX installer to get them.

        // 3. Here is the list of all available snippets for BELLATRIX Android module:
        // abutton - generates Button
        // acheckbox - generates CheckBox
        // acombobox - generates ComboBox
        // aelement - generates Element
        // agrid - generates Grid
        // aimage - generates Image
        // aimagebutton - generates ImageButton
        // alabel - generates Label
        // anumber - generates Number
        // apassword - generates Password
        // aprogress - generates Progress
        // aradiobutton - generates RadioButton
        // aradiogroup - generates RadioGroup
        // aseekbar - generates SeekBar
        // aswitch - generates Switch
        // atabs - generates Tabs
        // atextfield - generates TextField
        // atogglebutton - generates ToggleButton
    }
}
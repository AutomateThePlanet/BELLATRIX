namespace Bellatrix.Desktop.GettingStarted.Snippets
{
    public partial class MainDesktopPage
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
        // For example to generate a TextField property, you need to type somewhere in you class dtextfield and press Tab.
        // (d comes from desktop since you have snippets for other BELLATRIX modules- web and mobile)
        // On the first press of tab, the below code is generated. The first placeholder that is selected is the name of the property.
        // Once you change it, you can press Tab. Then, the second placeholder is chosen- the By locator's type, by default, it is set to Id,
        // press again Tab and change the actual locator.
        public Button Transfer => Element.CreateByName<Button>("E Button");
        public CheckBox PermanentTransfer => Element.CreateByName<CheckBox>("BellaCheckBox");
        public ComboBox Items => Element.CreateByAutomationId<ComboBox>("select");
        public Button ReturnItemAfter => Element.CreateByName<Button>("DisappearAfterButton1");
        public Label Results => Element.CreateByName<Label>("Result Label");
        public Password Password => Element.CreateByAutomationId<Password>("passwordBox");
        public TextField UserName => Element.CreateByAutomationId<TextField>("textBox");
        public RadioButton KeepMeLogged => Element.CreateByName<RadioButton>("RadioButton");

        // 2. Snippets are available for Visual Studio installations only on Windows. You need to make sure you have used the BELLATRIX installer to get them.

        // 3. Here is the list of all available snippets for BELLATRIX Desktop module:
        // dbutton - generates Button
        // dcalendar - generates Calendar
        // dcheckbox - generates CheckBox
        // dcombobox - generates ComboBox
        // ddate - generates Date
        // delement - generates Element
        // dexpander - generates Expander
        // dimage - generates Image
        // dlabel - generates Label
        // dlistbox - generates ListBox
        // dmenu - generates Menu
        // dpassword - generates Password
        // dprogress - generates Progress
        // dradioButton - generates RadioButton
        // dtabs - generates Tabs
        // dtextArea - generates TextArea
        // dtextField - generates TextField
        // dtime - generates Time
    }
}
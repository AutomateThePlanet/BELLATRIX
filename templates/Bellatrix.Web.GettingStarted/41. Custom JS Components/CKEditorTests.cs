using Bellatrix.Web.ComponentsJS.CKEditor;
using Bellatrix.Web.NUnit;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class CKEditorTests : WebTest
{
    public override void TestInit()
    {
        App.Navigation.Navigate("https://ckeditor.com/ckeditor-5/demo/feature-rich/");
    }

    [Test]
    public void AllTextSelected_When_CallSelectAllMethodCkEditorComponent()
    {
        var editor = App.Components.CreateById<CkEditorComponent>("demo").ToBeClickable();

        editor.SelectAll();
    }

    [Test]
    public void AllTextSelectedBolded_When_CallSelectAllMethod_And_SendBoldCommand()
    {
        var editor = App.Components.CreateById<CkEditorComponent>("demo").ToBeClickable();

        editor.SelectAll();
        editor.ExecuteCommand(EditorCommands.Bold);
    }

    [Test]
    public void ReturnCorrectText_When_CallGetTextMethodCkEditorComponent()
    {
        var editor = App.Components.CreateById<CkEditorComponent>("demo").ToBeClickable();

        var currentText = editor.GetText();

        StringAssert.Contains("Discover the riches of our editor", currentText);
    }

    [Test]
    public void ReturnCorrectHtml_When_CallGetHtmlMethodCkEditorComponent()
    {
        var editor = App.Components.CreateById<CkEditorComponent>("demo").ToBeClickable();

        var currentText = editor.GetHtml();

        StringAssert.Contains("<p>Read on to better understand the functionalities you can test with this demo.</p>", currentText);
    }
}
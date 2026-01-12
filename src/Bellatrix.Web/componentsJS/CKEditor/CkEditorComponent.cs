using Bellatrix.Web;
using OpenQA.Selenium;

namespace Bellatrix.Web.ComponentsJS.CKEditor;
public class CkEditorComponent : Component
{
    public Component TextArea => Create<Component, FindXpathStrategy>(new FindXpathStrategy(".//div[contains(@class, 'ck-content ck-editor__editable')]"));

    public void ExecuteCommand(EditorCommands command, string arg)
    {
        var commandText = $"arguments[0].ckeditorInstance.commands.get('{command.GetValue()}').execute({arg});";
        JavaScriptService.Execute(commandText, TextArea.WrappedElement);
    }

    public void ExecuteCommand(EditorCommands command)
    {
        var commandText = $"arguments[0].ckeditorInstance.commands.get('{command.GetValue()}').execute();";
        JavaScriptService.Execute(commandText, TextArea.WrappedElement);
    }

    public CkEditorComponent SetText(string text)
    {
        TextArea.WrappedElement.SendKeys(text);
        return this;
    }

    public CkEditorComponent SetText(string[] textAsArray)
    {
        for (int i = 0; i < textAsArray.Length; i++)
        {
            SetText(textAsArray[i]);
            if (i < textAsArray.Length - 1)
            {
                Enter();
            }
        }
        return this;
    }

    public void Clear()
    {
        SetText("");
    }

    public string GetHtml()
    {
        string command = GetEditorInstanceCommand("getData");
        var result = JavaScriptService.Execute($"return {command}", TextArea.WrappedElement);
        return result?.ToString();
    }

    public new string GetText()
    {
        return TextArea.WrappedElement.Text;
    }

    public CkEditorComponent SelectAll()
    {
        TextArea.WrappedElement.SendKeys(Keys.Control + "a");
        return this;
    }

    public void ExecuteAgainstEditorInstance(string command)
    {
        var commandText = $"arguments[0].ckeditorInstance.{command}";
        JavaScriptService.Execute(commandText, WrappedElement);
    }

    public string GetEditorInstanceCommand(string command)
    {
        return $"arguments[0].ckeditorInstance.{command}();";
    }

    public CkEditorComponent Enter()
    {
        TextArea.WrappedElement.SendKeys(Keys.Enter);
        return this;
    }

    public CkEditorComponent MoveCursorInText(int timesOfMovement)
    {
        for (int i = 0; i < timesOfMovement; i++)
        {
            TextArea.WrappedElement.SendKeys(Keys.Control + Keys.ArrowLeft);
        }
        return this;
    }

    private void ClickToolbarButton(ToolbarButton toolbarButton)
    {
        string selector = $"//span[text()='{toolbarButton.GetValue()}']//ancestor::button[contains(@class, 'ck-button')]";
        //var element = WrappedElement.FindElement(By.XPath(selector));
        Button button = this.Create<Button, FindXpathStrategy>(new FindXpathStrategy(selector));
        // wait to be clickable
        button.Click();
    }
}


namespace Bellatrix.Web;
public class KendoColorPicker : Component
{
    private readonly string _colorPickerSetColorExpression = "$('#{0}').data('kendoColorPicker').value('#{1}');";

    public void SetColor(string hexValue)
    {
        string scriptToBeExecuted = string.Format(_colorPickerSetColorExpression, By.Value, hexValue);
        JavaScriptService.Execute(scriptToBeExecuted);
    }
}
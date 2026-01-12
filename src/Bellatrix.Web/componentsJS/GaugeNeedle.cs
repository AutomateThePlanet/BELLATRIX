namespace Bellatrix.Web;
public class GaugeNeedle : Component
{
    public void SetValue(int value)
    {
        string scriptToBeExecuted = string.Format("$('#{0}').igRadialGauge('option', 'value', '{1}');", By.Value, value);
        JavaScriptService.Execute(scriptToBeExecuted);
    }
}
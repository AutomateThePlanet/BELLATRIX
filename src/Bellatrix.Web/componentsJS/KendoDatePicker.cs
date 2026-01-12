using System;

namespace Bellatrix.Web;
public class KendoDatePicker : Component
{
    public void SetDate(DateTime dateTime)
    {
        string scriptToBeExecuted = string.Format("$('#{0}').kendoDatePicker({{ value: new Date({1}, {2}, {3}) }});", By.Value, dateTime.Year, dateTime.Month - 1, dateTime.Day);
        JavaScriptService.Execute(scriptToBeExecuted);
    }
}
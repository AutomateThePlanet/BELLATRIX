using System;

namespace Bellatrix.Web;
public class FullCalendar : Component
{
    private readonly string _fullCalendarMethodJqueryExpression = "$('#{0}').fullCalendar('{1}')";

    // $('#calendar').fullCalendar('next');
    public void ClickNextButton()
    {
        string scriptToBeExecuted = string.Format(_fullCalendarMethodJqueryExpression, By.Value, "next");
        JavaScriptService.Execute(scriptToBeExecuted);
    }

    public void ClickPreviousButton()
    {
        string scriptToBeExecuted = string.Format(_fullCalendarMethodJqueryExpression, By.Value, "prev");
        JavaScriptService.Execute(scriptToBeExecuted);
    }

    public void GoToToday()
    {
        string scriptToBeExecuted = string.Format(_fullCalendarMethodJqueryExpression, By.Value, "today");
        JavaScriptService.Execute(scriptToBeExecuted); ;
    }

    public void GoToDate(DateTime date)
    {
        string scriptToBeExecuted = string.Format("$('#{0}').fullCalendar('gotoDate', $.fullCalendar.moment('{1}-{2}-{3}'))", By.Value, date.Year, date.Month - 1, date.Day);
        JavaScriptService.Execute(scriptToBeExecuted);
    }
}

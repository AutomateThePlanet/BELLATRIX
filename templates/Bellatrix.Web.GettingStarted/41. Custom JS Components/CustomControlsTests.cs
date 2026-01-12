using Bellatrix.Web.NUnit;
using NUnit.Framework;
using System;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class CustomControlsTests : WebTest
{
    [Test]
    public void SetDateKendoDatePickerCustomControl()
    {
        App.Navigation.Navigate("http://demos.telerik.com/kendo-ui/datepicker/index");
        var datePicket = App.Components.CreateById<KendoDatePicker>("datepicker");

        datePicket.SetDate(DateTime.Now);
    }

    [Test]
    public void SetValueGaugeNeedleCustomControl()
    {
        App.Navigation.Navigate("http://www.igniteui.com/radial-gauge/gauge-needle");
        var gaugeNeedle = App.Components.CreateById<GaugeNeedle>("radialgauge");
        gaugeNeedle.SetValue(44);
    }

    [Test]
    public void TestMethodsFullCalendarCustomControl()
    {
        App.Navigation.Navigate("https://fullcalendar.io/docs/v3/month-view-demo");

        var fullCalendar = App.Components.CreateById<FullCalendar>("calendar");
        fullCalendar.ClickNextButton();
        fullCalendar.ClickPreviousButton();
        fullCalendar.GoToDate(new DateTime(2012, 11, 28));
        fullCalendar.GoToToday();
    }

    [Test]
    public void SetColorKendoColorPickerCustomControl()
    {
        App.Navigation.Navigate("http://demos.telerik.com/kendo-ui/colorpicker/index");

        var kendoColorPicker = App.Components.CreateById<KendoColorPicker>("picker");
        kendoColorPicker.SetColor("ccc");
    }
}

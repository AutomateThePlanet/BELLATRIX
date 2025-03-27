using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Parameters;
using System;
using System.Collections.Generic;

namespace Bellatrix.DataGeneration;

public class TestInputComposer
{
    private readonly List<IInputParameter> _parameters = new();

    public static TestInputComposer Start() => new();

    public List<IInputParameter> Build() => _parameters;

    // --- Address ---
    public TestInputComposer AddAddress(Func<ParameterBuilder<AddressDataParameter>, ParameterBuilder<AddressDataParameter>> configure)
    {
        var builder = new ParameterBuilder<AddressDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    // --- Boolean ---
    public TestInputComposer AddBoolean()
    {
        _parameters.Add(new BooleanDataParameter());
        return this;
    }

    // --- Color ---
    public TestInputComposer AddColor(Func<ParameterBuilder<ColorDataParameter>, ParameterBuilder<ColorDataParameter>> configure)
    {
        var builder = new ParameterBuilder<ColorDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    // --- Currency ---
    public TestInputComposer AddCurrency(Func<ParameterBuilder<CurrencyDataParameter>, ParameterBuilder<CurrencyDataParameter>> configure)
    {
        var builder = new ParameterBuilder<CurrencyDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    // --- Date ---
    public TestInputComposer AddDate(Func<ParameterBuilder<DateDataParameter>, ParameterBuilder<DateDataParameter>> configure)
    {
        var builder = new ParameterBuilder<DateDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    public TestInputComposer AddDate(DateTime min, DateTime max)
    {
        _parameters.Add(new DateDataParameter(minBoundary: min, maxBoundary: max));
        return this;
    }

    // --- DateTime ---
    public TestInputComposer AddDateTime(Func<ParameterBuilder<DateTimeDataParameter>, ParameterBuilder<DateTimeDataParameter>> configure)
    {
        var builder = new ParameterBuilder<DateTimeDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    public TestInputComposer AddDateTime(DateTime min, DateTime max)
    {
        _parameters.Add(new DateTimeDataParameter(minBoundary: min, maxBoundary: max));
        return this;
    }

    // --- Email ---
    public TestInputComposer AddEmail(Func<ParameterBuilder<EmailDataParameter>, ParameterBuilder<EmailDataParameter>> configure)
    {
        var builder = new ParameterBuilder<EmailDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    public TestInputComposer AddEmail(int min, int max)
    {
        _parameters.Add(new EmailDataParameter(minBoundary: min, maxBoundary: max));
        return this;
    }

    // --- GeoCoordinate ---
    public TestInputComposer AddGeoCoordinate(Func<ParameterBuilder<GeoCoordinateDataParameter>, ParameterBuilder<GeoCoordinateDataParameter>> configure)
    {
        var builder = new ParameterBuilder<GeoCoordinateDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    // --- Integer ---
    public TestInputComposer AddInteger(Func<ParameterBuilder<IntegerDataParameter>, ParameterBuilder<IntegerDataParameter>> configure)
    {
        var builder = new ParameterBuilder<IntegerDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    public TestInputComposer AddInteger(int min, int max)
    {
        _parameters.Add(new IntegerDataParameter(minBoundary: min, maxBoundary: max));
        return this;
    }

    // --- Month ---
    public TestInputComposer AddMonth(Func<ParameterBuilder<MonthDataParameter>, ParameterBuilder<MonthDataParameter>> configure)
    {
        var builder = new ParameterBuilder<MonthDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    // --- MultiSelect ---
    public TestInputComposer AddMultiSelect(Func<ParameterBuilder<MultiSelectDataParameter>, ParameterBuilder<MultiSelectDataParameter>> configure)
    {
        var builder = new ParameterBuilder<MultiSelectDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    // --- Password ---
    public TestInputComposer AddPassword(Func<ParameterBuilder<PasswordDataParameter>, ParameterBuilder<PasswordDataParameter>> configure)
    {
        var builder = new ParameterBuilder<PasswordDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    public TestInputComposer AddPassword(int min, int max)
    {
        _parameters.Add(new PasswordDataParameter(minBoundary: min, maxBoundary: max));
        return this;
    }

    // --- Percentage ---
    public TestInputComposer AddPercentage(Func<ParameterBuilder<PercentageDataParameter>, ParameterBuilder<PercentageDataParameter>> configure)
    {
        var builder = new ParameterBuilder<PercentageDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    public TestInputComposer AddPercentage(int min, int max)
    {
        _parameters.Add(new PercentageDataParameter(minBoundary: min, maxBoundary: max));
        return this;
    }

    // --- Phone ---
    public TestInputComposer AddPhone(Func<ParameterBuilder<PhoneDataParameter>, ParameterBuilder<PhoneDataParameter>> configure)
    {
        var builder = new ParameterBuilder<PhoneDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    public TestInputComposer AddPhone(int min, int max)
    {
        _parameters.Add(new PhoneDataParameter(minBoundary: min, maxBoundary: max));
        return this;
    }

    // --- SingleSelect ---
    public TestInputComposer AddSingleSelect(Func<ParameterBuilder<SingleSelectDataParameter>, ParameterBuilder<SingleSelectDataParameter>> configure)
    {
        var builder = new ParameterBuilder<SingleSelectDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    // --- Text ---
    public TestInputComposer AddText(Func<ParameterBuilder<TextDataParameter>, ParameterBuilder<TextDataParameter>> configure)
    {
        var builder = new ParameterBuilder<TextDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    public TestInputComposer AddText(int min, int max)
    {
        _parameters.Add(new TextDataParameter(minBoundary: min, maxBoundary: max));
        return this;
    }

    // --- Time ---
    public TestInputComposer AddTime(Func<ParameterBuilder<TimeDataParameter>, ParameterBuilder<TimeDataParameter>> configure)
    {
        var builder = new ParameterBuilder<TimeDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    public TestInputComposer AddTime(TimeSpan min, TimeSpan max)
    {
        _parameters.Add(new TimeDataParameter(minBoundary: min, maxBoundary: max));
        return this;
    }

    // --- Url ---
    public TestInputComposer AddUrl(Func<ParameterBuilder<UrlDataParameter>, ParameterBuilder<UrlDataParameter>> configure)
    {
        var builder = new ParameterBuilder<UrlDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    public TestInputComposer AddUrl(int min, int max)
    {
        _parameters.Add(new UrlDataParameter(minBoundary: min, maxBoundary: max));
        return this;
    }

    // --- Username ---
    public TestInputComposer AddUsername(Func<ParameterBuilder<UsernameDataParameter>, ParameterBuilder<UsernameDataParameter>> configure)
    {
        var builder = new ParameterBuilder<UsernameDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }

    public TestInputComposer AddUsername(int min, int max)
    {
        _parameters.Add(new UsernameDataParameter(minBoundary: min, maxBoundary: max));
        return this;
    }

    // --- Week ---
    public TestInputComposer AddWeek(Func<ParameterBuilder<WeekDataParameter>, ParameterBuilder<WeekDataParameter>> configure)
    {
        var builder = new ParameterBuilder<WeekDataParameter>();
        var configured = configure(builder);
        _parameters.Add(configured.Build());
        return this;
    }
}

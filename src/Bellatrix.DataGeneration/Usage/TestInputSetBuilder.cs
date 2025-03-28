using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.Parameters;

namespace Bellatrix.DataGeneration.Usage;

public class TestInputSetBuilder
{
    private readonly TestInputBuilder _composer = TestInputBuilder.Start();

    public TestInputSetBuilder AddText(Func<ParameterBuilder<TextDataParameter>, ParameterBuilder<TextDataParameter>> configure)
    {
        _composer.AddText(configure);
        return this;
    }

    public TestInputSetBuilder AddEmail(Func<ParameterBuilder<EmailDataParameter>, ParameterBuilder<EmailDataParameter>> configure)
    {
        _composer.AddEmail(configure);
        return this;
    }

    public TestInputSetBuilder AddPhone(Func<ParameterBuilder<PhoneDataParameter>, ParameterBuilder<PhoneDataParameter>> configure)
    {
        _composer.AddPhone(configure);
        return this;
    }

    public TestInputSetBuilder AddBoolean()
    {
        _composer.AddBoolean();
        return this;
    }

    public TestInputSetBuilder AddUsername(Func<ParameterBuilder<UsernameDataParameter>, ParameterBuilder<UsernameDataParameter>> configure)
    {
        _composer.AddUsername(configure);
        return this;
    }

    public TestInputSetBuilder AddPassword(Func<ParameterBuilder<PasswordDataParameter>, ParameterBuilder<PasswordDataParameter>> configure)
    {
        _composer.AddPassword(configure);
        return this;
    }

    public TestInputSetBuilder AddUrl(Func<ParameterBuilder<UrlDataParameter>, ParameterBuilder<UrlDataParameter>> configure)
    {
        _composer.AddUrl(configure);
        return this;
    }

    public TestInputSetBuilder AddColor(Func<ParameterBuilder<ColorDataParameter>, ParameterBuilder<ColorDataParameter>> configure)
    {
        _composer.AddColor(configure);
        return this;
    }

    public TestInputSetBuilder AddCurrency(Func<ParameterBuilder<CurrencyDataParameter>, ParameterBuilder<CurrencyDataParameter>> configure)
    {
        _composer.AddCurrency(configure);
        return this;
    }

    public TestInputSetBuilder AddDate(Func<ParameterBuilder<DateDataParameter>, ParameterBuilder<DateDataParameter>> configure)
    {
        _composer.AddDate(configure);
        return this;
    }

    public TestInputSetBuilder AddInteger(Func<ParameterBuilder<IntegerDataParameter>, ParameterBuilder<IntegerDataParameter>> configure)
    {
        _composer.AddInteger(configure);
        return this;
    }

    public TestInputSetBuilder AddGeoCoordinate(Func<ParameterBuilder<GeoCoordinateDataParameter>, ParameterBuilder<GeoCoordinateDataParameter>> configure)
    {
        _composer.AddGeoCoordinate(configure);
        return this;
    }

    public TestInputSetBuilder AddAddress(Func<ParameterBuilder<AddressDataParameter>, ParameterBuilder<AddressDataParameter>> configure)
    {
        _composer.AddAddress(configure);
        return this;
    }

    public TestInputSetBuilder AddSelect(Func<ParameterBuilder<SingleSelectDataParameter>, ParameterBuilder<SingleSelectDataParameter>> configure)
    {
        _composer.AddSingleSelect(configure);
        return this;
    }

    public List<IInputParameter> Build() => _composer.Build();
}

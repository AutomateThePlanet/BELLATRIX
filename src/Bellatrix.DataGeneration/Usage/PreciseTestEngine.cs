using Bellatrix.DataGeneration.Contracts;

namespace Bellatrix.DataGeneration.Usage;
public partial class PreciseTestEngine
{
    private readonly List<IInputParameter> _parameters;
    private readonly PreciseTestEngineSettings _config;

    private PreciseTestEngine(List<IInputParameter> parameters, PreciseTestEngineSettings config)
    {
        _parameters = parameters;
        _config = config;
    }

    public static TestSuiteBuilder Configure(
        Action<TestInputSetBuilder> parametersConfig,
        Action<PreciseTestEngineSettings> configOverrides = null)
    {
        var composer = new TestInputSetBuilder();
        parametersConfig(composer);

        var config = new PreciseTestEngineSettings();
        configOverrides?.Invoke(config);

        return new TestSuiteBuilder(composer.Build(), config);
    }
}
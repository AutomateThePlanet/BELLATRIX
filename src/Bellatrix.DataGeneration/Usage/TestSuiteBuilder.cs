using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.OutputGenerators;
using Bellatrix.DataGeneration.TestCaseGenerators;

namespace Bellatrix.DataGeneration.Usage;
public class TestSuiteBuilder
    {
        private readonly List<IInputParameter> _parameters;
        private readonly PreciseTestEngineSettings _settings;

        internal TestSuiteBuilder(List<IInputParameter> parameters, PreciseTestEngineSettings config)
        {
            _parameters = parameters;
            _settings = config;
        }

        public List<TestCase> Generate()
        {
            return _settings.Mode switch
            {
                TestGenerationMode.HybridArtificialBeeColony => GenerateUsingABC(_settings.MethodName, _settings.TestCaseCategory),
                TestGenerationMode.Pairwise => GenerateUsingPairwise(_settings.MethodName, _settings.TestCaseCategory),
                _ => GenerateUsingPairwise(_settings.MethodName, _settings.TestCaseCategory)
            };
        }

    private List<TestCase> GenerateUsingPairwise(string methodName, TestCaseCategory category)
    {
        var testCases = PairwiseTestCaseGenerator.GenerateTestCases(_parameters).ToList();

        _settings.OutputGenerator?.GenerateOutput(methodName, testCases, category);

        return testCases;
    }

    private List<TestCase> GenerateUsingABC(string methodName, TestCaseCategory category)
    {
        var abcConfig = _settings.ABCSettings ?? new ABCGenerationSettings();
        var abc = new HybridArtificialBeeColonyTestCaseGenerator(abcConfig);
        return abc.GenerateTestCases(methodName, _parameters, category).ToList();
    }
}
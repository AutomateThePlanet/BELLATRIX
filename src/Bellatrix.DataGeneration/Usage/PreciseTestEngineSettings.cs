using Bellatrix.DataGeneration.OutputGenerators;

namespace Bellatrix.DataGeneration.Usage;

public class PreciseTestEngineSettings
{
    public TestGenerationMode Mode { get; set; } = TestGenerationMode.Pairwise;
    public TestCaseCategory TestCaseCategory { get; set; } = TestCaseCategory.All;
    public string MethodName { get; set; } = "TestMethodName";
    public ITestCaseOutputGenerator OutputGenerator { get; set; } = new NUnitTestCaseSourceOutputGenerator();

    public ABCGenerationSettings ABCSettings { get; set; }
}
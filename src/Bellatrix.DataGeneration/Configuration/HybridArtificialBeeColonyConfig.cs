using Bellatrix.DataGeneration.OutputGenerators;

namespace Bellatrix.DataGeneration;
public class HybridArtificialBeeColonyConfig
{
    public int PopulationSize { get; set; } = 10;
    public int TotalPopulationGenerations { get; set; } = 20;
    public double MutationRate { get; set; } = 0.3;
    public double FinalPopulationSelectionRatio { get; set; } = 0.5;
    public double EliteSelectionRatio { get; set; } = 0.5;
    public double InitialTemperature { get; set; } = 1.0;
    public double CoolingRate { get; set; } = 0.01;
    public double OnlookerSelectionRatio { get; set; } = 0.05;
    public double ScoutSelectionRatio { get; set; } = 0.3;
    public bool DisableOnlookerSelection { get; set; } = true;
    public bool DisableScoutPhase { get; set; } = false;
    public double StagnationThresholdPercentage { get; set; } = 0.75;
    public bool AllowMultipleInvalidInputs { get; set; } = false;
    public ITestCaseOutputGenerator OutputGenerator { get; set; } = new NUnitTestCaseOutputGenerator();

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + PopulationSize.GetHashCode();
            hash = hash * 31 + TotalPopulationGenerations.GetHashCode();
            hash = hash * 31 + MutationRate.GetHashCode();
            hash = hash * 31 + FinalPopulationSelectionRatio.GetHashCode();
            hash = hash * 31 + EliteSelectionRatio.GetHashCode();
            hash = hash * 31 + InitialTemperature.GetHashCode();
            hash = hash * 31 + CoolingRate.GetHashCode();
            hash = hash * 31 + OnlookerSelectionRatio.GetHashCode();
            hash = hash * 31 + ScoutSelectionRatio.GetHashCode();
            hash = hash * 31 + DisableOnlookerSelection.GetHashCode();
            hash = hash * 31 + DisableScoutPhase.GetHashCode();
            hash = hash * 31 + StagnationThresholdPercentage.GetHashCode();
            hash = hash * 31 + AllowMultipleInvalidInputs.GetHashCode();
            hash = hash * 31 + (OutputGenerator?.GetType().GetHashCode() ?? 0); // Using type hash for OutputGenerator
            return hash;
        }
    }
}

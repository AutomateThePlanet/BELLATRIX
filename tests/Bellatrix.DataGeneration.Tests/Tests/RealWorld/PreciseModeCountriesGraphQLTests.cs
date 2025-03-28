using RestSharp;
using System.Collections.Generic;
using Bellatrix.DataGeneration.Contracts;
using Bellatrix.DataGeneration.OutputGenerators;
using System;
using Bellatrix.DataGeneration.Usage;

namespace Bellatrix.DataGeneration.Tests.Tests.RealWorld;

[TestFixture]
public class PreciseModeCountriesGraphQLTests
{
    [Test]
    public void GenerateTests() =>
         PreciseTestEngine
            .Configure(
                parameters => parameters
                    .AddSelect(s => s
                        .Valid("US")
                        .Valid("BG")
                        .Valid("FR")
                        .Invalid("XX").WithExpectedMessage("Country code is invalid")
                        .Invalid("U1").WithExpectedMessage("Country code must contain only letters")
                        .Invalid("").WithExpectedMessage("Country code is required"))
                    .AddSelect(s => s
                        .Valid("en")
                        .Valid("fr")
                        .Valid("de")
                        .Invalid("zz").WithExpectedMessage("Language code not supported")
                        .Invalid("123").WithExpectedMessage("Language code must be alphabetic"))
                    .AddSelect(s => s
                        .Valid("EU")
                        .Valid("AF")
                        .Valid("AS")
                        .Invalid("999").WithExpectedMessage("Continent code cannot be numeric")
                        .Invalid("X").WithExpectedMessage("Continent code too short")
                        .Invalid("").WithExpectedMessage("Continent code is required")),
                settings =>
                {
                    settings.Mode = TestGenerationMode.HybridArtificialBeeColony;

                    settings.ABCSettings = new ABCGenerationSettings
                    {
                        TotalPopulationGenerations = 20,
                        MutationRate = 0.3,
                        FinalPopulationSelectionRatio = 0.5,
                        EliteSelectionRatio = 0.5,
                        OnlookerSelectionRatio = 0.1,
                        ScoutSelectionRatio = 0.3,
                        EnableOnlookerSelection = true,
                        EnableScoutPhase = false,
                        EnforceMutationUniqueness = true,
                        StagnationThresholdPercentage = 0.75,
                        CoolingRate = 0.95,
                        AllowMultipleInvalidInputs = false,
                        OutputGenerator = new NUnitTestCaseAttributeOutputGenerator()
                    };
                })
            .Generate();
}

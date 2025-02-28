namespace Bellatrix.Web.Tests.MetaheuristicVersion2.Tests
{
    public class ABCParameterSet
    {
        public double SelectionRatio { get; }
        public double EliteSelectionRatio { get; }
        public int MaxIterations { get; }
        public double MutationRate { get; }

        public ABCParameterSet(double selectionRatio, double eliteSelectionRatio, int maxIterations, double mutationRate)
        {
            SelectionRatio = selectionRatio;
            EliteSelectionRatio = eliteSelectionRatio;
            MaxIterations = maxIterations;
            MutationRate = mutationRate;
        }

        public override string ToString() =>
            $"SelectionRatio={SelectionRatio}, ElitSelectionRatio={EliteSelectionRatio}, MaxIterations={MaxIterations}, MutationRate={MutationRate}";
    }
}
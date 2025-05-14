namespace Bellatrix.LLM.cache;
public class SmartTestExecutionEntry
{
    public int Id { get; set; }
    public string TestFullName { get; set; }
    public DateTime ExecutionTime { get; set; }
    public string BddLog { get; set; }
    public string PageSummaryJson { get; set; }
    public string Project { get; set; }
}

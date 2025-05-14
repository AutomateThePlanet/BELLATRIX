using Microsoft.SemanticKernel;

namespace Bellatrix.Web.LLM.Plugins;
public class LocatorMapperSkill
{
    [KernelFunction]
    public string MatchPromptToKnownLocator(string pageSummary, string instruction)
    {
        return $"""
You are an AI assistant that maps user instructions to known UI locators.

User wants to: {instruction}

From the list of known locators below, pick the one that best matches and return its exact locator in the format:
locator_strategy=locator_value

Known Locators:
{pageSummary}

Examples:
For login button → return xpath=//button[@id='login']
For search input → return id=input-search

Return ONLY one valid locator in that format. If no match is found, return Unknown.
""";
    }


}

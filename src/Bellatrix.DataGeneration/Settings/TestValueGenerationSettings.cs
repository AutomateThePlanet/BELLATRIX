namespace Bellatrix.DataGeneration.Configuration;
public class TestValueGenerationSettings
{
    public ABCGenerationSettings ABCGenerationConfig { get; set; }
    /// <summary>
    /// Supported locale codes for Bogus:
    /// 
    /// "af_ZA" - Afrikaans (South Africa)
    /// "az"     - Azerbaijani
    /// "cz"     - Czech
    /// "de"     - German
    /// "de_AT"  - German (Austria)
    /// "de_CH"  - German (Switzerland)
    /// "el"     - Greek
    /// "en"     - English (default)
    /// "en_AU"  - English (Australia)
    /// "en_BORK" - Bork (fun/novelty)
    /// "en_CA"  - English (Canada)
    /// "en_GB"  - English (Great Britain)
    /// "en_IE"  - English (Ireland)
    /// "en_IND" - English (India)
    /// "en_US"  - English (United States)
    /// "en_ZA"  - English (South Africa)
    /// "es"     - Spanish
    /// "fa"     - Persian (Farsi)
    /// "fi"     - Finnish
    /// "fr"     - French
    /// "fr_CA"  - French (Canada)
    /// "fr_CH"  - French (Switzerland)
    /// "ge"     - Georgian
    /// "hr"     - Croatian
    /// "hu"     - Hungarian
    /// "hy"     - Armenian
    /// "id_ID"  - Indonesian
    /// "it"     - Italian
    /// "ja"     - Japanese
    /// "ko"     - Korean
    /// "lv"     - Latvian
    /// "mk"     - Macedonian
    /// "nb_NO"  - Norwegian (Bokmål)
    /// "ne"     - Nepali
    /// "nl"     - Dutch
    /// "pl"     - Polish
    /// "pt_BR"  - Portuguese (Brazil)
    /// "pt_PT"  - Portuguese (Portugal)
    /// "ro"     - Romanian
    /// "ru"     - Russian
    /// "sk"     - Slovak
    /// "sl"     - Slovenian
    /// "sr_RS"  - Serbian
    /// "sv"     - Swedish
    /// "tr"     - Turkish
    /// "uk"     - Ukrainian
    /// "uz"     - Uzbek
    /// "vi"     - Vietnamese
    /// "zh_CN"  - Chinese (Simplified)
    /// "zh_TW"  - Chinese (Traditional)
    /// </summary>
    public string Locale { get; set; } = "en";
    public int Seed { get; set; } = 12345;
    public bool IncludeBoundaryValues { get; set; } = true;
    public bool AllowValidEquivalenceClasses { get; set; } = false;
    public bool AllowInvalidEquivalenceClasses { get; set; } = false;
    public Dictionary<string, InputTypeSettings> InputTypeSettings { get; set; } = new();
}
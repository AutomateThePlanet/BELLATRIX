namespace Bellatrix.DataGeneration;

public static class TestStringExtensions
{
    /// <summary>
    /// Trims the string to the specified maximum length if it exceeds it.
    /// </summary>
    public static string EnsureMaxLength(this string input, int maxLength)
    {
        if (input == null) return string.Empty;
        return input.Length <= maxLength ? input : input.Substring(0, maxLength);
    }

    /// <summary>
    /// Ensures the string is at least the specified minimum length by padding with a given character.
    /// </summary>
    public static string EnsureMinLength(this string input, int minLength, char paddingChar = 'A')
    {
        if (input == null) input = string.Empty;
        return input.Length >= minLength
            ? input
            : input + new string(paddingChar, minLength - input.Length);
    }

    /// <summary>
    /// Ensures the string is within a min and max range. Trims or pads accordingly.
    /// </summary>
    public static string EnsureRangeLength(this string input, int minLength, int maxLength, char paddingChar = 'A')
    {
        return input
            .EnsureMaxLength(maxLength)
            .EnsureMinLength(minLength, paddingChar);
    }
}


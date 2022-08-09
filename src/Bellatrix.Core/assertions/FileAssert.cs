using Bellatrix.Assertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellatrix.Assertions;

public static class FileAssert
{/// <summary>
 ///  Assert text file(.txt).
 /// </summary>
 /// <param name="expectedFile">Text File which content is expected.</param>
 /// <param name="actualFileContent">Actual file content.</param>
    public static void AssertTextFileContent(FileInfo expectedFile, string actualFileContent)
    {
        var expectedTextFileLines = File.ReadAllLines(expectedFile.FullName);
        var actualFileContentLines = actualFileContent.Split("\r\n", StringSplitOptions.None);
        var diffrences = GetDifrences(expectedTextFileLines, actualFileContentLines);

        if (diffrences.Any())
        {
            Assert.Fail($"File content is not the same. Differences:/n{string.Join(Environment.NewLine, diffrences)}");
        }
    }

    /// <summary>
    /// Assert 2 text files with extensions .txt, .csv, .docx, .odt. The two files should have same extensions.
    /// </summary>
    /// <param name="expectedFile">Expected file.</param>
    /// <param name="actualFile">Actual file.</param>
    public static void AssertTextFiles(FileInfo expectedFile, FileInfo actualFile)
    {
        Assert.AreEqual(expectedFile.Extension, actualFile.Extension, $"Files are different. Expected file to be {expectedFile.Extension}, but was {actualFile.Extension}");

        var expectedTextFileLines = File.ReadAllLines(expectedFile.FullName);
        var actualFileContentLines = File.ReadAllLines(actualFile.FullName);
        var diffrences = GetDifrences(expectedTextFileLines, actualFileContentLines);

        if (diffrences.Any())
        {
            Assert.Fail($"Downloaded File is not the same. Differences:/n{string.Join(Environment.NewLine, diffrences)}");
        }
    }

    private static List<string> GetDifrences(string[] expectedTextFileLines, string[] actualFileContentLines)
    {
        var diffrences = new List<string>();
        if (expectedTextFileLines.Length == actualFileContentLines.Length)
        {
            for (int i = 0; i < expectedTextFileLines.Length; i++)
            {
                var areEqual = string.Compare(expectedTextFileLines[i], actualFileContentLines[i]);
                if (areEqual != 0)
                {
                    diffrences.Add($"Line {i}: expected {expectedTextFileLines[i]}, but was {actualFileContentLines[i]}");
                }
            }
        }
        else
        {
            diffrences.Add($"Line Count differ. Expected {expectedTextFileLines.Length}, but was {actualFileContentLines.Length}");
        }

        return diffrences;
    }
}
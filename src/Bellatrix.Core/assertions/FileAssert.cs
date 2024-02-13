using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bellatrix.Utilities;
using Newtonsoft.Json;

namespace Bellatrix.Assertions;

public static class FileAssert
{/// <summary>
 ///  Assert text file(.txt).
 /// </summary>
 /// <param name="expectedFile">Text File which content is expected.</param>
 /// <param name="actualFileContent">Actual file content.</param>
    public static void AssertTextFileContent(FileInfo expectedFile, string actualFileContent, bool removeEmptyLines = false)
    {
        var stringSplitOptions = removeEmptyLines ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
        {
            var expectedTextFileLines = File.ReadAllLines(expectedFile.FullName);
            var actualFileContentLines = actualFileContent.Split("\r\n", stringSplitOptions);
            var differences = GetDifferences(expectedTextFileLines, actualFileContentLines);

            if (differences.Any())
            {
                Assert.Fail($"File content is not the same. Differences:/n{string.Join(Environment.NewLine, differences)}");
            }
        }
    }

    /// <summary>
    /// Assert 2 text files with extensions .txt, .csv, .docx, .odt. The two files should have same extensions.
    /// </summary>
    /// <param name="expectedFile">Expected file.</param>
    /// <param name="actualFile">Actual file.</param>
    public static void AssertTextFiles(FileInfo expectedFile, FileInfo actualFile, bool removeEmptyLines = false)
    {
        Assert.AreEqual(expectedFile.Extension, actualFile.Extension, $"Files are different. Expected file to be {expectedFile.Extension}, but was {actualFile.Extension}");

        var actualFileContent = File.ReadAllText(actualFile.FullName);

        AssertTextFileContent(expectedFile, actualFileContent, removeEmptyLines);
    }

    public static void AssertJson<TModel>(string jsonfilePath, List<TModel> expectedEntities)
         where TModel : class, new()
    {
        var json = File.ReadAllText(jsonfilePath);

        var actualEntities = typeof(TModel) != typeof(object)
            ? JsonConvert.DeserializeObject<List<TModel>>(json)
            : (IList)JsonConvert.DeserializeObject(json, typeof(IList<>).MakeGenericType(expectedEntities.First().GetType()));

        Assert.AreEqual(expectedEntities.Count, actualEntities.Count);
        for (int i = 0; i < expectedEntities.Count; i++)
        {
            EntitiesAsserter.AreEqual(expectedEntities[i], actualEntities[i]);
        }
    }

    public static void AssertCsv<TModel>(string actualFile, List<TModel> expectedEntities)
        where TModel : class, new()
    {
        CsvService.ValidateData(actualFile, expectedEntities);
    }

    public static void AssertExcel<TModel>(string actualFile, List<TModel> expectedEntities)
        where TModel : class, new()
    {
        ExcelService.ValidateData(actualFile, expectedEntities);
    }

    private static List<string> GetDifferences(string[] expectedTextFileLines, string[] actualFileContentLines)
    {
        var differences = new List<string>();
        if (expectedTextFileLines.Length == actualFileContentLines.Length)
        {
            for (int i = 0; i < expectedTextFileLines.Length; i++)
            {
                var areEqual = string.Compare(expectedTextFileLines[i], actualFileContentLines[i]);
                if (areEqual != 0)
                {
                    differences.Add($"Line {i}: expected {expectedTextFileLines[i]}, but was {actualFileContentLines[i]}");
                }
            }
        }
        else
        {
            differences.Add($"Line Count differ. Expected {expectedTextFileLines.Length}, but was {actualFileContentLines.Length}");
        }

        return differences;
    }
}
using Bellatrix.Assertions;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Bellatrix.Utilities;
public static class CSVService
{
    public static void ValidateData<TEntity>(string localFile, List<TEntity> expectedEntities)
    {
        var actualEntities = ReadAllLines<TEntity>(localFile);
        Assert.AreEqual(expectedEntities.Count, expectedEntities.Count);
        int i = 0;
        foreach (var actualEntity in actualEntities)
        {
            Assert.AreEqual(expectedEntities[i++], actualEntity);
        }
    }

    public static List<TEntity> ReadAllLines<TEntity>(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<TEntity>();
        return records.ToList();
    }
}
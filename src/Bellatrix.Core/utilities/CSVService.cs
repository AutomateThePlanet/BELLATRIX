using System;
using System.Collections;
using Bellatrix.Assertions;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Bellatrix.Utilities;

public static class CsvService
{
    public static void ValidateData<TEntity>(string localFile, List<TEntity> expectedEntities)
        where TEntity : class, new()
    {
        var actualEntities = typeof(TEntity) != typeof(object)
            ? ReadAllLines<TEntity>(localFile)
            : ReadAllLines(expectedEntities.First().GetType(), localFile);

        Assert.AreEqual(expectedEntities.Count, actualEntities.Count);
        for (int i = 0; i < expectedEntities.Count; i++)
        {
            EntitiesAsserter.AreEqual(expectedEntities[i], actualEntities![i]);
        }
    }

    public static List<TEntity> ReadAllLines<TEntity>(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<TEntity>();
        return records.ToList();
    }

    private static IList ReadAllLines(Type entityType, string filePath)
    {
        const string methodName = nameof(ReadAllLines);
        var method = typeof(CsvService).GetMethod(methodName);
        var genericMethod = method!.MakeGenericMethod(entityType);

        return (IList)genericMethod.Invoke(null, new object[] { filePath });
    }
}
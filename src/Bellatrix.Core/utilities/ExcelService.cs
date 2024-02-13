using Bellatrix.Assertions;
using CsvHelper;
using CsvHelper.Excel;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.Utilities;
public static class ExcelService
{
    public static void ValidateData<TEntity>(string localFile, List<TEntity> expectedEntities)
        where TEntity : class, new()
    {
        var actualEntities = typeof(TEntity) != typeof(object)
            ? MapToModel<TEntity>(localFile)
            : MapToModel(expectedEntities.First().GetType(), localFile);

        Assert.AreEqual(expectedEntities.Count, expectedEntities.Count);
        for (int i = 0; i < expectedEntities.Count; i++)
        {
            EntitiesAsserter.AreEqual(actualEntities[i], expectedEntities[i]);
        }
    }

    public static List<TEntity> MapToModel<TEntity>(string filePath)
         where TEntity : class, new()
    {
        using var reader = new CsvReader(new ExcelParser(filePath));
        var records = reader.GetRecords<TEntity>();

        return records.ToList();
    }

    private static IList MapToModel(Type entityType, string filePath)
    {
        const string methodName = nameof(MapToModel);
        var method = typeof(ExcelService).GetMethod(methodName);
        var genericMethod = method!.MakeGenericMethod(entityType);

        return (IList)genericMethod.Invoke(null, new object[] { filePath });
    }
}
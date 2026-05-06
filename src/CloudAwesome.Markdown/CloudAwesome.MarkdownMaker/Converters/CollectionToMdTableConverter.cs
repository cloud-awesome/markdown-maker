using System.Globalization;
using System.Reflection;

namespace CloudAwesome.MarkdownMaker.Converters;

public static class CollectionToMdTableConverter 
{
    public static MdTable ToMdTable<T>(this IEnumerable<T>? collection, bool ignorePropertiesWithOnlyNullValues = true)
    {
        var result = new MdTable();
        
        if (collection == null)
        {
            return result;
        }
        
        var items = collection.ToList();
        if (items.Count == 0)
        {
            return result;
        }

        var properties = typeof(T)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.CanRead)
            .ToArray();

        if (ignorePropertiesWithOnlyNullValues)
        {
            properties = properties
                .Where(p => items.Any(item => p.GetValue(item) is not null))
                .ToArray();
        }

        if (properties.Length == 0)
        {
            return result;
        }

        result.AddColumns(properties.Select(p => p.Name).ToArray());

        foreach (var item in items)
        {
            var rowCells = properties
                .Select(p => ConvertToString(p.GetValue(item)))
                .ToArray();

            result.AddRowCells(rowCells);
        }

        return result;
    }
    
    private static string ConvertToString(object? value)
    {
        if (value is null)
        {
            return "(empty)";
        }

        return value switch
        {
            double d => d.ToString("0.00", CultureInfo.InvariantCulture),
            //double? d => d?.ToString("0.00", CultureInfo.InvariantCulture) ?? string.Empty,
            float f => f.ToString("0.00", CultureInfo.InvariantCulture),
            //float? f => f?.ToString("0.00", CultureInfo.InvariantCulture) ?? string.Empty,
            DateTime dt => dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            DateOnly d => d.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
            _ => value.ToString() ?? "(empty)"
        };
    }
}
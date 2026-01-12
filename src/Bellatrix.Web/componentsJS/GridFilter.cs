namespace Bellatrix.Web;

public class GridFilter
{
    public GridFilter(string columnName, FilterOperator filterOperator, string filterValue)
    {
        ColumnName = columnName;
        FilterOperator = filterOperator;
        FilterValue = filterValue;
    }

    public string ColumnName { get; set; }

    public FilterOperator FilterOperator { get; set; }

    public string FilterValue { get; set; }
}

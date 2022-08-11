using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellatrix.Web;

/// <summary>
/// An Attribute used for mapping the exact column from the grid model and according to this exact column, the row will be selected clicking on the cell from this column.
/// </summary>
public class SelectColumnAttribute : Attribute
{
    public SelectColumnAttribute()
    {
    }
}

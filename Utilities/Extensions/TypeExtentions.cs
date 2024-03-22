using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Utilities.Extensions;

public static class TypeExtentions
{
    public static string GetTableName(this Type type)
    {
        return type.GetCustomAttribute<TableAttribute>()?.Name;
    }
}

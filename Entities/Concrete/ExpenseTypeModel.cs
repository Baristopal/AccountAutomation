using Entities.Abstract;
using Library.Entities.Attributes;

namespace Entities.Concrete;
[NoSqlConfig("Data", "ExpenseTypes")]
public class ExpenseTypeModel : BaseEntity
{
    public string Name { get; set; }
    public bool IsStcoked { get; set; } = false;
}

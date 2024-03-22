using Dapper.Contrib.Extensions;
using Entities.Abstract;

namespace Entities.Concrete;
[Table("ExpenseType")]
public class ExpenseTypeModel : BaseEntity
{
    public string Name { get; set; }
    public bool IsStocked { get; set; } = false;
    public bool IsShowInExpenseListPage { get; set; } = false;
}

using Entities.Abstract;
using Library.Entities.Attributes;

namespace Entities.Concrete;
[NoSqlConfig("AccountAutomation","ProcessTypes")]
public class ProcessTypeModel : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
}

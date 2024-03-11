using Entities.Abstract;
using Library.Entities.Attributes;

namespace Entities.Concrete;
[NoSqlConfig("Data", "Check")]
public class CheckModel : BaseEntity
{
    public string BankName { get; set; }
    public string CheckNumber { get; set; }
    public string CompanyName { get; set; }
    public DateTime? MaturityDate { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
    public string ProcessType { get; set; }
    public DateTime? ProcessDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string Status { get; set; }
}

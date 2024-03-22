using Dapper.Contrib.Extensions;
using Entities.Abstract;

namespace Entities.Concrete;

[Table("Instant")]
public class InstantModel : BaseEntity
{
    public decimal TLEncumbrance { get; set; }
    public decimal TLReceivable { get; set; }
    public decimal TLBalance { get; set; }
    public decimal CurrencyEncumbrance { get; set; }
    public decimal CurrencyReceivable { get; set; }
    public decimal CurrencyBalance { get; set; }
    public int InstantId { get; set; }
    public string CompanyName { get; set; }
    public string CompanyOfficer { get; set; }
    public string CompanyAddress { get; set; }
    public string TaxHouse { get; set; }
    public string TaxNumber { get; set; }
    public string LandPhoneNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
}

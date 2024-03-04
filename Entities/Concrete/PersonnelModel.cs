using Entities.Abstract;
using Library.Entities.Attributes;

namespace Entities.Concrete;
[NoSqlConfig("Data", "Personnel")]
public class PersonnelModel :BaseEntity
{
    public int StaffId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string IdentityNumber { get; set; }
    public string Iban { get; set; }

}

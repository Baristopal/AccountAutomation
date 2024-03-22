using Dapper.Contrib.Extensions;
using Entities.Abstract;

namespace Entities.Concrete;
[Table("Personnel")]
public class PersonnelModel : BaseEntity
{
    public int StaffId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string IdentityNumber { get; set; }
    public string Iban { get; set; }

}

using Entities.Abstract;
using Library.Entities.Attributes;

namespace Entities.Concrete;
[NoSqlConfig("Data","Company")]
public class CompanyModel : BaseEntity
{
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
}

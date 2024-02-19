using Entities.Abstract;

namespace Entities.Concrete;
public class CompanyModel : BaseEntity
{
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
}

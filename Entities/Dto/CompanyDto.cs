using Dapper.Contrib.Extensions;
using Entities.Abstract;

namespace Entities.Dto;
[Table("Company")]
public class CompanyDto : BaseEntity, IDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
}

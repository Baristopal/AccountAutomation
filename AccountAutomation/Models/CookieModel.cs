namespace Presentation.Web.SuperAdmin.Models;

public class CookieModel
{
    public string Email { get; set; }
    public string Name { get; set; }
    public DateTime Expiration { get; set; }
    public int CompanyId { get; set; }
    public string Role { get; set; }
}

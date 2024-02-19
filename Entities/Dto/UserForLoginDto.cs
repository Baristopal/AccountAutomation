using Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Entities.Dto;
public class UserForLoginDto : IDto
{
    [Required]
    [EmailAddress(ErrorMessage = "Lütfen e-posta adresinizi kontrol ediniz.")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(3, ErrorMessage = "Şifreniz en az 3 karakter olmalıdır.")]
    public string Password { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Business.Enums;
public enum PaidStatusEnum
{
    [Display(Name = "Ödendi")]
    PAID,
    [Display(Name = "Kısmi Ödendi")]
    PARTIALLY_PAID,
    [Display(Name = "Ödenmedi")]
    NOT_PAID
}

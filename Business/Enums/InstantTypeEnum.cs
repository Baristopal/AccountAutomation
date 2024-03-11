using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Enums;
public enum InstantTypeEnum
{
    [Display(Name ="Alıcı")]
    BUYER,
    [Display(Name ="Satıcı")]
    SELLER,
    [Display(Name ="Masraf")]
    EXPENSE,
}

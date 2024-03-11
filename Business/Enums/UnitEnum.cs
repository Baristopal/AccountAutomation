using System.ComponentModel.DataAnnotations;

namespace Business.Enums;
public enum UnitEnum
{
    [Display(Name = "Kilogram")]
    KG,
    [Display(Name = "Metre")]
    METER,
    [Display(Name = "Koli")]
    PACKAGE,
    [Display(Name = "Adet")]
    PIECE 
}

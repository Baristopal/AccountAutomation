using System.ComponentModel.DataAnnotations;

namespace Business.Enums;
public enum PurchaseTypeEnum
{
    [Display(Name = "TL Nakit")]
    TL_CASH,
    [Display(Name = "USD Nakit")]
    USD_CASH,
    [Display(Name = "EUR Nakit")]
    EUR_CASH,
    [Display(Name = "Banka")]
    BANK_TRANSFER,
    [Display(Name = "Kredi Kartı")]
    CREDIT_CARD,
    [Display(Name = "Cari")]
    INSTANT,
    [Display(Name = "Çek/Senet")]
    CHECK,
}

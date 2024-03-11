using System.ComponentModel.DataAnnotations;

namespace Business.Enums;
public enum ProcessTypeEnum
{
    [Display(Name = "Alış")]
    BUY,
    [Display(Name = "Satış")]
    SELL,
    [Display(Name = "Tahsilat")]
    COLLECT,
    [Display(Name = "Ödeme")]
    PAY,
    [Display(Name = "Alış İade")]
    PURCHASE_REFUND,
    [Display(Name = "Satış İade")]
    SALE_REFUND,
    [Display(Name = "Üretime Giden")]
    PRODUCTION,
    [Display(Name = "Fire")]
    WASTE,
    [Display(Name = "İrsaliye")]
    WAYBILL,
}

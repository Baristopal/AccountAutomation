using Dapper.Contrib.Extensions;
using Entities.Abstract;

namespace Entities.Concrete;
[Table("ProductTracking")]
public class ProductTrackingModel : BaseEntity
{
    public string ProcessType { get; set; }
    public DateTime? ProcessDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public string InvoiceNumber { get; set; }
    public string CompanyName { get; set; }
    public string Description { get; set; }
    public string StockName { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; }
    public decimal TaxAmount { get; set; }
    public string Currency { get; set; }
    public decimal CurrencyTotalAmount { get; set; }
    public decimal TLTotalAmount { get; set; }
    public string Process { get; set; }
    public int EstimatedQuantity { get; set; }
    public decimal Grammage { get; set; }
    public int ActualQuantity { get; set; }
    public int WasteQuantity { get; set; }
    public DateTime? DicontinuationDate { get; set; }
    public int DelayTime { get; set; }
    public int TimeInProduction { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalCost { get; set; }


}

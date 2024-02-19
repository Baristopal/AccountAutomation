namespace Entities.Dto;
public class DataEntryDto
{
    public string ExpenseType { get; set; }
    public int Stock { get; set; }
    public string Unit { get; set; }
    public decimal TaxAmount { get; set; }
    public string Currency { get; set; }
    public decimal CurrencyTotalAmount { get; set; }
    public decimal TLTotalAmount { get; set; }
}

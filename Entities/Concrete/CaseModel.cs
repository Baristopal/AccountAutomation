namespace Entities.Concrete;
public class CaseModel
{
    public DateTime ProcessDate { get; set; }
    public string Currency { get; set; }
    public string Description { get; set; }
    public string SalesType { get; set; }
    public string ExpenseType { get; set; }
    public string ProcessType { get; set; }
    public decimal TLTotalAmount { get; set; }
    public decimal CurrencyTotalAmount { get; set; }

}

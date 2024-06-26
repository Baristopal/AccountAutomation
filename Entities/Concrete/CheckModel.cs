﻿using Dapper.Contrib.Extensions;
using Entities.Abstract;

namespace Entities.Concrete;
[Table("Checks")]
public class CheckModel : BaseEntity
{
    public string BankName { get; set; }
    public string CheckNumber { get; set; }
    public string CompanyName { get; set; }
    public DateTime? MaturityDate { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
    public string ProcessType { get; set; }
    public DateTime? ProcessDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string Department { get; set; }
    public string AccountNumber { get; set; }
    public string Status { get; set; }
}

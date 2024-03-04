using Entities.Abstract;
using Library.Entities.Attributes;

namespace Entities.Concrete;
[NoSqlConfig("Data", "Payroll")]
public class PayrollModel : BaseEntity
{
    public int StaffId { get; set; }
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IncentiveNumber { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Iban { get; set; }
    public decimal PayrollGrossWage { get; set; }
    public decimal PayrollNetSalary { get; set; }
    public int WorkingDays { get; set; }
    public decimal NetSalary { get; set; }
    public decimal BankAdvancePayment { get; set; }
    public decimal CashAdvancePayment { get; set; }

}


public class PersonnelShiftHour
{
    public int StaffId { get; set; }
    public DateTime? WorkDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public int ShiftDuration { get; set; }
    public int HolidayShiftDuration { get; set; }
    public int AuditShiftDuration { get; set; }
    public int TotalShiftDuration { get; set; }
}

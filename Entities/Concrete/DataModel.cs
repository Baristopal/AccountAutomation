﻿using Dapper.Contrib.Extensions;
using Entities.Abstract;

namespace Entities.Concrete;

[Table("Data")]
public class DataModel : BaseEntity, IEntity
{
    public string ProcessType { get; set; } // İşlem Türü 
    public DateTime? ProcessDate { get; set; } // İşlem Tarihi
    public DateTime? MaturityDate { get; set; } // Vade Tarihi
    public string SalesType { get; set; } // Satış Türü
    public string InvoiceNumber { get; set; } // Fatura Numarası
    public string Description { get; set; } // Açıklama
    public string ExpenseType { get; set; } // Gider Türü
    public bool IsStocked { get; set; } // ticari mal mı
    public int InstantId { get; set; }
    public string InstantName { get; set; }
    public int Stock { get; set; } // Stok
    public string Unit { get; set; } // Birim
    public decimal TaxAmount { get; set; } // Vergi Tutarı
    public string Currency { get; set; } // Para Birimi
    public decimal USDExchange { get; set; } // USD Kuru
    public decimal EURExchange { get; set; } // EUR Kuru
    public decimal CurrencyTotalAmount { get; set; } // Para Birimi Toplam Tutar
    public decimal TLTotalAmount { get; set; } // TL Toplam Tutar
    public decimal PaidAmount { get; set; }
    public string Status { get; set; }
    public string OrderNumber { get; set; }

}

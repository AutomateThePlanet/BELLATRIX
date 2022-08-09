using System.Collections.Generic;

namespace Bellatrix.API.MSTest.Tests.Models;

public class Invoices
{
    public Invoices() => InvoiceItems = new HashSet<InvoiceItems>();

    public long InvoiceId { get; set; }
    public long CustomerId { get; set; }
    public string InvoiceDate { get; set; }
    public string BillingAddress { get; set; }
    public string BillingCity { get; set; }
    public string BillingState { get; set; }
    public string BillingCountry { get; set; }
    public string BillingPostalCode { get; set; }
    public string Total { get; set; }

    public Customers Customer { get; set; }
    public ICollection<InvoiceItems> InvoiceItems { get; set; }
}

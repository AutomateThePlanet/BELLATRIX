namespace Bellatrix.API.MSTest.Tests.Models;

public class InvoiceItems
{
    public long InvoiceLineId { get; set; }
    public long InvoiceId { get; set; }
    public long TrackId { get; set; }
    public string UnitPrice { get; set; }
    public long Quantity { get; set; }

    public Invoices Invoice { get; set; }
    public Tracks Track { get; set; }
}

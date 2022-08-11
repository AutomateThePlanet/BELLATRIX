namespace Bellatrix.Web.Tests;

public class DataTableExampleOne
{
    [HeaderName("Last Name")]
    public string LastName { get; set; }
    [HeaderName("First Name")]
    public string FirstName { get; set; }
    [HeaderName("Email")]
    public string Email { get; set; }
    [HeaderName("Due")]
    public string Due { get; set; }
    [HeaderName("Web Site")]
    public string WebSite { get; set; }
}
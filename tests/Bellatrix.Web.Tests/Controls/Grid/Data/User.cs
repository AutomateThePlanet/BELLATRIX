namespace Bellatrix.Web.Tests.Controls;

public class User
{
    [HeaderName("Last Name")]
    public string LastName { get; set; }
    [HeaderName("First Name")]
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string Due { get; set; }
    [HeaderName("Web Site")]
    public string WebSite { get; set; }
}
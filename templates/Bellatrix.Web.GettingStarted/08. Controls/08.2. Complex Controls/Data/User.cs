namespace Bellatrix.Web.GettingStarted;

// In order GetItems to be able to work you need to map the properties to headers through the HeaderName attribute
// this is how we handle differences between the property name, spaces in the headers and such.
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
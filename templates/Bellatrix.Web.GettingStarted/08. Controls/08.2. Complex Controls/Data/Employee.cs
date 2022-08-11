namespace Bellatrix.Web.GettingStarted;

// In order GetItems to be able to work you need to map the properties to headers through the HeaderName attribute
// this is how we handle differences between the property name, spaces in the headers and such.
public class Employee
{
    [HeaderName("Order")]
    public string Order { get; set; }

    [HeaderName("Firstname")]
    public string FirstName { get; set; }

    [HeaderName("Lastname")]
    public string LastName { get; set; }

    [HeaderName("Email Business")]
    public string BusinessEmail { get; set; }

    [HeaderName("Email Personal")]
    public string PersonalEmail { get; set; }
}

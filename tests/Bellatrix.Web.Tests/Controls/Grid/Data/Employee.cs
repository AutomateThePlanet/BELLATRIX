namespace Bellatrix.Web.Tests.Controls;

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

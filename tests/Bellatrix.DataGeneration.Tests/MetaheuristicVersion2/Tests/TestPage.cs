using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellatrix.Web.Tests.MetaheuristicVersion2.Tests;
public class TestPage : WebPage
{
    public override string Url => "testpage.html"; // Update with actual path

    public TextField FirstName => App.Components.CreateById<TextField>("firstName");
    public TextField LastName => App.Components.CreateById<TextField>("lastName");
    public Email Email => App.Components.CreateById<Email>("email");
    public Phone Phone => App.Components.CreateById<Phone>("phone");
    public TextField ZipCode => App.Components.CreateById<TextField>("zip");
    public TextField Company => App.Components.CreateById<TextField>("company");
    public TextField Address => App.Components.CreateById<TextField>("address");
    public Button SubmitButton => App.Components.CreateByInnerTextContaining<Button>("Submit");
}
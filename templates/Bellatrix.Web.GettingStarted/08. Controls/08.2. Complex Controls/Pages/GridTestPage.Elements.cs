using Bellatrix.Web;

namespace Bellatrix.Web.GettingStarted;

public partial class GridTestPage
{
    public Grid SampleGrid => App.Components.CreateById<Grid>("sampleGrid")
        .SetColumn("Order", typeof(TextField), Find.By.Tag("input"))
        .SetColumn("Firstname")
        .SetColumn("Lastname")
        .SetColumn("Email Personal")
        .SetColumn("Email Business")
        .SetColumn("Actions", typeof(Button), Find.By.Xpath("./input[@type='button']"));
}
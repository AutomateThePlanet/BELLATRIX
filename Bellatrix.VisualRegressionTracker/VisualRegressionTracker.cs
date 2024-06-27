using System.Diagnostics;
using Bellatrix.Plugins.Screenshots.Contracts;
using VisualRegressionTracker;

namespace Bellatrix.VRT;

public static class VisualRegressionTracker
{
    private static VisualRegressionTrackerSettings Settings => ConfigurationService.GetSection<VisualRegressionTrackerSettings>();

    private static Config config = new Config {
    ApiUrl=Settings.ApiUrl,
    Project=Settings.Project,
    ApiKey=Settings.ApiKey,
    CiBuildId=Settings.CiBuildId,
    BranchName=Settings.Branch,
    EnableSoftAssert=Settings.EnableSoftAssert
    };

    private static global::VisualRegressionTracker.VisualRegressionTracker Tracker { get; set; }

    static VisualRegressionTracker()
    {
        Tracker = new global::VisualRegressionTracker.VisualRegressionTracker(config);
    }

    public static void TakeSnapshot()
    {
        var caller = new StackTrace().GetFrame(1);
        var name = caller?.GetMethod().Name;


        Tracker.Start().RunSynchronously();

        Tracker.Track(name, ServicesCollection.Current.Resolve<IScreenshotEngine>().TakeScreenshot(ServicesCollection.Current));

        Tracker.Stop();
    }

    public static void TakeSnapshot(string name)
    {
        Tracker.Start().RunSynchronously();

        Tracker.Track(name, ServicesCollection.Current.Resolve<IScreenshotEngine>().TakeScreenshot(ServicesCollection.Current));

        Tracker.Stop();
    }
}

using Bellatrix.Plugins;
using System;

namespace Bellatrix.Core.logging;
public class LoggerFlushPlugin : Plugin
{
    protected override void PreTestCleanup(object sender, PluginEventArgs e)
    {
        Logger.FlushLogs();
    }

    public static void Add()
    {
        ServicesCollection.Current.RegisterType<Plugin, LoggerFlushPlugin>(Guid.NewGuid().ToString());
    }
}

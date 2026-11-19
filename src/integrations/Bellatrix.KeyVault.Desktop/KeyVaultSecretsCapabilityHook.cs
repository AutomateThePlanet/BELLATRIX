using Bellatrix.Desktop.Events;
using Bellatrix.Desktop.Plugins;
using Bellatrix.KeyVault;

public static class KeyVaultSecretsCapabilityHook
{
    private static bool _initialized;

    public static void Add()
    {
        if (_initialized)
        {
            return;
        }

        AppLifecyclePlugin.CapabilityValueResolving += OnResolve;

        _initialized = true;
    }

    private static void OnResolve(object sender, CapabilityValueResolvingEventArgs e)
    {
        if (e.RawValue.StartsWith("env_") || e.RawValue.StartsWith("vault_"))
        {
            e.ResolvedValue = SecretsResolver.GetSecret(() => e.RawValue);
            e.Handled = true;
        }
    }
}
using Bellatrix.KeyVault;
using Bellatrix.Mobile.Events;
using Bellatrix.Mobile.Plugins;

public static class KeyVaultSecretsCapabilityHook
{
    private static bool _initialized;

    public static void Add()
    {
        if (_initialized)
        {
            return;
        }

        AppWorkflowPlugin.CapabilityValueResolving += OnResolve;
        CloudProviderCredentialsResolver.CapabilityValueResolving += OnResolve;

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
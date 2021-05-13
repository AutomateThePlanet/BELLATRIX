using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Bellatrix.KeyVault
{
    public static class KeyVault
    {
        private static SecretClient _secretClient;

        static KeyVault()
        {
            InitializeClient();
        }

        public static bool IsAvailable = _secretClient != null;

        public static string GetSecret(string name)
        {
            if (_secretClient == null)
            {
                return null;
            }

            var secret = _secretClient.GetSecret(name);
            return secret.Value.Value;
        }

        private static void InitializeClient()
        {
            if (_secretClient == null)
            {
                var settings = ConfigurationService.GetSection<KeyVaultSettings>();
                if (settings.IsEnabled && !string.IsNullOrEmpty(settings.KeyVaultEndpoint))
                {
                    // Create a new secret client using the default credential from Azure.Identity using environment variables previously set,
                    // including AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, and AZURE_TENANT_ID.
                    var cred = new ChainedTokenCredential(new ManagedIdentityCredential(), new AzureCliCredential());
                    _secretClient = new SecretClient(vaultUri: new Uri(settings.KeyVaultEndpoint), cred);
                }
            }
        }
    }
}

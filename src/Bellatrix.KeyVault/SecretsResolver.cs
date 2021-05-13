using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bellatrix.KeyVault
{
    public static class SecretsResolver
    {
        public static string GetSecret(Func<string> getConfigValue)
        {
            if (getConfigValue().StartsWith("env_"))
            {
                string environmentalVariable = Environment.GetEnvironmentVariable(getConfigValue().Replace("env_", string.Empty));
                return environmentalVariable;
            }
            else if (getConfigValue().StartsWith("vault_"))
            {
                string keyVaultValue = KeyVault.GetSecret(getConfigValue().Replace("vault_", string.Empty));
                return keyVaultValue;
            }
            else
            {
                return getConfigValue();
            }
        }

        public static string GetSecret(string name)
        {
            string environmentalVariable = Environment.GetEnvironmentVariable(name);
            if (!string.IsNullOrEmpty(environmentalVariable))
            {
                return environmentalVariable;
            }
            else if (KeyVault.IsAvailable)
            {
                string keyVaultValue = KeyVault.GetSecret(name);
                return keyVaultValue;
            }
            else
            {
                throw new ArgumentException("You need to initialize an environmental variable or key vault secret first.");
            }
        }
    }
}
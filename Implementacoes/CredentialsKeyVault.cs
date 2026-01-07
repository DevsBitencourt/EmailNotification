using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EmailNotification.Dto;
using Newtonsoft.Json;

namespace EmailNotification.Implementacoes
{
    internal class CredentialsKeyVault
    {
        public static async Task<RemetenteDto> Obter()
        {
            string keyVaultName = Environment.GetEnvironmentVariable(Constants.EnvironmentKeyVault);
            var client = new SecretClient(new Uri(keyVaultName), new DefaultAzureCredential());
            var keyVaultSecret = (await client.GetSecretAsync(Constants.CredentialsEmail)).Value;
            return JsonConvert.DeserializeObject<RemetenteDto>(keyVaultSecret.Value);
        }
    }
}

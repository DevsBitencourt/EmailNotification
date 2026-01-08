using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using EmailNotification.Dto;
using EmailNotification.Exceptions;
using Newtonsoft.Json;

namespace EmailNotification.Implementacoes
{
    internal class CredentialsKeyVault
    {
        #region Metodos Publicos

        public static async Task<RemetenteDto> Obter()
        {
            string keyVaultName = Environment.GetEnvironmentVariable(Constants.CredentialsKeyVault) ?? throw new CredentialsException(Constants.CredentialMessageError);
            
            var client = new SecretClient(new Uri(keyVaultName), new DefaultAzureCredential());
            var keyVaultSecret = (await client.GetSecretAsync(Constants.CredentialsEmail)).Value;
            
            return JsonConvert.DeserializeObject<RemetenteDto>(keyVaultSecret.Value) ?? throw new CredentialsException(Constants.CredentialMessageError);
        }

        #endregion
    }
}

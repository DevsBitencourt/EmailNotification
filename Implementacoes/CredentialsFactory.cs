using EmailNotification.Dto;
using EmailNotification.Enums;
using EmailNotification.Exceptions;

namespace EmailNotification.Implementacoes
{
    internal class CredentialsFactory
    {
        #region Metodos publicos

        public static async Task<RemetenteDto> Get()
        {
            var _environment = Environment.GetEnvironmentVariable(Constants.CredentialType) ?? throw new CredentialsException(Constants.CredentialMessageError);

            var tipoCredential = (ECredentialType)Convert.ToInt16(_environment);

            return tipoCredential switch
            {
                ECredentialType.KeyVault => await CredentialsKeyVault.Obter(),
                ECredentialType.Environment => CredentialsEnvironment.Get(),
                _ => throw new NotImplementedException(),
            };
        }

        #endregion
    }
}

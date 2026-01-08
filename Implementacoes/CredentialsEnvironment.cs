using EmailNotification.Dto;
using EmailNotification.Exceptions;
using Newtonsoft.Json;

namespace EmailNotification.Implementacoes
{
    internal class CredentialsEnvironment
    {
        #region Metodos publicos

        public static RemetenteDto Get()
        {
            string credentials = Environment.GetEnvironmentVariable(Constants.CredentialsEnvironment) ?? throw new CredentialsException(Constants.CredentialMessageError);

            return JsonConvert.DeserializeObject<RemetenteDto>(credentials) ?? throw new CredentialsException(Constants.CredentialMessageError);
        }

        #endregion
    }
}

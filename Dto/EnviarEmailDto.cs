namespace EmailNotification.Dto
{
    internal class EnviarEmailDto
    {
        #region Propriedades

        public EmailDadosDto Email { get; set; }
        public RemetenteDto Remetente { get; set; }

        #endregion

        #region Construtores

        public EnviarEmailDto(EmailDadosDto email, RemetenteDto remetente)
        {
            Email = email;
            Remetente = remetente;
        }

        #endregion
    }
}

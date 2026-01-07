namespace EmailNotification.Dto
{
    internal sealed class RemetenteDto
    {
        #region Propriedades
        public int Port { get; set; }
        public string Host { get; set; }
        public string From { get; set; }
        public string PasswdFrom { get; set; }

        #endregion
    }
}

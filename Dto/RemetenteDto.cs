namespace EmailNotification.Dto
{
    internal sealed class RemetenteDto
    {
        #region Propriedades

        public int Port { get; set; }
        public string Host { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string PasswdFrom { get; set; } = string.Empty;

        #endregion
    }
}

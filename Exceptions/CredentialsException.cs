namespace EmailNotification.Exceptions
{
    internal class CredentialsException : Exception
    {
        #region Construtores

        public CredentialsException()
        {
        }

        public CredentialsException(string? message) : base(message)
        {
        }

        #endregion
    }
}

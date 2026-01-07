namespace EmailNotification.Dto
{
    internal sealed class ObjectResponse
    {
        #region Propriedades

        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }

        #endregion
    }
}

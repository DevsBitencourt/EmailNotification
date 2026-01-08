namespace EmailNotification.Implementacoes
{
    internal sealed class DownloadTemplateHtml
    {
        #region Metodos publicos

        public static async Task<string> BaixarTemplateHtmlAsync()
        {
            using var http = new HttpClient();
            return await http.GetStringAsync(Constants.UriHtmlPadrao);
        }

        #endregion
    }
}

namespace EmailNotification.Implementacoes
{
    internal sealed class DownloadTemplateHtml
    {
        public static async Task<string> BaixarTemplateHtmlAsync()
        {
            var url = "https://raw.githubusercontent.com/DevsBitencourt/Imagens/refs/heads/main/Html/NotificacaoHelper.html";
            using var http = new HttpClient();
            return await http.GetStringAsync(url);
        }
    }
}

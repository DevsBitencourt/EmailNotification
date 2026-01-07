using EmailNotification.Dto;
using EmailNotification.Utilities;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace EmailNotification.Implementacoes
{
    internal class EmailServices
    {
        #region propriedades

        private readonly MimeMessage message = new();
        private readonly EnviarEmailDto data;

        #endregion

        #region Construtores

        public EmailServices(EnviarEmailDto data)
        {
            this.data = data;
        }

        #endregion

        #region Metodos Privados

        private EmailServices FromSet()
        {
            int startIndex = data.Remetente.From.IndexOf('@');
            string name = data.Remetente.From.Remove(startIndex);

            message.From.Add(new MailboxAddress(name, data.Remetente.From));
            return this;
        }

        private EmailServices SubjectSet()
        {
            message.Subject = data.Email.Assunto;
            return this;
        }

        private EmailServices ToSet()
        {
            foreach (var to in data.Email.Destinatarios)
            {
                message.To.Add(new MailboxAddress(to.Name, to.Email));
            }

            return this;
        }

        private async Task<EmailServices> BodySet()
        {
            var builder = new BodyBuilder();

            await AttachmentsSet(builder);

            if (data.Email.Formato.ToParse() == TextFormat.Html)
            {
                var html = await DownloadTemplateHtml.BaixarTemplateHtmlAsync();
                html = html.Replace("{{CORPO_REQUISICAO}}", data.Email.Corpo);

                builder.HtmlBody = html;
            }
            else
                builder.TextBody = data.Email.Corpo;

            message.Body = builder.ToMessageBody();

            return this;
        }

        private async Task<EmailServices> AttachmentsSet(BodyBuilder builder)
        {
            if (data.Email.Attachments.Any())
            {
                foreach (var item in data.Email.Attachments)
                {
                    await builder.Attachments.AddAsync(item.Name, new MemoryStream(Convert.FromBase64String(item.Data)), new ContentType(item.ContentType, string.Empty));
                }
            }

            return this;
        }

        private async Task<EmailServices> SendAsync()
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(data.Remetente.Host, data.Remetente.Port, false);
            await client.AuthenticateAsync(data.Remetente.From, data.Remetente.PasswdFrom);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            return this;
        }

        #endregion

        #region Metodos publicos

        public async Task SendEmailAsync()
        {
            await FromSet()
                    .SubjectSet()
                    .ToSet()
                    .BodySet();

            await SendAsync();
        }

        #endregion
    }
}

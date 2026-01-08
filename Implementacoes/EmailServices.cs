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
        private readonly EmailDadosDto data;
        private RemetenteDto Remetente;

        #endregion

        #region Construtores

        public EmailServices(EmailDadosDto data)
        {
            this.data = data;
        }

        #endregion

        #region Metodos Privados

        private EmailServices FromSet()
        {
            int startIndex = Remetente.From.IndexOf('@');
            string name = Remetente.From[..startIndex];

            message.From.Add(new MailboxAddress(name, Remetente.From));
            return this;
        }

        private EmailServices SubjectSet()
        {
            message.Subject = data.Assunto;
            return this;
        }

        private EmailServices ToSet()
        {
            foreach (var to in data.Destinatarios)
            {
                message.To.Add(new MailboxAddress(to.Name, to.Email));
            }

            return this;
        }

        private async Task<EmailServices> BodySet()
        {
            var builder = new BodyBuilder();

            await AttachmentsSet(builder);

            if (data.Formato.ToParse() == TextFormat.Html)
            {
                var html = await DownloadTemplateHtml.BaixarTemplateHtmlAsync();
                html = html.Replace("{{CORPO_REQUISICAO}}", data.Corpo);

                builder.HtmlBody = html;
            }
            else
                builder.TextBody = data.Corpo;

            message.Body = builder.ToMessageBody();

            return this;
        }

        private async Task<EmailServices> AttachmentsSet(BodyBuilder builder)
        {
            if (data.Attachments.Any())
            {
                foreach (var item in data.Attachments)
                {
                    await builder.Attachments.AddAsync(item.Name, new MemoryStream(Convert.FromBase64String(item.Data)), new ContentType(item.ContentType, string.Empty));
                }
            }

            return this;
        }

        private async Task<EmailServices> SendAsync()
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(Remetente.Host, Remetente.Port, false);
            await client.AuthenticateAsync(Remetente.From, Remetente.PasswdFrom);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            return this;
        }

        #endregion

        #region Metodos publicos

        public async Task SendEmailAsync()
        {
            Remetente = await CredentialsFactory.Get();

            await FromSet()
                    .SubjectSet()
                    .ToSet()
                    .BodySet();

            await SendAsync();
        }

        #endregion
    }
}

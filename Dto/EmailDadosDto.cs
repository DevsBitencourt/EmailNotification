using EmailNotification.Enums;
using EmailNotification.Properties;

namespace EmailNotification.Dto
{
    internal sealed class EmailDadosDto
    {
        #region propriedade

        public IEnumerable<DestinatariosDto> Destinatarios { get; set; } = Enumerable.Empty<DestinatariosDto>();
        public string Assunto { get; set; } = string.Empty;
        public string Corpo { get; set; } = string.Empty;
        public EFormatoBody Formato { get; set; } = EFormatoBody.Text;
        public IEnumerable<AttachmentsDto> Attachments { get; set; } = Enumerable.Empty<AttachmentsDto>();

        #endregion
    }
}

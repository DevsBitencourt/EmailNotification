using EmailNotification.Enums;

namespace EmailNotification.Utilities
{
    internal static class Utils
    {
        #region Metodos estaticos Publicos

        public static MimeKit.Text.TextFormat ToParse(this EFormatoBody format)
        {
            return format switch
            {
                EFormatoBody.Plain => MimeKit.Text.TextFormat.Plain,
                EFormatoBody.Flowed => MimeKit.Text.TextFormat.Flowed,
                EFormatoBody.Html => MimeKit.Text.TextFormat.Html,
                EFormatoBody.Enriched => MimeKit.Text.TextFormat.Enriched,
                EFormatoBody.CompressedRichText => MimeKit.Text.TextFormat.CompressedRichText,
                EFormatoBody.RichText => MimeKit.Text.TextFormat.RichText,
                _ => MimeKit.Text.TextFormat.Plain,
            };
        }

        #endregion
    }
}

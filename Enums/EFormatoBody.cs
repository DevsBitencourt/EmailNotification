namespace EmailNotification.Enums
{
    internal enum EFormatoBody : byte
    {
        #region Literais

        /// <summary>
        /// The plain text format.
        /// </summary>
        Plain = 0,

        /// <summary>
        /// An alias for the plain text format.
        /// </summary>
        Text = Plain,

        /// <summary>
        /// The flowed text format (as described in rfc3676).
        /// </summary>
        Flowed = 1,

        /// <summary>
        /// The HTML text format.
        /// </summary>
        Html = 2,

        /// <summary>
        /// The enriched text format.
        /// </summary>
        Enriched = 3,

        /// <summary>
        /// The compressed rich text format.
        /// </summary>
        CompressedRichText = 4,

        /// <summary>
        /// The rich text format.
        /// </summary>
        RichText = 5

        #endregion
    }
}

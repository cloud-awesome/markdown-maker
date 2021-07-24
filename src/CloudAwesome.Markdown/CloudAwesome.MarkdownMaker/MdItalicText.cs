namespace CloudAwesome.MarkdownMaker
{
    public class MdItalicText: IDocumentPart
    {
        public string Text { get; set; }

        public string Markdown => $"_{Text}_ ";

        public MdItalicText(string text)
        {
            Text = text;
        }
    }
}
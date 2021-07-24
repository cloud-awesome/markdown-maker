namespace CloudAwesome.MarkdownMaker
{
    public class MdPlainText: IDocumentPart
    {
        public string Text { get; set; }

        public string Markdown => $"{Text} ";

        public MdPlainText(string text)
        {
            Text = text;
        }
    }
}
namespace CloudAwesome.MarkdownMaker
{
    public class MdBoldText: IDocumentPart
    {
        public string Text { get; set; }

        public string Markdown => $"**{Text}** ";

        public MdBoldText(string text)
        {
            Text = text;
        }
    }
}
namespace CloudAwesome.MarkdownMaker
{
    public class MdLink: IDocumentPart
    {
        public string Text { get; set; }
        
        public string Url { get; set; }

        public string Markdown => $"[{Text}]({Url}) ";

        public MdLink(string text, string url)
        {
            Text = text;
            Url = url;
        }
    }
}
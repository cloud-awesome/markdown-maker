using System;

namespace CloudAwesome.MarkdownMaker
{
    public class MdImage: IDocumentPart
    {
        public string Text { get; set; }
        
        public string Url { get; set; }

        public string Markdown => $"![{Text}]({Url}){Environment.NewLine}";

        public MdImage(string text, string url)
        {
            Text = text;
            Url = url;
        }
    }
}
using System;

namespace CloudAwesome.MarkdownMaker
{
    public class MdCodeBlock: IDocumentPart
    {
        public string Text { get; set; }
        
        public string Language { get; set; }

        public string Markdown => 
            $"```{Language}{Environment.NewLine}" +
            $"{Text}" +
            $"```{Environment.NewLine}";

        public MdCodeBlock(string text, string language)
        {
            Text = text;
            Language = language;
        }
    }
}
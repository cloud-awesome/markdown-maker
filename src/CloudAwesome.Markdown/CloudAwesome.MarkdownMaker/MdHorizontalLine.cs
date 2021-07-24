using System;

namespace CloudAwesome.MarkdownMaker
{
    public class MdHorizontalLine: IDocumentPart
    {
        public string Markdown =>
            $"{Environment.NewLine}" +
            $"---" +
            $"{Environment.NewLine}" +
            $"{Environment.NewLine}";
    }
}
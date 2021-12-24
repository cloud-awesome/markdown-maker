using System;
using System.Text;

namespace CloudAwesome.MarkdownMaker.DocFx
{
    public class DocfxTab
    {
        public string Markdown
        {
            get
            {
                var stringBuilder = new StringBuilder();

                stringBuilder.Append($"# [{Title}](#tab/{Id}){Environment.NewLine}");
                stringBuilder.Append($"{Content.Markdown}{Environment.NewLine}");
                
                return stringBuilder.ToString();
            }
        }

        public DocfxTab(string title, string id, IDocumentPart content)
        {
            Title = title;
            Id = id;
            Content = content;
        }

        public string Title { get; set; }
        public string Id { get; set; }
        
        public IDocumentPart Content { get; set; }
        
    }
}
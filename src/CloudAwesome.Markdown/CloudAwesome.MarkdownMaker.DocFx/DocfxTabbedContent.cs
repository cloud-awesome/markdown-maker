using System;
using System.Collections.Generic;
using System.Text;

namespace CloudAwesome.MarkdownMaker.DocFx
{
    public class DocfxTabbedContent: IDocumentPart
    {
        public string Markdown
        {
            get
            {
                var stringBuilder = new StringBuilder();

                foreach (var tab in Tabs)
                {
                    stringBuilder.Append(tab.Markdown);
                }

                stringBuilder.Append($"***{Environment.NewLine}");

                return stringBuilder.ToString();
            }
        }

        public List<DocfxTab> Tabs { get; set; }

        public DocfxTabbedContent()
        {
            Tabs = new List<DocfxTab>();
        }

        public DocfxTabbedContent AddTab(DocfxTab tab)
        {
            Tabs.Add(tab);
            return this;
        }
    }
}
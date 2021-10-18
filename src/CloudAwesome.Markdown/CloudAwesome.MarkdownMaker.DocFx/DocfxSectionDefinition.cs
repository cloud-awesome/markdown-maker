using System;
using System.Collections.Generic;
using System.Text;

namespace CloudAwesome.MarkdownMaker.DocFx
{
    public class DocfxSectionDefinition: IDocumentPart
    {
        public string Markdown
        {
            get
            {
                var attributeStringBuilder = new StringBuilder();
                foreach (var attribute in DivAttributes)
                {
                    attributeStringBuilder.Append($"{attribute.Key}=\"{attribute.Value}\" ");
                }
                
                var stringBuilder = new StringBuilder();

                stringBuilder.Append($"> [!div {attributeStringBuilder.ToString()}]{Environment.NewLine}");
                foreach (var item in Items)
                {
                    stringBuilder.Append($"> {item.Markdown}{Environment.NewLine}");
                }

                return stringBuilder.ToString();
            }
        }

        public Dictionary<string, string> DivAttributes { get; }
        
        public List<MdPlainText> Items { get; }

        public DocfxSectionDefinition()
        {
            Items = new List<MdPlainText>();
            DivAttributes = new Dictionary<string, string>();
        }

        public DocfxSectionDefinition(MdPlainText text)
        {
            Items = new List<MdPlainText>
            {
                text
            };
            DivAttributes = new Dictionary<string, string>();
        }
        
        public DocfxSectionDefinition(string text)
        {
            Items = new List<MdPlainText>
            {
                new MdPlainText(text)
            };
            DivAttributes = new Dictionary<string, string>();
        }
        
        public DocfxSectionDefinition AddItem(MdPlainText item)
        {
            Items.Add(item);
            return this;
        }

        public DocfxSectionDefinition AddItem(string item)
        {
            Items.Add(new MdPlainText(item));
            return this;
        }

        public DocfxSectionDefinition AddDivAttribute(string key, string value)
        {
            DivAttributes.Add(key, value);
            return this;
        }
    }
}
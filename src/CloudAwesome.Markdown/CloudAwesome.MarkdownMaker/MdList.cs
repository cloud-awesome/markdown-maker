using System;
using System.Collections.Generic;
using System.Text;
using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdList: IDocumentPart
    {
        public List<MdPlainText> Items { get; set; }
        public MdListType ListType { get; set; }

        public string Markdown
        {
            get
            {
                this.Validate();
                
                var stringBuilder = new StringBuilder();
                var listPrefixMarkdown = ListType == MdListType.Ordered ? "1." : "-";

                foreach (var item in Items)
                {
                    stringBuilder.Append($"{listPrefixMarkdown} {item.Markdown}{Environment.NewLine}");
                }

                return stringBuilder.ToString();
            }
        }

        public MdList(MdListType listType)
        {
            Items = new List<MdPlainText>();
            ListType = listType;
        }

        public MdList AddItem(MdPlainText item)
        {
            Items.Add(item);
            
            return this;
        }
        
        private void Validate()
        {
            var validator = new MdListValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new InputValidationException(result.ToString());
            }
        }
    }
}
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
        public MdListType? ListType { get; set; }

        public string Markdown
        {
            get
            {
                this.Validate();
                
                var stringBuilder = new StringBuilder();

                string listPrefixMarkdown = ListType switch
                {
                    MdListType.Ordered => "1.",
                    MdListType.Unordered => "-",
                    MdListType.Todo => "- [ ]"
                };
                
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

        public MdList()
        {
            Items = new List<MdPlainText>();
        }

        public MdList AddItem(MdPlainText item)
        {
            Items.Add(item);
            return this;
        }
        
        public MdList AddItem(string item)
        {
            Items.Add(new MdPlainText(item));
            return this;
        }
        
        private void Validate()
        {
            var validator = new MdListValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
    }
}
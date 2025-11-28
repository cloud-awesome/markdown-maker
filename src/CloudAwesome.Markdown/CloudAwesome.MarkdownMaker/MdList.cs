using System.Text;
using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdList: IDocumentPart, IListPart
    {
        public List<IListPart> Items { get; set; }
        public MdListType? ListType { get; set; }

        public int Level = 0;

        public string Markdown
        {
            get
            {
                this.Validate();
                
                var stringBuilder = new StringBuilder();

                var indent = new string(' ', Level * 4);

                var listPrefixMarkdown = ListType switch
                {
                    MdListType.Ordered => $"{indent}1.",
                    MdListType.Unordered => $"{indent}-",
                    MdListType.Todo => $"{indent}- [ ]",
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                foreach (var item in Items)
                {
                    if (item.GetType() == typeof(MdPlainText))
                    {
                        stringBuilder.Append($"{listPrefixMarkdown} {item.Markdown}{Environment.NewLine}");
                    }
                    else
                    {
                        var childList = (MdList) item;
                        childList.Level = Level + 1;
                        
                        stringBuilder.Append(childList.Markdown);
                    }
                }

                return stringBuilder.ToString();
            }
        }

        public MdList(MdListType listType)
        {
            Items = new List<IListPart>();
            ListType = listType;
        }

        public MdList()
        {
            Items = new List<IListPart>();
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

        public MdList AddChildList(MdList childList)
        {
            Items.Add(childList);
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
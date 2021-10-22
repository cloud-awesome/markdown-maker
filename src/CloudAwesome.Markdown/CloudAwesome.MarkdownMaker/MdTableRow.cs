using System.Collections.Generic;
using System.Text;
using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdTableRow
    {
        public List<ISingleLinePart> Cells { get; }

        public string Markdown
        {
            get
            {
                this.Validate();
                
                var stringBuilder = new StringBuilder();

                stringBuilder.Append("| ");
                foreach (var cell in Cells)
                {
                    stringBuilder.Append($"{cell.Markdown}| ");
                }

                return stringBuilder.ToString();
            }
        }

        public MdTableRow()
        {
            Cells = new List<ISingleLinePart>();
        }
        
        public MdTableRow AddCell(ISingleLinePart cell)
        {
            Cells.Add(cell);
            return this;
        }
        
        public MdTableRow AddCell(string cellText)
        {
            Cells.Add(new MdPlainText(cellText));
            return this;
        }
        
        private void Validate()
        {
            var validator = new MdTableRowValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
    }
}
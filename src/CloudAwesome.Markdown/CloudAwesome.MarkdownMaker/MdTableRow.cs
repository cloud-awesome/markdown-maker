using System.Collections.Generic;
using System.Text;

namespace CloudAwesome.MarkdownMaker
{
    public class MdTableRow
    {
        public List<MdPlainText> Cells { get; set; }

        public string Markdown
        {
            get
            {
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
            Cells = new List<MdPlainText>();
        }
        
        public MdTableRow AddCell(MdPlainText cell)
        {
            Cells.Add(cell);

            return this;
        }
    }
}
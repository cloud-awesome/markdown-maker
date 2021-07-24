using System;
using System.Collections.Generic;
using System.Text;

namespace CloudAwesome.MarkdownMaker
{
    public class MdTable: IDocumentPart
    {
        public List<MdTableRow> Rows { get; set; }
        
        public List<MdPlainText> ColumnsHeaders { get; set; }

        public string Markdown
        {
            get
            {
                var stringBuilder = new StringBuilder();
                
                // Column headers
                stringBuilder.Append("| ");
                foreach (var columnsHeader in ColumnsHeaders)
                {
                    stringBuilder.Append($"{columnsHeader.Markdown}| ");
                }
                stringBuilder.Append(Environment.NewLine);
                
                // Header row
                stringBuilder.Append("|");
                foreach (var columnsHeader in ColumnsHeaders)
                {
                    stringBuilder.Append("---|");
                }
                stringBuilder.Append(Environment.NewLine);
                
                // Rows
                foreach (var row in Rows)
                {
                    stringBuilder.Append(row.Markdown);
                    stringBuilder.Append(Environment.NewLine);
                }
                
                stringBuilder.Append(Environment.NewLine);

                return stringBuilder.ToString();
            }
        }

        public MdTable()
        {
            ColumnsHeaders = new List<MdPlainText>();
            Rows = new List<MdTableRow>();
        }
        
        public MdTable AddColumn(MdPlainText columnHeader)
        {
            ColumnsHeaders.Add(columnHeader);

            return this;
        }

        public MdTable AddRow(MdTableRow tableRow)
        {
            Rows.Add(tableRow);

            return this;
        }
        
    }
}
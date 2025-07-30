﻿using System;
using System.Collections.Generic;
using System.Text;
using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdTable: IDocumentPart
    {
        public List<MdTableRow> Rows { get; set; }
        
        public List<ISingleLinePart> ColumnsHeaders { get; set; }

        public string Markdown
        {
            get
            {
                this.Validate();
                
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
            ColumnsHeaders = new List<ISingleLinePart>();
            Rows = new List<MdTableRow>();
        }
        
        public MdTable AddColumn(ISingleLinePart columnHeader)
        {
            ColumnsHeaders.Add(columnHeader);
            return this;
        }
        
        public MdTable AddColumn(string columnHeader)
        {
            ColumnsHeaders.Add(new MdPlainText(columnHeader));
            return this;
        }

        public MdTable AddColumns(params string[] columnHeaders)
        {
            foreach (var header in columnHeaders)
            {
                ColumnsHeaders.Add(new MdPlainText(header));
            }

            return this;
        }
        
        public MdTable AddColumns(params ISingleLinePart[] columnHeaders)
        {
            foreach (var header in columnHeaders)
            {
                ColumnsHeaders.Add(header);
            }

            return this;
        }

        public MdTable AddRow(MdTableRow tableRow)
        {
            Rows.Add(tableRow);
            return this;
        }

        public MdTable AddRowCells(params string[] rowCells)
        {
            var tableRow = new MdTableRow();
            
            foreach (var rowCell in rowCells)
            {
                tableRow.AddCell(rowCell);
            }
            
            Rows.Add(tableRow);
            
            return this;
        }
        
        public MdTable AddRowCells(params ISingleLinePart[] rowCells)
        {
            var tableRow = new MdTableRow();
            
            foreach (var rowCell in rowCells)
            {
                tableRow.AddCell(rowCell);
            }
            
            Rows.Add(tableRow);
            
            return this;
        }
        
        private void Validate()
        {
            var validator = new MdTableValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
    }
}
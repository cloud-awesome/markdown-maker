using System;
using System.Collections.Generic;
using System.Text;
using CloudAwesome.MarkdownMaker.DocFx.Validators;
using CloudAwesome.MarkdownMaker.Exceptions;

namespace CloudAwesome.MarkdownMaker.DocFx
{
    public class DocfxNote: IDocumentPart
    {
        public List<MdPlainText> Items { get; set; }

        public string Markdown
        {
            get
            {
                this.Validate();

                var stringBuilder = new StringBuilder();
                var noteClassMarkdown = $"> [!{NoteType.Value}] {Environment.NewLine}";
                stringBuilder.Append(noteClassMarkdown);

                foreach (var item in Items)
                {
                    stringBuilder.Append($"> {item.Markdown}{Environment.NewLine}");
                }

                stringBuilder.Append(Environment.NewLine);

                return stringBuilder.ToString();
            }
        }

        public DocfxNoteType? NoteType { get; set; }

        public DocfxNote()
        {
            Items = new List<MdPlainText>();
        }
        
        public DocfxNote(DocfxNoteType docfxNoteType)
        {
            Items = new List<MdPlainText>();
            NoteType = docfxNoteType;
        }
        
        public DocfxNote(DocfxNoteType docfxNoteType, string text)
        {
            Items = new List<MdPlainText>();
            NoteType = docfxNoteType;
            
            Items.Add(new MdPlainText(text));
        }

        public DocfxNote AddItem(MdPlainText item)
        {
            Items.Add(item);
            return this;
        }

        public DocfxNote AddItem(string item)
        {
            Items.Add(new MdPlainText(item));
            return this;
        }

        private void Validate()
        {
            var validator = new DocfxNoteValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
    }
}
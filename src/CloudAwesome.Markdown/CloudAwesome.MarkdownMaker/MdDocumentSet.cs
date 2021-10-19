using System.Collections.Generic;
using CloudAwesome.MarkdownMaker.Exceptions;
using CloudAwesome.MarkdownMaker.Validators;

namespace CloudAwesome.MarkdownMaker
{
    public class MdDocumentSet
    {
        public string FolderPath { get; }

        public List<MdDocument> Documents { get; set; }

        public MdDocumentSet(string folderPath)
        {
            Documents = new List<MdDocument>();
            FolderPath = !folderPath.EndsWith("/") ? $"{folderPath}/" : folderPath;
        }
        
        public MdDocumentSet AddDocument(MdDocument document)
        {
            Documents.Add(document);
            return this;
        }

        public void Generate()
        {
            this.Validate();
            
            foreach (var document in Documents)
            {
                document.FileName = $"{FolderPath}{document.FileName}";
                document.Save();
            }
        }

        private void Validate()
        {
            var validator = new MdDocumentSetValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                throw new MdInputValidationException(result.ToString());
            }
        }
    }
}
using System.IO;
using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdDocumentInSetValidator: AbstractValidator<MdDocument>
    {
        public MdDocumentInSetValidator()
        {
            RuleFor(document => document.FileName)
                .Must(BeAValidFileName)
                .WithMessage("MdDocument filename is not a valid filename. " +
                             "When a document is included in a MdDocumentSet the file name cannot be a file path");
        }

        private bool BeAValidFileName(string fileName)
        {
            return fileName.IndexOfAny(Path.GetInvalidFileNameChars()) <= 0;
        }
    }
}
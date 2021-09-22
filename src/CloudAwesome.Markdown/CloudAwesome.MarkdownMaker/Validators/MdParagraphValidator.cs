using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdParagraphValidator: AbstractValidator<MdParagraph>
    {
        public MdParagraphValidator()
        {
            RuleFor(paragraph => paragraph.DocumentParts.Count).GreaterThan(0)
                .WithMessage("The paragraph element is empty. " +
                             "Add items to the paragraph before writing to the document");
        }
    }
}
using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdQuoteValidator: AbstractValidator<MdQuote>
    {
        public MdQuoteValidator()
        {
            RuleFor(paragraph => paragraph.DocumentParts.Count).GreaterThan(0)
                .WithMessage("The quote element is empty. " +
                             "Add items to the quote before writing to the document");
        }
    }
}
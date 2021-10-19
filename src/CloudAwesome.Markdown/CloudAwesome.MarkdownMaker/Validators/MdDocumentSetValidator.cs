using FluentValidation;

namespace CloudAwesome.MarkdownMaker.Validators
{
    public class MdDocumentSetValidator: AbstractValidator<MdDocumentSet>
    {
        public MdDocumentSetValidator()
        {
            RuleForEach(set => set.Documents)
                .SetValidator(new MdDocumentInSetValidator());
        }
    }
}
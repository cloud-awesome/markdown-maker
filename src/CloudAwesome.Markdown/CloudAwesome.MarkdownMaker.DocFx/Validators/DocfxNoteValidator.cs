using FluentValidation;

namespace CloudAwesome.MarkdownMaker.DocFx.Validators
{
    public class DocfxNoteValidator: AbstractValidator<DocfxNote>
    {
        public DocfxNoteValidator()
        {
            RuleFor(note => note.Items.Count).GreaterThan(0)
                .WithMessage("There is no text in the note. " +
                             "Add text or items before writing to the document");

            RuleFor(note => note.NoteType.HasValue).Equal(true)
                .WithMessage("Note Type is null. " +
                             "It must be populated with the DocfxNoteType enum value");
        }
    }
}
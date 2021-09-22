using System;
using System.Diagnostics.CodeAnalysis;

namespace CloudAwesome.MarkdownMaker.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class MdInputValidationException: Exception
    {
        public MdInputValidationException() { }
        
        public MdInputValidationException(string message)
            : base(message) { }

        public MdInputValidationException(string message, Exception inner)
            : base(message, inner) { }
    }
}
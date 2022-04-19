using System;
using System.Collections.Generic;

namespace CloudAwesome.MarkdownMaker.Converters
{
    public static class PocoConverter
    {
        public static List<IDocumentPart> Convert(this IDocumentMe pocoClass)
        {
            throw new NotImplementedException();
        }

        public static List<IDocumentPart> Convert(this IEnumerable<IDocumentMe> pocoClasses)
        {
            throw new NotImplementedException();
        }
        
    }
}
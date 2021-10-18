using System.Collections.Generic;

namespace CloudAwesome.MarkdownMaker.DocFx.Tests.YamlHeaderModels
{
    public class StandardPageData: IYamlHeader
    {
        public string Uid { get; set; }
        public string Description { get; set; }
        public SeeAlsoReference[] References { get; set; }
    }
}
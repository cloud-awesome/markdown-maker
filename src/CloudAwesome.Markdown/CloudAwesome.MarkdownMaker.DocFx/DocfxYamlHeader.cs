using System;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CloudAwesome.MarkdownMaker.DocFx
{
    public class DocfxYamlHeader: IDocumentPart
    {
        public string Markdown
        {
            get
            {
                var serializer = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                var yaml = serializer.Serialize(YamlHeader);
                
                var stringBuilder = new StringBuilder();
                
                stringBuilder.Append($"---{Environment.NewLine}");
                stringBuilder.Append(yaml);
                stringBuilder.Append($"---{Environment.NewLine}");
                
                return stringBuilder.ToString();
            }
        }

        public IYamlHeader YamlHeader { get; set; }

        public DocfxYamlHeader(IYamlHeader yamlHeader)
        {
            YamlHeader = yamlHeader;
        } 
    }
}
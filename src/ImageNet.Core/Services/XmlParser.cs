using System.Xml.Linq;
using ImageNet.Core.Entities;
using ImageNet.Core.Interfaces;

namespace ImageNet.Core.Services
{
    public class XmlParser : IXmlParser
    {
        public List<ImageNetItem> ParseXml(string xmlContent)
        {
            try
            {
                XDocument doc = XDocument.Parse(xmlContent);
                var nodes = new List<ImageNetItem>();
                ParseNode(doc.Root, "", nodes);
                return nodes;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed to parse XML: {e.Message}", e);
            }
        }

        private static void ParseNode(XElement element, string parentPath, List<ImageNetItem> nodes)
        {
            foreach (var synset in element.Elements("synset"))
            {
                string name = synset.Attribute("words")?.Value ?? synset.Attribute("wnid")?.Value ?? "";
                string fullPath = string.IsNullOrEmpty(parentPath) ? name : $"{parentPath} > {name}";
                int size = synset.Descendants("synset").Count();

                nodes.Add(new ImageNetItem { Name = fullPath, Size = size });
            
                ParseNode(synset, fullPath, nodes);
            }
        }
    }
}
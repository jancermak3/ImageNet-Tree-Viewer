using ImageNet.Core.Entities;

namespace ImageNet.Core.Interfaces
{
    public interface IXmlParser
    {
        List<ImageNetItem> ParseXml(string xmlContent);
    }
}
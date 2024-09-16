using ImageNet.Core.Interfaces;

namespace ImageNet.Worker.Services
{
    public class XmlProcessingService : IXmlProcessingService
    {
        private readonly IXmlParser _xmlParser;
        private readonly IImageNetItemRepository _repository;
        private readonly ILogger<XmlProcessingService> _logger;

        public XmlProcessingService(IXmlParser xmlParser, IImageNetItemRepository repository, ILogger<XmlProcessingService> logger)

        {
            _xmlParser = xmlParser;
            _repository = repository;
            _logger = logger;
        }

        public void ProcessXmlFile(string xmlContent)
        {
            _logger.LogInformation("Parsing XML content");
            var items = _xmlParser.ParseXml(xmlContent);
            _logger.LogInformation("Parsed {count} items from XML", items.Count());

            _logger.LogInformation("Saving items to database");
            _repository.SaveAll(items);
            _logger.LogInformation("Items saved successfully");
        }
    }
}
using ImageNet.Core.Interfaces;

namespace ImageNet.Infrastructure.Services
{
    public class XmlDownloader : IXmlDownloader
    {
        private readonly HttpClient _httpClient;

        public XmlDownloader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> DownloadXmlAsync(string url)
        {
            try
            {
                return await _httpClient.GetStringAsync(url);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Failed to download XML from {url}: {ex.Message}", ex);
            }
        }
    }
}
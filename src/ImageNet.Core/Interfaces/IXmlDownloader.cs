namespace ImageNet.Core.Interfaces
{
    public interface IXmlDownloader
    {
        Task<string> DownloadXmlAsync(string url);
    }
}
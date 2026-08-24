namespace Store.Application.Settings
{
    public class FileStorageSettings
    {
        public string[] AllowedImageExtensions { get; set; } = [];
        public string[] AllowedDocumentExtensions { get; set; } = [];
    }
}
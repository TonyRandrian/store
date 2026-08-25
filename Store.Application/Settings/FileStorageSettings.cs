namespace Store.Application.Settings
{
    public class FileStorageSettings
    {
        public string[] AllowedImageExtensions { get; set; } = [];
        public string[] AllowedDocumentExtensions { get; set; } = [];
        public string ProductImageFolder { get; set; } = string.Empty;
        public string ProductDocumentFolder { get; set; } = string.Empty;
        public string UploadDir { get; set; } = "uploads";
    }
}
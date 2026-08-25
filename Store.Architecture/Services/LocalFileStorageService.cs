using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Store.Application.Interfaces.Services;
using Store.Application.Settings;

namespace Store.Infrastructure.Services
{
    public class LocalFileStorageService(
        IWebHostEnvironment env,
        IOptions<FileStorageSettings> settings)
        : IFileStorageService
    {
        private readonly string _webRootPath = env.WebRootPath;
        private readonly FileStorageSettings _settings = settings.Value;


        public Task DeleteAsync(string path)
        {
            string fullPath = Path.Combine(_webRootPath, path);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }

        public async Task<string> SaveAsync(Stream content, string fileName, string folder)
        {
            string uploadsFolder = Path.Combine(_webRootPath, _settings.UploadDir, folder);
            Directory.CreateDirectory(uploadsFolder);

            string filePath = Path.Combine(uploadsFolder, fileName);
            await using FileStream output = new(filePath, FileMode.Create);
            await content.CopyToAsync(output);

            return Path.Combine(_settings.UploadDir, folder, fileName).Replace("\\", "/");
        }
    }
}

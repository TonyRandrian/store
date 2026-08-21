using Microsoft.AspNetCore.Hosting;
using Store.Application.Interfaces;

namespace Store.Infrastructure.Services
{
    public class LocalFileStorageService(IWebHostEnvironment env)
        : IFileStorageService
    {
        private readonly string _webRootPath = env.WebRootPath;


        public Task DeleteAsync(string path)
        {
            string fullPath = Path.Combine(_webRootPath, path);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }

        public async Task<string> SaveAsync(Stream content, string fileName, string folder)
        {
            string uploadsFolder = Path.Combine(_webRootPath, "uploads", folder);
            Directory.CreateDirectory(uploadsFolder);

            string filePath = Path.Combine(uploadsFolder, fileName);
            await using FileStream output = new(filePath, FileMode.Create);
            await content.CopyToAsync(output);

            return Path.Combine("uploads", folder, fileName).Replace("\\", "/");
        }
    }
}

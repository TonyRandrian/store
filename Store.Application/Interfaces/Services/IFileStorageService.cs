namespace Store.Application.Interfaces.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveAsync(Stream content, string fileName, string folder);
        Task DeleteAsync(string path);
    }
}
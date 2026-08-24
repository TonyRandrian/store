namespace Store.Application.Interfaces.Repositories
{
    public interface IFileStorageRepository
    {
        Task<string> SaveAsync(string fileStream, string fileName, string folder);
        Task DeleteAsync(string path);
    }
}

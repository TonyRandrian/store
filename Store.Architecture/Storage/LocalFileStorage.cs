using Store.Application.Interfaces;

namespace Store.Infrastructure.Storage
{
    public class LocalFileStorage : IFileStorageRepository
    {

        public Task DeleteAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<string> SaveAsync(string fileStream, string fileName, string folder)
        {
            throw new NotImplementedException();
        }
    }
}

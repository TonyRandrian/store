using Store.Application.Commons;
using Store.Application.Interfaces;
using Store.Domain.Entities;
using Store.Infrastructure.Persistence;

namespace Store.Infrastructure.Repositories
{
    public class ImageRepository(StoreDbContext context) : IImageRepository
    {
        private readonly StoreDbContext _context = context;


        public async Task<Image> AddAsync(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<Image>> GetAllAsync(int pageNum, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Image?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Image> Update(Image image)
        {
            throw new NotImplementedException();
        }
    }
}

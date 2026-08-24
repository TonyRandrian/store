using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteAsync(Guid id)
        {
            Image? image = await GetById(id)
                ?? throw new KeyNotFoundException($"No image with the id {id} found");

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
        }

        public Task<PagedResult<Image>> GetAllAsync(int pageNum, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Image?> GetById(Guid id)
        {
            return await _context.Images
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<Image> Update(Image image)
        {
            throw new NotImplementedException();
        }
    }
}

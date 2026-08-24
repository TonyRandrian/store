using Microsoft.EntityFrameworkCore;
using Store.Application.Commons;
using Store.Application.Interfaces.Repositories;
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

        public async Task<PagedResult<Image>> GetAllAsync(int pageNum, int pageSize)
        {
            int totalRecords = await _context.Images.CountAsync();

            List<Image> data = await _context.Images
                .Include(i => i.Product)
                .AsNoTracking()
                .OrderBy(i => i.Id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Image>
            {
                Data = data,
                TotalRecords = totalRecords,
                PageNumber = pageNum,
                PageSize = pageSize
            };
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

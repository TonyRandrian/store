using Store.Application.Commons;
using Store.Domain.Entities;

namespace Store.Application.Interfaces
{
    public interface IImageRepository
    {
        Task<PagedResult<Image>> GetAllAsync(int pageNum, int pageSize);
        Task<Image?> GetById(Guid id);
        Task<Image> AddAsync(Image image);
        Task DeleteAsync(Guid id);
        Task<Image> Update(Image image);
    }
}

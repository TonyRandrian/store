using Store.Application.Commons;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Categories
{
    public class GetCategoryProductsUseCase(ICategoryRepository categoryRepository)
    {
        private readonly ICategoryRepository CategoryRepository = categoryRepository;


        public async Task<PagedResult<ProductResponse>> Execute(Guid id, int pageNum, int pageSize)
        {
            PagedResult<Product> products = await CategoryRepository.GetCategoryProducts(
                id, pageNum, pageSize);

            PagedResult<ProductResponse> response = new()
            {
                PageNumber = pageNum,
                PageSize = pageSize,
                TotalRecords = products.TotalRecords
            };

            foreach (Product product in products.Data)
            {
                response.Data.Add(new ProductResponse(product));
            }

            return response;
        }
    }
}

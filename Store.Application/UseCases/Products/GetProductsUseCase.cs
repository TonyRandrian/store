using Store.Application.Commons;
using Store.Application.DTOs.Products;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Application.UseCases.Products
{
    public class GetProductsUseCase(IProductRepository productRepository)
    {
        private readonly IProductRepository ProductRepository = productRepository;

        public async Task<PagedResult<ProductResponse>> Execute(int pageNum, int pageSize)
        {
            PagedResult<Product> products = await ProductRepository.GetAllAsync(pageNum, pageSize);
            PagedResult<ProductResponse> result = new()
            {
                PageNumber = pageNum,
                PageSize = pageSize,
                TotalRecords = products.TotalRecords
            };

            foreach (Product product in products.Data)
            {
                result.Data.Add(new ProductResponse(product));
            }

            return result;
        }
    }
}

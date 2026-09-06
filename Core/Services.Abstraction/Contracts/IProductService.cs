using Shared;
using Shared.Dtos.ProductModule;
using Shared.Enums;

namespace Services.Abstraction.Contracts
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters);
        Task<IEnumerable<BrandResultDto>> GetAllBrandAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypeAsync();
        Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}

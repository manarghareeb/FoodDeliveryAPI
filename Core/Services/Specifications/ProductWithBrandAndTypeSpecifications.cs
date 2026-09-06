using Domain.Entities.ProductModule;
using Shared;
using Shared.Enums;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        // GetAllProduct => Include Brand, Type
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationParameters parameters) 
            : base(p=> (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId) && 
                       (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId) &&
                       (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower())))
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
            // Sort by switch [NameAsc, NameDesc, PriceAsc, PriceDesc]
            switch (parameters.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc: 
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
            ApplyPagination(parameters.PageSize, parameters.PageIndex);
        }
        // GetProductById (int id) [Where] => Include Brand, Type
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
        }
    }
}

using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : Specification<Product>
{
    public ProductSpecification(ProductSpecParams specParams) : base(x => 
        (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) && //x.Name.Contains(specParams.Search, StringComparison.CurrentCultureIgnoreCase))
        (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) &&
        (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type))
    ) 
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex -1), specParams.PageSize);

        switch (specParams.Sort)
        {
            case "priceAsc":
                base.AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                base.AddOrderByDescending(x => x.Price);
                break;
            default:
                base.AddOrderBy(x => x.Name);
                break;
        }
    }
}

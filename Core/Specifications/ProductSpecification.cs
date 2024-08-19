using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : Specification<Product>
{
    public ProductSpecification(string? brand, string? type, string? sort) : base(x => 
        (x.Brand == brand || string.IsNullOrWhiteSpace(brand)) &&
        (x.Type == type || string.IsNullOrWhiteSpace(type))
    ) 
    {
        switch (sort)
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

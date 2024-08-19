using Core.Entities;

namespace Core.Specifications;

public class TypeListSpecification : Specification<Product, string>
{   
    public TypeListSpecification()
    {
        base.AddSelect(x => x.Type);
        base.ApplyDistinct();   
    }
}

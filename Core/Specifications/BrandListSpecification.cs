using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class BrandListSpecification : Specification<Product, string>
{
    public BrandListSpecification()
    {
        base.AddSelect(x => x.Brand);
        base.ApplyDistinct();
    }    
}

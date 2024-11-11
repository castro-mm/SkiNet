using Api.RequestHelpers;
using Core.Base;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repository, ISpecification<T> spec, int pageIndex, int pageSize) where T : Entity
    {
        var items = await repository.ListAsync(spec);
        var count = await repository.CountAsync(spec);

        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);

        return Ok(pagination);
    }    

    protected async Task<ActionResult> CreatePagedResult<T, TDto>(IGenericRepository<T> repository, ISpecification<T> spec, int pageIndex, int pageSize, Func<T, TDto> toDto) where T : Entity, IDtoConvertible
    {
        var items = await repository.ListAsync(spec);
        var count = await repository.CountAsync(spec);

        var dtoItems = items.Select(toDto).ToList();

        var pagination = new Pagination<TDto>(pageIndex, pageSize, count, dtoItems);

        return Ok(pagination);
    }
}

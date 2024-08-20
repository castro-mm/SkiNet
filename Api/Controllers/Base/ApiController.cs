using Api.RequestHelpers;
using Core.Base;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class ApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repository, ISpecification<T> spec, int pageIndex, int pageSize) where T : Entity
    {
        var items = await repository.ListAsync(spec);
        var count = await repository.CountAsync(spec);

        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);

        return Ok(pagination);
    }    
}

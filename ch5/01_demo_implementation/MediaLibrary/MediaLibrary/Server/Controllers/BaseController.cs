using Microsoft.AspNetCore.Mvc;

namespace MediaLibrary.Server.Controllers;

[ApiController]
[Route("rest/[controller]")]
public class BaseController<TModel, TEntity, TService> : ControllerBase
    where TModel : Shared.Model.IModel, new()
    where TEntity : Data.BaseEntity
    where TService : Services.BaseService<TEntity, TModel>
{
    private readonly TService _service;
    private readonly string _createPath;

    public BaseController(TService service, string createPath)
    {
        _service = service;
        _createPath = createPath;
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] TModel model)
    {
        model = await _service.CreateAsync(model);
        return Created($"{_createPath}/{model.Id}", new { Id = model.Id });
    }

    [HttpPut("{id:int}")]
    public virtual async Task<IActionResult> Update([FromRoute] int id, [FromBody] TModel model)
    {
        await _service.UpdateAsync(id, model);
        return Ok();
    }

    [HttpGet("{id:int}")]
    public virtual async Task<TModel> Get(int id)
    {
        return await _service.GetByIdAsync(id);
    }

    [HttpGet("list")]
    public virtual async Task<IEnumerable<TModel>> GetList()
    {
        var data = await _service.GetAllAsync();
        return data;
    }


    [HttpDelete("{id:int}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}

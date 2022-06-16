using AutoMapper;
using MediaLibrary.Server.Data;
using MediaLibrary.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Server.Services;
public abstract class BaseService<TEntity, TModel>
    where TEntity : BaseEntity
    where TModel : IModel, new()

{
    private readonly MediaLibraryDbContext _dbContext;
    private readonly IMapper _mapper;

    public BaseService(MediaLibraryDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<TModel> CreateAsync(TModel model)
    {
        var entity = _mapper.Map<TEntity>(model);
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TModel>(entity);
    }

    public async Task<TModel> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            return new TModel();
        }

        var entity = await GetEntityByIdAsync(id);
        return _mapper.Map<TModel>(entity);
    }

    public async Task<IEnumerable<TModel>> GetAllAsync()
    {
        var entities = await _dbContext
            .Set<TEntity>()
            .AsNoTracking()
            .ToListAsync();
        return entities.Select(x => _mapper.Map<TModel>(x));
    }

    public async Task<TModel> UpdateAsync(int id, TModel model)
    {
        var entity = await GetEntityByIdAsync(id);
        _mapper.Map<TModel, TEntity>(model, entity);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TModel>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetEntityByIdAsync(id);
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<TEntity> GetEntityByIdAsync(int id)
    {
        var entity = await _dbContext.FindAsync<TEntity>(id);

        if (entity == null)
        {
            throw new Exception($"Cannot find entity type {typeof(TEntity)} with id {id}");
        }

        return entity;
    }
}

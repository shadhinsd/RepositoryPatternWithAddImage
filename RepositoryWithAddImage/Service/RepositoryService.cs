
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepositoryWithAddImage.DatabaseContext;
using RepositoryWithAddImage.Models.Base;
using RepositoryWithAddImage.ViewModel;

namespace RepositoryWithAddImage.Service;

public class RepositoryService<TEntity, IModel> : IRepositoryService<TEntity, IModel> where TEntity : AuditableEntity, new() where IModel : BaseEntity
{
    private readonly IMapper _mapper;
    private readonly StudentDbContext dbContext;

    public RepositoryService(StudentDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        _mapper = mapper;
        Dbset=dbContext.Set<TEntity>();
    }
    public DbSet<TEntity> Dbset { get; } 
    public async Task<IModel> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity=await Dbset.FindAsync(id, cancellationToken);
        if (entity != null) { return null; }
        entity.IsDeleted=true;
        Dbset.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        var deleteModel=_mapper.Map<TEntity,IModel>(entity);
        return deleteModel;
    }

    public async Task<IEnumerable<IModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entity=await Dbset.AsNoTracking().Where(x=>!x.IsDeleted).ToListAsync(cancellationToken);
        if (entity == null) return null;
        var model = _mapper.Map<IEnumerable<IModel>>(entity);
        return model;
    }

    public async Task<IModel> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await Dbset.FindAsync(id);
        if (entity == null) return null;
        var model= _mapper.Map<TEntity,IModel>(entity);
        return model;
    }

    public async Task<IModel> InsertAsync(IModel model, CancellationToken cancellationToken)
    {
        var entity=_mapper.Map<IModel,TEntity>(model);
        entity.CreatedDate = DateTime.Now;
        entity.CreatedBy = 2866;
        Dbset.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        var insertModel = _mapper.Map<TEntity, IModel>(entity);
        return insertModel;
    }

    public async Task<IModel> UpdateAsync(long id, IModel model, CancellationToken cancellationToken)
    {
        var entity=await Dbset.FindAsync(id);
        if (entity == null) return null;
        entity.ModifyidDate = DateTime.Now;
        _mapper.Map(model, entity);
        Dbset.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        var updateModel = _mapper.Map<TEntity, IModel>(entity);
        return updateModel;
    }
}

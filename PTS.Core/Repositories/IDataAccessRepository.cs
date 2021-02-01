using PTS.Core.Signatures;

namespace PTS.Core.Repositories
{
    public interface IDataAccessRepository<TEntity>:IRepository<TEntity> where TEntity : class,IBaseEntity,new()
    {
    }
}

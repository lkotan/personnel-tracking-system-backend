using Microsoft.EntityFrameworkCore;
using PTS.Core.Signatures;

namespace PTS.Core.Repositories.EF
{
    public class EfDataAccessRepository<TEntity> : EfRepository<TEntity, DbContext>, IDataAccessRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        public EfDataAccessRepository(DbContext context) : base(context)
        {
        }
    }
}

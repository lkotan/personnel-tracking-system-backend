using PTS.Core.Signatures;
using PTS.Core.Utilities.Results.DataResult;
using PTS.Core.Utilities.Results.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetAsync(int id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> AnyAsync(int id);
        Task<IDataResponse<int>> InsertAsync(TEntity entity);
        Task<IResponse> UpdateAsync(TEntity entity);
        Task<IDataResponse<int>> DeleteAsync(TEntity entity);
        IEnumerable<TEntity> GetSql(string sql);
    }
}

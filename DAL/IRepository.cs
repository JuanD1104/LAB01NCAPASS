using System.Linq.Expressions;

namespace DAL
{
    namespace Models
    {
        public interface IRepository : IDisposable
        {
            Task<TEntity> CreateAsync<TEntity>(TEntity toCreate) where TEntity : class;

            Task<bool> DeleteAsync<TEntity>(TEntity toDelete) where TEntity : class;

            Task<bool> UpdateAsync<TEntity>(TEntity toUpdate) where TEntity : class;

            Task<TEntity> RetreiveAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

            Task<List<TEntity>> FilterAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        }
    }

}
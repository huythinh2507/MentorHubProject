using System.Linq.Expressions;
using YPP.MH.Shared.BaseEntity;

namespace YPP.MH.DataAccessLayer.Repositories.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        Task<bool> IsUpdate(T entity);
        Task CreateRange(List<T> entities);
        Task Delete(T entity);
        Task<int> ExecStoredProcedure(string procName);
    }
}

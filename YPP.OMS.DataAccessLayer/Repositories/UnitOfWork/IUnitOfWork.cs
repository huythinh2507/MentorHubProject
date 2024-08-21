using YPP.MH.DataAccessLayer.Repositories.Base;
using YPP.MH.Shared.BaseEntity;

namespace YPP.MH.DataAccessLayer.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        public int SaveChanges();
        Task<int> SaveChangesAsync();
        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    }
}

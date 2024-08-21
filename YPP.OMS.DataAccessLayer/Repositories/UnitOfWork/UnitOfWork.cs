using Microsoft.EntityFrameworkCore;
using System.Collections;
using YPP.MH.DataAccessLayer.Models;
using YPP.MH.DataAccessLayer.Repositories.Base;
using YPP.MH.Shared.BaseEntity;

namespace YPP.MH.DataAccessLayer.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContextFactory<MHDbContext> _dbContextFactory;
        private readonly MHDbContext _dbContext;
        private Hashtable? _repos;


        public UnitOfWork(IDbContextFactory<MHDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _dbContext = _dbContextFactory.CreateDbContext();        
        }

        #region Repositories


        #endregion

        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            _repos ??= new Hashtable();
            var type = typeof(TEntity).Name;
            if (!_repos.ContainsKey(type))
            {
                var repository = new BaseRepository<TEntity>(_dbContextFactory);
                _repos.Add(type, repository);
            }
            return (IBaseRepository<TEntity>)_repos[type]!;
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

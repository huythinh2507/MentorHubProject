using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YPP.MH.DataAccessLayer.Models;
using YPP.MH.Shared.BaseEntity;

namespace YPP.MH.DataAccessLayer.Repositories.Base
{
    //generic repository pattern
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IDbContextFactory<MHDbContext> _dbContextFactory;
        private readonly MHDbContext _dbContext;
        private bool disposed = false;

        public BaseRepository(IDbContextFactory<MHDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _dbContext = _dbContextFactory.CreateDbContext();
        }
        public IQueryable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbContextFactory.CreateDbContext().Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }


        public async Task Create(T entity)
        {
            await using MHDbContext dbContext = await _dbContextFactory.CreateDbContextAsync();
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            var existed = await _dbContext.FindAsync<T>(entity.Id);
            if (existed != null)
            {
                _dbContext.Remove(entity);
            }
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dbContextFactory.CreateDbContext().Set<T>().AsNoTracking();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContextFactory.CreateDbContext().Set<T>()
                .FirstOrDefaultAsync(t => t.Id.Equals(id)) ?? throw new Exception("Invalid entity");
        }

        public async Task CreateRange(List<T> entities)
        {
            await _dbContextFactory.CreateDbContext().AddRangeAsync(entities);
        }
        public async Task Update(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            await _dbContext.DisposeAsync();
        }
        public async Task<bool> IsUpdate(T entity)
        {

            var context = _dbContextFactory.CreateDbContext();
            context.Update(entity);
            return await context.SaveChangesAsync() > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed && disposing)
            {
                _dbContext.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public Task<int> ExecStoredProcedure(string procName)
        {
            return _dbContextFactory.CreateDbContext().Database.ExecuteSqlAsync($"{procName}");
        }
    }
}

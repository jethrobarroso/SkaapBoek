using Microsoft.EntityFrameworkCore;
using SkaapBoek.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
    public interface IDataService<TEntity> where TEntity : class
    {
        Task<TEntity> Add(TEntity newEntity);
        Task<TEntity> Delete(int id);
        IQueryable<TEntity> GetAll();
        Task<TEntity> Update(TEntity updatedEntity);
        Task<IEnumerable<Color>> GetColors();
        Task<IEnumerable<Feed>> GetAllFeed();
        Task<IEnumerable<SheepCategory>> GetCategories();
    }

    public abstract class BaseService<TEntity> : IDataService<TEntity>
        where TEntity : class
    {
        public BaseService(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; set; }

        public async Task<TEntity> Add(TEntity newEntity)
        {
            Context.Set<TEntity>().Add(newEntity);
            await Context.SaveChangesAsync();
            return newEntity;
        }

        public async Task<TEntity> Update(TEntity updatedEntity)
        {
            Context.Entry(updatedEntity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return updatedEntity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
                await Context.SaveChangesAsync();
            }

            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public async Task<IEnumerable<Color>> GetColors()
        {
            return await Context.ColorSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Feed>> GetAllFeed()
        {
            return await Context.FeedSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SheepCategory>> GetCategories()
        {
            return await Context.SheepCategorySet.AsNoTracking().ToListAsync();
        }
    }
}

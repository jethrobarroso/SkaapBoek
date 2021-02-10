using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL.Services
{
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
            
            if(entity != null)
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
            return await Context.ColorSet.ToListAsync();
        }
    }
}

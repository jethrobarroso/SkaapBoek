using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkaapBoek.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkaapBoek.DAL
{
    public interface IDataRepository<TEntity>
    {
        Task<TEntity> Add(TEntity newEntity);
        Task<TEntity> Delete(int id);
        IQueryable<TEntity> GetAll();
        Task<TEntity> Update(TEntity entity);
    }

    public class DataRepository<TEntity> : IDataRepository<TEntity>
        where TEntity : class
    {
        private readonly AppDbContext _context;

        public DataRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Add(TEntity newEntity)
        {
            _context.Set<TEntity>().Add(newEntity);
            await _context.SaveChangesAsync();
            return newEntity;
        }
    }
}

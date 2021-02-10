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
    }
}

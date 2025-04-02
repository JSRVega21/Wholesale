using System.Collections.Generic;
using System.Threading.Tasks;
using Wholesale.Models;
using Wholesale.Server.Data;

namespace Wholesale.Server.Repository
{
    public interface IVisitHeaderRepository<T, TKey>
    {
        IList<T> GetList();
        T GetByKey(TKey key);
        T GetByKey(TKey key, bool tracking = false);
        T Add(T entity);
        T Update(T entity);
        void Delete(TKey key);

        Task<IList<T>> GetListAsync();
        Task<T> GetByKeyAsync(TKey key);
        Task<T> GetByKeyAsync(TKey key, bool tracking = false);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(TKey key);
        ApplicationDbContext GetDbContext();

        IEnumerable<T> GetBySalespersonOrPos(int? slpcode, int? codigopos);
    }
}

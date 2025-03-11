using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wholesale.Server.Data;
using Wholesale.Models;

namespace Wholesale.Server.Repository
{

    public class VisitTypeRepository : IRepository<VisitType, int>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;
        public VisitTypeRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public VisitType Add(VisitType entity)
        {
            var db = _factory.CreateDbContext();
            entity.Initialize();
            db.VisitTypes.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public async Task<VisitType> AddAsync(VisitType entity)
        {
            var db = _factory.CreateDbContext();
            entity.Initialize();
            db.VisitTypes.Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public void Delete(int key)
        {
            var db = _factory.CreateDbContext();
            VisitType entity = db.VisitTypes.Find(key);
            db.VisitTypes.Remove(entity);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int key)
        {
            var db = _factory.CreateDbContext();
            VisitType entity = await db.VisitTypes.FindAsync(key);
            db.VisitTypes.Remove(entity);
            await db.SaveChangesAsync();
        }

        public VisitType GetByKey(int key)
        {
            return GetByKey(key, true);
        }

        public VisitType GetByKey(int key, bool tracking = false)
        {
            var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.VisitTypes.Find(key);
            }
            else
            {
                return db.VisitTypes.AsNoTracking().FirstOrDefault(item => item.VisitTypeId == key);
            }
        }

        public async Task<VisitType> GetByKeyAsync(int key)
        {
            return await GetByKeyAsync(key, true);
        }

        public async Task<VisitType> GetByKeyAsync(int key, bool tracking = false)
        {
            var db = _factory.CreateDbContext();
            if (tracking)
            {
                return await db.VisitTypes.FindAsync(key);
            }
            else
            {
                return await db.VisitTypes.AsNoTracking().FirstOrDefaultAsync(item => item.VisitTypeId == key);
            }
        }

        public IList<VisitType> GetList()
        {
            var db = _factory.CreateDbContext();
            return db.VisitTypes.ToList();
        }

        public async Task<IList<VisitType>> GetListAsync()
        {
            var db = _factory.CreateDbContext();
            return await db.VisitTypes.ToListAsync();
        }

        public VisitType Update(VisitType entity)
        {
            var db = _factory.CreateDbContext();
            entity.Updated();
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return entity;
        }

        public async Task<VisitType> UpdateAsync(VisitType entity)
        {
            var db = _factory.CreateDbContext();
            entity.Updated();
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return entity;
        }
    }
}

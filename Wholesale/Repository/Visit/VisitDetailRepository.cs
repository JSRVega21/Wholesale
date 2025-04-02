using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wholesale.Server.Data;
using Wholesale.Models;

namespace Wholesale.Server.Repository
{
    public class VisitDetailRepository : IRepository<VisitDetail, int>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public VisitDetailRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public VisitDetail Add(VisitDetail entity)
        {
            var db = _factory.CreateDbContext();
            entity.Initialize();
            db.VisitDetails.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public async Task<VisitDetail> AddAsync(VisitDetail entity)
        {
            var db = _factory.CreateDbContext();
            entity.Initialize();
            db.VisitDetails.Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public void Delete(int key)
        {
            var db = _factory.CreateDbContext();
            var entity = db.VisitDetails.Find(key);
            db.VisitDetails.Remove(entity);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int key)
        {
            var db = _factory.CreateDbContext();
            var entity = await db.VisitDetails.FindAsync(key);
            db.VisitDetails.Remove(entity);
            await db.SaveChangesAsync();
        }

        public VisitDetail GetByKey(int key)
        {
            return GetByKey(key, true);
        }

        public VisitDetail GetByKey(int key, bool tracking = true)
        {
            var db = _factory.CreateDbContext();
            var query = db.VisitDetails.Include(d => d.VisitHeader);

            return tracking
                ? query.FirstOrDefault(x => x.VisitDetailId == key)
                : query.AsNoTracking().FirstOrDefault(x => x.VisitDetailId == key);
        }

        public async Task<VisitDetail> GetByKeyAsync(int key)
        {
            return await GetByKeyAsync(key, true);
        }

        public async Task<VisitDetail> GetByKeyAsync(int key, bool tracking = true)
        {
            var db = _factory.CreateDbContext();
            var query = db.VisitDetails.Include(d => d.VisitHeader);

            return tracking
                ? await query.FirstOrDefaultAsync(x => x.VisitDetailId == key)
                : await query.AsNoTracking().FirstOrDefaultAsync(x => x.VisitDetailId == key);
        }

        public IList<VisitDetail> GetList()
        {
            var db = _factory.CreateDbContext();
            return db.VisitDetails
                     .Include(d => d.VisitHeader)
                     .OrderByDescending(d => d.VisitDetailId)
                     .ToList();
        }
        public async Task<IList<VisitDetail>> GetListAsync()
        {
            var db = _factory.CreateDbContext();
            return await db.VisitDetails
                           .Include(d => d.VisitHeader)
                           .OrderByDescending(d => d.VisitDetailId)
                           .ToListAsync();
        }


        public VisitDetail Update(VisitDetail entity)
        {
            var db = _factory.CreateDbContext();
            entity.Updated();
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return entity;
        }

        public async Task<VisitDetail> UpdateAsync(VisitDetail entity)
        {
            var db = _factory.CreateDbContext();
            entity.Updated();
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return entity;
        }
    }
}

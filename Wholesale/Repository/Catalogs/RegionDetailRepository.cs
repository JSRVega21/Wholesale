using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wholesale.Server.Data;
using Wholesale.Models;

namespace Wholesale.Server.Repository
{
    public class RegionDetailRepository : IRepository<RegionDetail, int>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public RegionDetailRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public RegionDetail Add(RegionDetail entity)
        {
            var db = _factory.CreateDbContext();
            entity.Initialize();
            db.RegionDetails.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public async Task<RegionDetail> AddAsync(RegionDetail entity)
        {
            var db = _factory.CreateDbContext();
            entity.Initialize();
            db.RegionDetails.Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public void Delete(int key)
        {
            var db = _factory.CreateDbContext();
            var entity = db.RegionDetails.Find(key);
            db.RegionDetails.Remove(entity);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int key)
        {
            var db = _factory.CreateDbContext();
            var entity = await db.RegionDetails.FindAsync(key);
            db.RegionDetails.Remove(entity);
            await db.SaveChangesAsync();
        }

        public RegionDetail GetByKey(int key)
        {
            return GetByKey(key, true);
        }

        public RegionDetail GetByKey(int key, bool tracking = true)
        {
            var db = _factory.CreateDbContext();
            var query = db.RegionDetails.Include(d => d.RegionHeader);

            return tracking
                ? query.FirstOrDefault(x => x.RouteId == key)
                : query.AsNoTracking().FirstOrDefault(x => x.RouteId == key);
        }

        public async Task<RegionDetail> GetByKeyAsync(int key)
        {
            return await GetByKeyAsync(key, true);
        }

        public async Task<RegionDetail> GetByKeyAsync(int key, bool tracking = true)
        {
            var db = _factory.CreateDbContext();
            var query = db.RegionDetails.Include(d => d.RegionHeader);

            return tracking
                ? await query.FirstOrDefaultAsync(x => x.RouteId == key)
                : await query.AsNoTracking().FirstOrDefaultAsync(x => x.RouteId == key);
        }

        public IList<RegionDetail> GetList()
        {
            var db = _factory.CreateDbContext();
            return db.RegionDetails.Include(d => d.RegionHeader).ToList();
        }

        public async Task<IList<RegionDetail>> GetListAsync()
        {
            var db = _factory.CreateDbContext();
            return await db.RegionDetails.Include(d => d.RegionHeader).ToListAsync();
        }

        public RegionDetail Update(RegionDetail entity)
        {
            var db = _factory.CreateDbContext();
            entity.Updated();
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return entity;
        }

        public async Task<RegionDetail> UpdateAsync(RegionDetail entity)
        {
            var db = _factory.CreateDbContext();
            entity.Updated();
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return entity;
        }
    }
}

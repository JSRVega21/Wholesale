using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wholesale.Server.Data;
using Wholesale.Models;

namespace Wholesale.Server.Repository
{
    public class RegionHeaderRepository : IRepository<RegionHeader, int>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;
        public RegionHeaderRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public RegionHeader Add(RegionHeader entity)
        {
            var db = _factory.CreateDbContext();
            entity.Initialize();
            db.RegionHeaders.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public async Task<RegionHeader> AddAsync(RegionHeader entity)
        {
            var db = _factory.CreateDbContext();
            entity.Initialize();
            db.RegionHeaders.Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public void Delete(int key)
        {
            var db = _factory.CreateDbContext();
            var entity = db.RegionHeaders.Find(key);
            db.RegionHeaders.Remove(entity);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int key)
        {
            var db = _factory.CreateDbContext();
            var entity = await db.RegionHeaders.FindAsync(key);
            db.RegionHeaders.Remove(entity);
            await db.SaveChangesAsync();
        }

        public RegionHeader GetByKey(int key)
        {
            return GetByKey(key, true);
        }

        public RegionHeader GetByKey(int key, bool tracking = true)
        {
            var db = _factory.CreateDbContext();
            var query = db.RegionHeaders.Include(v => v.Details);

            return tracking
                ? query.FirstOrDefault(x => x.RegionId == key)
                : query.AsNoTracking().FirstOrDefault(x => x.RegionId == key);
        }

        public async Task<RegionHeader> GetByKeyAsync(int key)
        {
            return await GetByKeyAsync(key, true);
        }

        public async Task<RegionHeader> GetByKeyAsync(int key, bool tracking = true)
        {
            var db = _factory.CreateDbContext();
            var query = db.RegionHeaders.Include(v => v.Details);

            return tracking
                ? await query.FirstOrDefaultAsync(x => x.RegionId == key)
                : await query.AsNoTracking().FirstOrDefaultAsync(x => x.RegionId == key);
        }

        public IList<RegionHeader> GetList()
        {
            var db = _factory.CreateDbContext();
            return db.RegionHeaders
                .Include(v => v.Details)
                .OrderBy(d => d.RegionName)
                .ToList();
        }

        public async Task<IList<RegionHeader>> GetListAsync()
        {
            var db = _factory.CreateDbContext();
            return await db.RegionHeaders
                .Include(v => v.Details)
                .OrderBy(d => d.RegionName)
                .ToListAsync();
        }

        public RegionHeader Update(RegionHeader entity)
        {
            var db = _factory.CreateDbContext();
            entity.Updated();
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return entity;
        }

        public async Task<RegionHeader> UpdateAsync(RegionHeader entity)
        {
            var db = _factory.CreateDbContext();
            entity.Updated();
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return entity;
        }

        public ApplicationDbContext GetDbContext()
        {
            return _factory.CreateDbContext();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wholesale.Server.Data;
using Wholesale.Models;

namespace Wholesale.Server.Repository
{
    public class VisitHeaderRepository : IVisitHeaderRepository<VisitHeader, int>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;
        public VisitHeaderRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public VisitHeader Add(VisitHeader entity)
        {
            var db = _factory.CreateDbContext();
            entity.Initialize();
            db.VisitHeaders.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public async Task<VisitHeader> AddAsync(VisitHeader entity)
        {
            var db = _factory.CreateDbContext();
            entity.Initialize();
            db.VisitHeaders.Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public void Delete(int key)
        {
            var db = _factory.CreateDbContext();
            var entity = db.VisitHeaders.Find(key);
            db.VisitHeaders.Remove(entity);
            db.SaveChanges();
        }

        public async Task DeleteAsync(int key)
        {
            var db = _factory.CreateDbContext();
            var entity = await db.VisitHeaders.FindAsync(key);
            db.VisitHeaders.Remove(entity);
            await db.SaveChangesAsync();
        }

        public VisitHeader GetByKey(int key)
        {
            return GetByKey(key, true);
        }

        public VisitHeader GetByKey(int key, bool tracking = true)
        {
            var db = _factory.CreateDbContext();
            var query = db.VisitHeaders.Include(v => v.Details);

            return tracking
                ? query.FirstOrDefault(x => x.VisitHeaderId == key)
                : query.AsNoTracking().FirstOrDefault(x => x.VisitHeaderId == key);
        }

        public async Task<VisitHeader> GetByKeyAsync(int key)
        {
            return await GetByKeyAsync(key, true);
        }

        public async Task<VisitHeader> GetByKeyAsync(int key, bool tracking = true)
        {
            var db = _factory.CreateDbContext();
            var query = db.VisitHeaders.Include(v => v.Details);

            return tracking
                ? await query.FirstOrDefaultAsync(x => x.VisitHeaderId == key)
                : await query.AsNoTracking().FirstOrDefaultAsync(x => x.VisitHeaderId == key);
        }

        public IList<VisitHeader> GetList()
        {
            var db = _factory.CreateDbContext();
            return db.VisitHeaders
                .Include(v => v.Details)
                .OrderByDescending(v => v.VisitHeaderId)
                .ToList();
        }

        public async Task<IList<VisitHeader>> GetListAsync()
        {
            var db = _factory.CreateDbContext();
            return await db.VisitHeaders
                .Include(v => v.Details)
                .OrderByDescending(v => v.VisitHeaderId)
                .ToListAsync();
        }

        public VisitHeader Update(VisitHeader entity)
        {
            var db = _factory.CreateDbContext();
            entity.Updated();
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
            return entity;
        }

        public async Task<VisitHeader> UpdateAsync(VisitHeader entity)
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

        public IEnumerable<VisitHeader> GetBySalespersonOrPos(int? slpcode, int? codigopos)
        {
            using var db = _factory.CreateDbContext();
            var query = db.VisitHeaders.Include(v => v.Details).AsQueryable();

            if (slpcode.HasValue)
            {
                query = query.Where(v => v.Slpcode == slpcode.Value);
            }

            if (codigopos.HasValue)
            {
                query = query.Where(v => v.U_CodigoPOS == codigopos.Value);
            }

            return query.ToList();
        }

    }
}

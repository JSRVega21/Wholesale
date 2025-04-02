using Wholesale.Models;
using Wholesale.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Wholesale.Server.Repository;


namespace Wholesale.Server.Repository
{
    public class VisitRepository : IVisitRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public VisitRepository(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<VisitHeader> GetFiltered(
            int? id,
            string? region,
            string? routes,
            DateTime? startDate,
            DateTime? endDate,
            int? SlpCode,
            int? U_CodigoPOS,
            string? SlpName,
            string? SalespersonCode,
            string? SalespersonName)
        {
            using var db = _factory.CreateDbContext();

            var query = db.VisitHeaders.Include(v => v.Details).AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(v => v.VisitHeaderId == id.Value);
            }

            if (!string.IsNullOrEmpty(region))
            {
                query = query.Where(v => v.Region.Contains(region));
            }

            if (!string.IsNullOrEmpty(routes))
            {
                query = query.Where(v => v.Routes.Contains(routes));
            }

            if (SlpCode.HasValue)
            {
                query = query.Where(v => v.Slpcode == SlpCode.Value);
            }

            if (U_CodigoPOS.HasValue)
            {
                query = query.Where(v => v.U_CodigoPOS == U_CodigoPOS.Value);
            }

            if (!string.IsNullOrEmpty(SlpName))
            {
                query = query.Where(v => v.SlpName.Contains(SlpName));
            }

            if (startDate.HasValue)
            {
                query = query.Where(v => v.CheckInTime >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(v => v.CheckOutTime <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(SalespersonCode))
            {
                query = query.Where(v => v.Details.Any(d => d.SalespersonCode.Contains(SalespersonCode)));
            }

            if (!string.IsNullOrEmpty(SalespersonName))
            {
                query = query.Where(v => v.Details.Any(d => d.SalespersonCode.Contains(SalespersonName)));
            }

            return query.ToList();
        }
    }
}

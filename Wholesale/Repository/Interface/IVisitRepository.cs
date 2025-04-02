using System.Collections.Generic;
using System.Threading.Tasks;
using Wholesale.Models;
using Wholesale.Server.Data;

namespace Wholesale.Server.Repository
{
    public interface IVisitRepository
    {
        IEnumerable<VisitHeader> GetFiltered(
            int? id,
            string? region,
            string? routes,
            DateTime? startDate,
            DateTime? endDate,
            int? SlpCode,
            int? U_CodigoPOS,
            string? SlpName,
            string? SalespersonCode,
            string? SalespersonName);
    }
}


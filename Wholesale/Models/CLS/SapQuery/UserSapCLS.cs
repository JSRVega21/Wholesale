using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesale.Models
{
    public class UserSapCLS
    {
        public string SlpCode { get; set; }
        public string U_CodigoPOS { get; set; }
        public string SlpName { get; set; }
        public string U_Region { get; set; }
        public string U_RegionMayoreo { get; set; }
    }
}
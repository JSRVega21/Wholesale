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
    public class ClientsCLS
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public int SlpCode { get; set; }
        public string U_CodigoPOS { get; set; }
        public string SlpName { get; set; }
        public string U_Region { get; set; }
        public string U_RegionMayoreo { get; set; }
        public string Address { get; set; }
    }

    public class ClientHeaderCLS
    {
        public int SlpCode { get; set; }
        public string SlpName { get; set; }
        public string U_CodigoPOS { get; set; }
        public string U_Region { get; set; }
        public string U_RegionMayoreo { get; set; }
        public List<ClientDetailCLS> Clients { get; set; }
    }

    public class ClientDetailCLS
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string Phone1 { get; set; }
        public string LicTradNum { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}
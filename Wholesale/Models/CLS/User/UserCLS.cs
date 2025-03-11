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
    public class UserCLS
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? SlpCode { get; set; }
        public string? U_CodigoPOS { get; set; }
        public string? SlpName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }

        [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault)]
        public string? UserPassword { get; set; }

        public int? UserRoleId { get; set; }
        public string? UserRole { get; set; }
    }

}

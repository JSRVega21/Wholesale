using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wholesale.Models;

namespace Wholesale.Models
{
    public class RegionDetail : IRecordLogger
    {
        [ForeignKey("RegionHeader")]
        public int RegionId { get; set; }
        [Key]
        public int RouteId { get; set; }
        [Required]
        public string NameRoute { get; set; }
        public RecordLog? RecordLog { get; set; } = new RecordLog();
        public virtual RegionHeader RegionHeader { get; set; }
    }
}

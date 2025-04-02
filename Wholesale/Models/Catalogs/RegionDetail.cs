using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wholesale.Models;
using System.Text.Json.Serialization;

namespace Wholesale.Models
{
    public class RegionDetail : IRecordLogger
    {
        [Key]
        public int RouteId { get; set; }
        [ForeignKey(nameof(RegionHeader))]
        public int RegionId { get; set; }
        [JsonIgnore]
        public virtual RegionHeader? RegionHeader { get; set; }
        [Required]
        public string NameRoute { get; set; }
        public RecordLog? RecordLog { get; set; } = new RecordLog();

    }
}

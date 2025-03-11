using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wholesale.Models;

namespace Wholesale.Models
{
    public class RegionHeader : IRecordLogger
    {
        [Key]
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public RecordLog? RecordLog { get; set; } = new RecordLog();
        public virtual ICollection<RegionDetail> Details { get; set; } = new List<RegionDetail>();

    }
}


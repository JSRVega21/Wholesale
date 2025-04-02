using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wholesale.Models
{
    public class VisitHeader : IRecordLogger
    {
        [Key]
        public int VisitHeaderId { get; set; }
        public int Slpcode { get; set; }
        public string SlpName { get; set; }
        public int U_CodigoPOS { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        [Required(ErrorMessage = "Se debe seleccionar una ruta")]
        public string? Region { get; set; }
        public string? Routes { get; set; }
        public float? TotalVisits { get; set; }
        [NotMapped]
        public int SumTotalVisits => Details?.Count() ?? 0;
        public List<VisitDetail>? Details { get; set; }
        public RecordLog? RecordLog { get; set; } = new RecordLog();
    }
}

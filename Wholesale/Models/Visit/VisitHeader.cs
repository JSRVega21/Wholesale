using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wholesale.Models
{
    public class VisitHeader : IRecordLogger
    {
        [Key]
        public int VisitHeaderId { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        [Required(ErrorMessage = "Se debe seleccionar una ruta")]
        public string? Region { get; set; }
        public string? Routes { get; set; }
        public float? TotalVisits { get; set; }
        public List<VisitDetail>? Details { get; set; }
        public RecordLog? RecordLog { get; set; } = new RecordLog();
    }
}

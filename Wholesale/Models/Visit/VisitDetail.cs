using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wholesale.Models
{
    public class VisitDetail : IRecordLogger
    {
        [Key]
        public int VisitDetailId { get; set; }
        [Required]
        [ForeignKey(nameof(VisitHeader))]
        public int VisitHeaderId { get; set; }
        public VisitHeader? VisitHeader { get; set; }
        [Required(ErrorMessage = "Se debe seleccionar un cliente")]
        public int? SalespersonCode { get; set; }
        public string? SalespersonName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[]? Photo { get; set; }
        public string? Coordinates { get; set; }
        public string? Comment { get; set; }
        public RecordLog? RecordLog { get; set; } = new RecordLog();
    }
}

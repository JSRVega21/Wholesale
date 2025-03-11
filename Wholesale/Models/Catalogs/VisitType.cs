using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wholesale.Models;

namespace Wholesale.Models
{
    public class VisitType : IRecordLogger
    {
        public int VisitTypeId { get; set; }
        public string Description { get; set; }
        public RecordLog? RecordLog { get; set; } = new RecordLog();
    }
}


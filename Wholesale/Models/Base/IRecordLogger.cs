﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wholesale.Models
{
    public interface IRecordLogger
    {
        public RecordLog RecordLog { get; set; }
    }
}

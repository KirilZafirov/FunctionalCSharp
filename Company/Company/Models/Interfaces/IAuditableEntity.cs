﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Models.Interfaces
{
   public interface IAuditableEntity
   {
      string CreatedBy { get; set; }
      string UpdatedBy { get; set; }
      DateTime CreatedDate { get; set; }
      DateTime UpdatedDate { get; set; }
   }
}

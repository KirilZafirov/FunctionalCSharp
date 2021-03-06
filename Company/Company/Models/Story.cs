﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Models
{
   public class Story : AuditableEntity
   {
      public int Id { get; set; }

      public string Lang { get; set; }

      public string Heading { get; set; }

      public string Description { get; set; }

      public DateTime DateCreated { get; set; }

      public DateTime DateModified { get; set; }

      public string FileName { get; set; }

      public byte[] Image { get; set; }

      public virtual int ImageId { get; set; }

   }
}

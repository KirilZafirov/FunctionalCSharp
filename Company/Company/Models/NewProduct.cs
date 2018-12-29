using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Models
{
   public class NewProduct : AuditableEntity
   {
      public int Id { get; set; }

      public string Lang { get; set; }

      public string Name { get; set; }

      public string Code { get; set; }

      public string CorrespondsTo { get; set; }

      public string Description { get; set; }
      
      public byte[] Image { get; set; }

      public virtual int ImageId { get; set; }

   }
}

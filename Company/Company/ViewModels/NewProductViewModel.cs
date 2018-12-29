using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.ViewModels
{
   public class NewProductViewModel
   {
      public int Id { get; set; }

      public string Lang { get; set; }

      public string Name { get; set; }

      public string Code { get; set; }

      public string CorrespondsTo { get; set; }

      public string Description { get; set; }

      public DateTime CreatedDate { get; set; }

      public byte[] Image { get; set; }

      public virtual int ImageId { get; set; }
   }
}

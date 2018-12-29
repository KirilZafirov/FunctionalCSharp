using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Models
{
   public class FileHolder
   {
      public int Id { get; set; }

      public byte[] File { get; set; }

      public string Name { get; set; }

      public bool SliderImage { get; set; }
   }
}

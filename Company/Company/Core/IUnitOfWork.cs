using Company.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Core
{
   public interface IUnitOfWork
   {
      IStoryRepository Stories { get; }
      
      int SaveChanges();
   }
}

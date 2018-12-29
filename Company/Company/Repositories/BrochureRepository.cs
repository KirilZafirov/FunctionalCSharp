using Company.Models;
using Company.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Repositories
{
   public class BrochureRepository : Repository<Brochure>, IBrochureRepository
   {
      public BrochureRepository(DbContext context) : base(context)
      { }

      public IEnumerable<Brochure> GetAllBrochures()
      {
         return _appContext.AppBrochures
             .OrderBy(c => c.CreatedDate)
             .ToList();
      }
      
      private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
   }
}

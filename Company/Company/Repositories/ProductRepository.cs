using Company.Models;
using Company.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Repositories
{
   public class ProductRepository : Repository<NewProduct>, IProductRepository
   {
      public ProductRepository(DbContext context) : base(context)
      { }

      public IEnumerable<NewProduct> GetAllProducts()
      {
         return _appContext.AppProducts
             .OrderBy(c => c.CreatedDate)
             .ToList();
      }



      private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
   }
}

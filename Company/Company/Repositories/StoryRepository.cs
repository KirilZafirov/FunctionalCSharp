using Company.Models;
using Company.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Repositories
{
   public class StoryRepository : Repository<Story>, IStoryRepository
   {
      public StoryRepository(DbContext context) : base(context)
      { }

      public IEnumerable<Story> GetAllStories()
      {
         return _appContext.AppStories
             .OrderBy(c => c.DateCreated)
             .ToList();
      }



      private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
   }
}

using Company.Models;
using Company.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Repositories
{
   public class FileRepository : Repository<FileHolder>, IFileRepository
   {
      public FileRepository(DbContext context) : base(context)
      { }



      private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
   }
}

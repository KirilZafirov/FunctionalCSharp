using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Models
{
   public class ApplicationContext: IdentityDbContext<User>, IApplicationContext
   {

      public ApplicationContext(DbContextOptions options): base(options)
      {

      }



      public DbSet<User> UsersDB { get; set; }
      public DbSet<Story> Story { get; set; }

      public new void SaveChanges()

      {

         base.SaveChanges();

      }

      public new DbSet<T> Set<T>() where T : class

      {

         return base.Set<T>();

      }
      
   }
}

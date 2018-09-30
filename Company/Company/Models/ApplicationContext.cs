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
      private IDbContextTransaction dbContextTransaction;

      public ApplicationContext(DbContextOptions options)

          : base(options)

      {

      }



      public DbSet<User> UsersDB { get; set; }

      public new void SaveChanges()

      {

         base.SaveChanges();

      }

      public new DbSet<T> Set<T>() where T : class

      {

         return base.Set<T>();

      }

      public void BeginTransaction()

      {

         dbContextTransaction = Database.BeginTransaction();

      }

      public void CommitTransaction()

      {

         if (dbContextTransaction != null)

         {

            dbContextTransaction.Commit();

         }

      }

      public void RollbackTransaction()

      {

         if (dbContextTransaction != null)

         {

            dbContextTransaction.Rollback();

         }

      }

      public void DisposeTransaction()

      {

         if (dbContextTransaction != null)

         {

            dbContextTransaction.Dispose();

         }

      }
   }
}

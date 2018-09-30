using Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Repositories
{
   public class UserRepository : IUserRepository
   {
      ApplicationContext _context;
      public UserRepository(ApplicationContext context)

      {
         _context = context;
      }

      public User Save(User domain)

      {
         return new User();

      }

      public bool Update(User domain)

      {

         return true;

      }

      public bool Delete(int id)

      {
         return false;
      }

      public List<User> GetAll()
      {
         try
         {
            return _context.UsersDB.OrderBy(x => x.Name).ToList();

         }

         catch (Exception ex)

         {

            //ErrorManager.ErrorHandler.HandleError(ex);

            throw ex;

         }

      }
   }
}

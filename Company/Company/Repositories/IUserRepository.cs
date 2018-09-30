using Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Repositories
{
   public interface IUserRepository
   {
     User Save(User domain);

     bool Update(User domain);

     bool Delete(int id);

     List<User> GetAll();
   }
}

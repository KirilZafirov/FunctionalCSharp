using Company.Models;
using Company.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Services
{
   public class UserService : IUserService
   {
      private UserRepository repository;

      public UserService(UserRepository userRepository)

      {

         repository = userRepository;

      }

      public User Create(User domain)

      {

         return repository.Save(domain);

      }

      public bool Update(User domain)

      {

         return repository.Update(domain);

      }

      public bool Delete(int id)

      {

         return repository.Delete(id);

      }

      public List<User> GetAll()

      {

         return repository.GetAll();

      }
   }
}

using Company.Models;
using Company.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Maps
{
   public interface IUserMap
   {

      UserViewModel Create(UserViewModel viewModel);

      bool Update(UserViewModel viewModel);

      bool Delete(int id);

      List<UserViewModel> GetAll();

      UserViewModel DomainToViewModel(User domain);

      List<UserViewModel> DomainToViewModel(List<User> domain);

      User ViewModelToDomain(UserViewModel officeViewModel);
   }
}

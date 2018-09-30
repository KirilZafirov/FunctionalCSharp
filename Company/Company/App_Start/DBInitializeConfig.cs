using Company.Maps;
using Company.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.App_Start
{
   public class DBInitializeConfig
   {
      private IUserMap userMap;

      public DBInitializeConfig(IUserMap _userMap)
      {
         userMap = _userMap;
      }

      public void DataTest()

      {

         Users();

      }

      private void Users()

      {

         userMap.Create(new UserViewModel() { Id = 1, Name = "Pablo" });

         userMap.Create(new UserViewModel() { Id = 2, Name = "Diego" });

      }
   }
}

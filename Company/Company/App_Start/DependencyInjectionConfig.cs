using Company.Maps;
using Company.Models;
using Company.Repositories;
using Company.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Company.App_Start
{
   public class DependencyInjectionConfig
   {
      public static void AddScope(IServiceCollection services)

      {

         services.AddScoped<IApplicationContext, ApplicationContext>();

         services.AddScoped<IUserMap, UserMap>();

         services.AddScoped<IUserService, UserService>();

         services.AddScoped<IUserRepository, UserRepository>();

      }
   }
}

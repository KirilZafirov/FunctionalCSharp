
using Company.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Company.Core;
using Company.Core.Interfaces;

namespace Company.Models
{
   public interface IDatabaseInitializer
   {
      Task SeedAsync();
   }




   public class DatabaseInitializer : IDatabaseInitializer
   {
      private readonly ApplicationDbContext _context;
      private readonly IAccountManager _accountManager;
      private readonly ILogger _logger;

      public DatabaseInitializer(ApplicationDbContext context, IAccountManager accountManager, ILogger<DatabaseInitializer> logger)
      {
         _accountManager = accountManager;
         _context = context;
         _logger = logger;
      }

      public async Task SeedAsync()
      {
         await _context.Database.MigrateAsync().ConfigureAwait(false);

         if (!await _context.Users.AnyAsync())
         {
            _logger.LogInformation("Generating inbuilt accounts");

            const string adminRoleName = "administrator";
            const string userRoleName = "user";

            await EnsureRoleAsync(adminRoleName, "Default administrator", ApplicationPermissions.GetAllPermissionValues());
            await EnsureRoleAsync(userRoleName, "Default user", new string[] { });

            await CreateUserAsync("admin", "tempP@ss123", "Inbuilt Administrator", "admin@kirilzafirov.com", "+1 (123) 000-0000", new string[] { adminRoleName });
            await CreateUserAsync("user", "tempP@ss123", "Inbuilt Standard User", "user@ebenmonney.com", "+1 (123) 000-0001", new string[] { userRoleName });

            _logger.LogInformation("Inbuilt account generation completed");
         }



         if (!await _context.AppStories.AnyAsync())
         {
            _logger.LogInformation("Seeding initial data");

            Story story_1 = new Story
            {
               Lang = "en",
               Description = "Something Here _1",
               DateCreated = DateTime.UtcNow,
               DateModified = DateTime.UtcNow
            };

            Story story_2 = new Story
            {
               Lang = "en",
               Description = "Something Here _2",
               DateCreated = DateTime.UtcNow,
               DateModified = DateTime.UtcNow
            };

            Story story_3 = new Story
            {
               Lang = "en",
               Description = "Something Here _3",
               DateCreated = DateTime.UtcNow,
               DateModified = DateTime.UtcNow
            };

            Story story_4 = new Story
            {
               Lang = "en",
               Description = "Something Here _4",
               DateCreated = DateTime.UtcNow,
               DateModified = DateTime.UtcNow
            };



            Story story_5 = new Story
            {
               Lang = "en",
               Description = "Something Here _5",
               DateCreated = DateTime.UtcNow,
               DateModified = DateTime.UtcNow
            };

            Story story_6 = new Story
            {
               Lang = "en",
               Description = "Something Here _6",
               DateCreated = DateTime.UtcNow,
               DateModified = DateTime.UtcNow
            };




            _context.AppStories.Add(story_1);
            _context.AppStories.Add(story_2);
            _context.AppStories.Add(story_3);
            _context.AppStories.Add(story_4);
            _context.AppStories.Add(story_5);
            _context.AppStories.Add(story_6);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Seeding initial data completed");
         }
      }



      private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
      {
         if ((await _accountManager.GetRoleByNameAsync(roleName)) == null)
         {
            ApplicationRole applicationRole = new ApplicationRole(roleName, description);

            var result = await this._accountManager.CreateRoleAsync(applicationRole, claims);

            if (!result.Item1)
               throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");
         }
      }

      private async Task<ApplicationUser> CreateUserAsync(string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
      {
         ApplicationUser applicationUser = new ApplicationUser
         {
            UserName = userName,
            FullName = fullName,
            Email = email,
            PhoneNumber = phoneNumber,
            EmailConfirmed = true,
            IsEnabled = true
         };

         var result = await _accountManager.CreateUserAsync(applicationUser, roles, password);

         if (!result.Item1)
            throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Item2)}");


         return applicationUser;
      }
   }
}

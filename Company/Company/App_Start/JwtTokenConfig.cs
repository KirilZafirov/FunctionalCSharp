using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Company.App_Start
{
   public class JwtTokenConfig
   {
      public static void AddAuthentication(IServiceCollection services, IConfiguration configuration)

      {

         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>

         {

            options.TokenValidationParameters = new TokenValidationParameters

            {

               ValidateIssuer = true,

               ValidateAudience = true,

               ValidateLifetime = true,

               ValidateIssuerSigningKey = true,

               ValidIssuer = configuration["Jwt:Issuer"],

               ValidAudience = configuration["Jwt:Issuer"],

               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))

            };

            services.AddCors();

         });
      }
   }
}

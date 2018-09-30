using Company.App_Start;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Company
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         DependencyInjectionConfig.AddScope(services);

         JwtTokenConfig.AddAuthentication(services, Configuration);

         DBContextConfig.Initialize(services, Configuration);
         
         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

         // In production, the Angular files will be served from this directory
         services.AddSpaStaticFiles(configuration =>
         {
            configuration.RootPath = "ClientApp/dist";
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env , IServiceProvider svp)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
         }

         DBContextConfig.Initialize(Configuration, env, svp);

         app.UseCors(builder => builder
                                  .AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowCredentials());

         app.UseAuthentication();

         app.UseHttpsRedirection();
         app.UseStaticFiles();
         app.UseSpaStaticFiles();

         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller}/{action=Index}/{id?}");
         });

         app.UseSpa(spa =>
         {
               // To learn more about options for serving an Angular SPA from ASP.NET Core,
               // see https://go.microsoft.com/fwlink/?linkid=864501

               spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
               spa.UseAngularCliServer(npmScript: "start");
            }
         });
      }
   }
}

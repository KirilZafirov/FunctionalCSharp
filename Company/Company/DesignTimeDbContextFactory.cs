﻿using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Company.Models;

namespace Company
{
   public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
   {
      public ApplicationDbContext CreateDbContext(string[] args)
      {
         Mapper.Reset();

         IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .AddJsonFile("appsettings.Development.json", optional: true)
             .Build();

         var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

         builder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"], b => b.MigrationsAssembly("Company"));
         builder.UseOpenIddict();

         return new ApplicationDbContext(builder.Options);
      }
   }
}
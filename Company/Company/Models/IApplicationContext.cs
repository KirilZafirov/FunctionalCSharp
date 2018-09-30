﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Models
{
   public interface IApplicationContext
   {


      DbSet<User> UsersDB { get; set; }

      void SaveChanges();

      void BeginTransaction();

      void CommitTransaction();

      void RollbackTransaction();

      void DisposeTransaction();
   }
}
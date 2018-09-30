using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization.Controllers
{
   
   public class HomeController : Controller
   {
      [Authorize(Policy = "BuildingEntry")]
      public IActionResult Index()
      {
         return View();
      }
   }
}

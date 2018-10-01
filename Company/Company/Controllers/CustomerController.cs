using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Company.Helpers;
using Company.Core;

namespace Company.Controllers
{
   [Route("api/[controller]")]
   public class CustomerController : Controller
   {
      private IUnitOfWork _unitOfWork;
      readonly ILogger _logger;
      readonly IEmailSender _emailer;


      public CustomerController(IUnitOfWork unitOfWork, ILogger<CustomerController> logger, IEmailSender emailer)
      {
         _unitOfWork = unitOfWork;
         _logger = logger;
         _emailer = emailer;
      }




      // GET: api/values
      [HttpGet]
      public IActionResult Get()
      {
         var allStories = _unitOfWork.Stories.GetAll();
         return Ok(allStories);
      }


      [HttpGet("throw")]
      public IEnumerable<CustomerViewModel> Throw()
      {
         throw new InvalidOperationException("This is a test exception: " + DateTime.Now);
      }
      
      [HttpGet("email")]
      public async Task<string> Email()
      {
         string recepientName = "Kiril Zafirov"; //         <===== Put the recepient's name here
         string recepientEmail = "kiril_zafirov@yahoo.com"; //   <===== Put the recepient's email here

         string message = EmailTemplates.GetTestEmail(recepientName, DateTime.UtcNow);

         (bool success, string errorMsg) = await _emailer.SendEmailAsync(recepientName, recepientEmail, "Test Email from QuickApp", message);

         if (success)
            return "Success";

         return "Error: " + errorMsg;
      }

      
   }
}

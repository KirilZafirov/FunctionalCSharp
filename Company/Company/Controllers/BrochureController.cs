using AutoMapper;
using Company.Core;
using Company.Models;
using Company.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Company.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class BrochureController : Controller
   {
      private IUnitOfWork _unitOfWork;
      readonly ILogger _logger;
      private IHostingEnvironment _env;

      public BrochureController(IUnitOfWork unitOfWork, ILogger<ProductController> logger, IHostingEnvironment env)
      {
         _unitOfWork = unitOfWork;
         _logger = logger;
         _env = env;
      }
      
      // GET: api/values
      [HttpGet]
      [AllowAnonymous]
      public IActionResult Get()
      {
         var allBrochures = _unitOfWork.Brochures.GetAll();
         var brochuresViewModel = new List<BrochuresViewModel>();
         foreach (var brochure in allBrochures)
         {
            brochuresViewModel.Add(Mapper.Map<BrochuresViewModel>(brochure));
         };
         return Ok(brochuresViewModel);
      }

      [HttpPost("AddBrochure")]
      [ProducesResponseType(201, Type = typeof(BrochuresViewModel))]
      [ProducesResponseType(400)]
      [ProducesResponseType(403)]
      public IActionResult AddBrochure([FromBody]BrochuresViewModel brochure)
      {
         if (ModelState.IsValid)
         {
            if (brochure == null)
               return BadRequest($"{nameof(brochure)} cannot be null");

            _unitOfWork.Brochures.Add(Mapper.Map<Brochure>(brochure));
            _unitOfWork.SaveChanges();

            return Ok(brochure);
         }
         return BadRequest(ModelState);
      }

      [HttpPost("UploadFile")]
      public IActionResult UploadFile(IFormFile document)
      {
         if (document == null) throw new Exception("File is null");
         if (document.Length == 0) throw new Exception("File is empty");

         string folderName = "cataloguesAndBrochures";
         string webRootPath = _env.ContentRootPath + "\\ClientApp\\src\\assets\\";
         string newPath = Path.Combine(webRootPath, folderName);
         if (!Directory.Exists(newPath))
         {
            Directory.CreateDirectory(newPath);
         }
         

         string fileName = ContentDispositionHeaderValue.Parse(document.ContentDisposition).FileName.Trim('"');
         string fullPath = Path.Combine(newPath, fileName);

         using (var stream = new FileStream(fullPath, FileMode.Create))
         {
            document.CopyTo(stream);   
         }
         return Ok();
      }
   }

}
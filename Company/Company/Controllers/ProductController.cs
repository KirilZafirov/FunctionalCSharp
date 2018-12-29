using AutoMapper;
using Company.Core;
using Company.Helpers;
using Company.Models;
using Company.ViewModels;
using Microsoft.AspNetCore.Authentication;
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
   public class ProductController : Controller
   {
      private IUnitOfWork _unitOfWork;
      readonly ILogger _logger;
      private IHostingEnvironment _env;

      public ProductController(IUnitOfWork unitOfWork, ILogger<ProductController> logger, IHostingEnvironment env)
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
         var allProducts = _unitOfWork.Products.GetAll();
         var productsViewModel = new List<NewProductViewModel>();
         foreach (var product in allProducts)
         {
            productsViewModel.Add(Mapper.Map<NewProductViewModel>(product));
         };
         return Ok(productsViewModel);
      }
      [HttpGet("GetProductById/{id}")]
      [AllowAnonymous]
      public IActionResult GetProductById(int id)
      {
         var product = new NewProduct();
         try
         {
            product = _unitOfWork.Products.GetSingleOrDefault(p => p.Id == id);
         }
         catch (Exception)
         {
            throw;
         }
         if (product != null)
         {
            var productViewModel = new NewProductViewModel();

            productViewModel = Mapper.Map<NewProductViewModel>(product);

            return Ok(productViewModel);
         }
         return Ok();
      }
      [HttpGet("{lang}")]
      [AllowAnonymous]
      public IActionResult GetProductsByLang(string lang)
      {
         var allProducts= _unitOfWork.Products.GetAll().Where(product => product.Lang == lang);

         var productsViewModel = new List<NewProductViewModel>();
         foreach (var product in allProducts)
         {
            productsViewModel.Add(Mapper.Map<NewProductViewModel>(product));
         };
         return Ok(productsViewModel);
      }
    

      [HttpPost("CreateProduct")]
      [ProducesResponseType(201, Type = typeof(NewProductViewModel))]
      [ProducesResponseType(400)]
      [ProducesResponseType(403)]
      public IActionResult CreateProduct(NewProductViewModel product)
      {
         if (ModelState.IsValid)
         {
            if (product == null)
               return BadRequest($"{nameof(product)} cannot be null");
            
            _unitOfWork.Products.Add(Mapper.Map<NewProduct>(product));
            _unitOfWork.SaveChanges();

            return Ok(product);
         }
         return BadRequest(ModelState);
      }
      [HttpPut("EditProduct")]
      [ProducesResponseType(201, Type = typeof(NewProductViewModel))]
      [ProducesResponseType(400)]
      [ProducesResponseType(403)]
      public IActionResult EditProduct(NewProductViewModel product)
      {

         if (ModelState.IsValid)
         {
            if (product == null)
               return BadRequest($"{nameof(product)} cannot be null");

            var productToEdit = _unitOfWork.Products.Get(product.Id);

            productToEdit.Lang = product.Lang;
            productToEdit.Description = product.Description;
            productToEdit.Code = product.Code;
            productToEdit.CorrespondsTo = product.CorrespondsTo;
            productToEdit.Name = product.Name;

            _unitOfWork.Products.Update(productToEdit);
            _unitOfWork.SaveChanges();

            return Ok(product);
         }
         return BadRequest(ModelState);
      }



      [HttpDelete("DeleteProduct/{id}")]
      [ProducesResponseType(200, Type = typeof(NewProductViewModel))]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public IActionResult DeleteProduct(int id)
      {
         var productToDelete = _unitOfWork.Products.Get(id);


         if (productToDelete != null)
         {
            var imageForProduct = _unitOfWork.Files.Get(productToDelete.ImageId);
            if (imageForProduct != null)
            {
               _unitOfWork.Files.Remove(imageForProduct);
            }
            _unitOfWork.Products.Remove(productToDelete);
            _unitOfWork.SaveChanges();
         }
         return Ok();
      }

   }

}
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
   public class StoryController : Controller
   {
      private IUnitOfWork _unitOfWork;
      readonly ILogger _logger;
      private IHostingEnvironment _env;
      IHttpContextAccessor _httpContextAccessor;

      public StoryController(IUnitOfWork unitOfWork, ILogger<StoryController> logger, 
         IEmailSender emailer, 
         IHostingEnvironment env,
         IHttpContextAccessor httpContextAccessor)
      {
         _unitOfWork = unitOfWork;
         _logger = logger;
         _env = env;
         _httpContextAccessor = httpContextAccessor;
      }




      // GET: api/values
      [HttpGet]
      //[Authorize(Authorization.Policies.ViewAllUsersPolicy)]
      public IActionResult Get()
      {
         var allStories = _unitOfWork.Stories.GetAll();
         var storiesViewModel = new List<StoryViewModel>();
         foreach(var story in allStories)
         {
            storiesViewModel.Add(Mapper.Map<StoryViewModel>(story));
         };
         return Ok(storiesViewModel);
      }
      [HttpGet("GetStoryById/{id}")]
      [AllowAnonymous]
      public IActionResult GetStoryById(int id)
      {
         var story = new Story();
         try
         {
            story = _unitOfWork.Stories.GetSingleOrDefault(s => s.Id == id);
         }
         catch (Exception)
         {

            throw;
         }
         if (story != null)
         {
            var storyViewModel = new StoryViewModel();

            storyViewModel = Mapper.Map<StoryViewModel>(story);

            return Ok(storyViewModel);
         }

         return Ok();
      }
      [HttpGet("{lang}")]
      //[AllowAnonymous]
      public IActionResult GetStoriesByLang(string lang)
      {
         IEnumerable<Story> allStories = null;
         try
         {
           allStories = _unitOfWork.Stories.GetAll().Where(story => story.Lang == lang);
         }
         catch (Exception)
         {

            throw;
         }
         if(allStories != null)
         {
            var storiesViewModel = new List<StoryViewModel>();
            foreach (var story in allStories)
            {
               storiesViewModel.Add(Mapper.Map<StoryViewModel>(story));
            };
            return Ok(storiesViewModel);
         }

         return Ok();
      }
      
      [HttpGet("Files")]
      [AllowAnonymous]
      public IActionResult Files()
      {
         var allFiles = _unitOfWork.Files.GetAll();
       
         return Ok(allFiles);
      }
      [HttpGet("SliderImages")]
      [AllowAnonymous]
      public IActionResult SliderImages()
      {
         try
         {
            var allFiles = _unitOfWork.Files.GetAll().Where(image => image.SliderImage == true);
            return Ok(allFiles);
         }
         catch (Exception)
         {
            return Ok();
            throw;
         }
      }
      [HttpPost("AddSliderImages")]
      public IActionResult AddSliderImages([FromBody]int[] listOfImageIds) 
      {
         var allSliderFiles = _unitOfWork.Files.GetAll().Where(image => image.SliderImage == true);
         
         foreach (var id in listOfImageIds)
         {
           var image = _unitOfWork.Files.Get(id);
           image.SliderImage = true;
           _unitOfWork.Files.Update(image);
         }
         _unitOfWork.SaveChanges();
         return Ok();
      }
      [HttpPost("RemoveSliderImages")]
      public IActionResult RemoveSliderImages([FromBody]int imageId)
      {
         var image = _unitOfWork.Files.Get(imageId);

         image.SliderImage = false;

         _unitOfWork.Files.Update(image);
         _unitOfWork.SaveChanges();
         return Ok();
      }
      [HttpDelete("DeleteImage/{id}")]
      public IActionResult DeleteImage(int id)
      {
          var image = _unitOfWork.Files.Get(id);

         image.SliderImage = false;
         _unitOfWork.Files.Remove(image);
         var storyRelatedToImage = _unitOfWork.Stories.Find(story => story.ImageId == id).SingleOrDefault();
         if (storyRelatedToImage != null)
         {
            _unitOfWork.Stories.Remove(storyRelatedToImage);
         }
         
         _unitOfWork.SaveChanges();
         return Ok();
      }
      [HttpPost("CreateStory")]
      //[Authorize(Authorization.Policies.ViewAllUsersPolicy)]
      [ProducesResponseType(201, Type = typeof(StoryViewModel))]
      [ProducesResponseType(400)]
      [ProducesResponseType(403)]
      public IActionResult CreateStory(StoryViewModel story)
      {
            if (ModelState.IsValid)
            {
               if (story == null)
                  return BadRequest($"{nameof(story)} cannot be null");

              _unitOfWork.Stories.Add(Mapper.Map<Story>(story));
               _unitOfWork.SaveChanges();
          
               return Ok(story);
            }
         return BadRequest(ModelState);
      }
      [HttpPut("EditStory")]
      [ProducesResponseType(201, Type = typeof(StoryViewModel))]
      [ProducesResponseType(400)]
      [ProducesResponseType(403)]
      public IActionResult EditStory(StoryViewModel story)
      {
         
         if (ModelState.IsValid)
         {
            if (story == null)
               return BadRequest($"{nameof(story)} cannot be null");

            var storyToEdit = _unitOfWork.Stories.Get(story.Id);

            storyToEdit.Lang = story.Lang;
            storyToEdit.Description = story.Description;

            _unitOfWork.Stories.Update(storyToEdit);
            _unitOfWork.SaveChanges();

            return Ok(story);
         }
         return BadRequest(ModelState);
      }



      [HttpDelete("DeleteStory/{id}")]
      [ProducesResponseType(200, Type = typeof(StoryViewModel))]
      [ProducesResponseType(400)]
      [ProducesResponseType(404)]
      public IActionResult DeleteStory(int id)
      {
         var storyToDelete = _unitOfWork.Stories.Get(id);

        
         if (storyToDelete != null)
         {
            var imageForStory = _unitOfWork.Files.Get(storyToDelete.ImageId);
            if (imageForStory != null)
            {
               _unitOfWork.Files.Remove(imageForStory);
            }
            _unitOfWork.Stories.Remove(storyToDelete);
            _unitOfWork.SaveChanges();
         }
         return Ok();
      }

      [HttpPost("UploadFile")]
      public async Task<IActionResult> UploadFileAsync(IFormFile image)
      {
         if (image == null) throw new Exception("File is null");
         if (image.Length == 0) throw new Exception("File is empty");

         string folderName = "images";
         string webRootPath = _env.ContentRootPath + "\\ClientApp\\src\\assets\\";
         string newPath = Path.Combine(webRootPath, folderName);
         if (!Directory.Exists(newPath))
         {
            Directory.CreateDirectory(newPath);
         }

         var file = new FileHolder();
         file.Name = image.FileName;
         
         using (var memoryStream = new MemoryStream())
         {
            await image.CopyToAsync(memoryStream);
            file.File = memoryStream.ToArray();
         }
         string fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
         string fullPath = Path.Combine(newPath, fileName);

         using (var stream = new FileStream(fullPath, FileMode.Create))
         {
            image.CopyTo(stream);
         }

         _unitOfWork.Files.Add(file);
         _unitOfWork.SaveChanges();

         return Ok(file);
      }

      [HttpPost("UploadImage")]
      public IActionResult UploadImage(IFormFile image)
      {
         if (image == null) throw new Exception("File is null");
         if (image.Length == 0) throw new Exception("File is empty");

         string folderName = "images";
         string webRootPath = _env.ContentRootPath + "\\ClientApp\\src\\assets\\";
         string newPath = Path.Combine(webRootPath, folderName);

         var file = new FileHolder();
         file.Name = image.FileName;

         if (!Directory.Exists(newPath))
         {
            Directory.CreateDirectory(newPath);
         }
         if (image.Length > 0)
         {
            string fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
            string fullPath = Path.Combine(newPath, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
               image.CopyTo(stream);
            }
         }

         _unitOfWork.Files.Add(file);
         _unitOfWork.SaveChanges();

         return Ok(image);
      }
   }
}
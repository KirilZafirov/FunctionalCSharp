using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Controllers
{
   [Produces("application/json")]
   [Route("api/Story")]
   public class StoryController : Controller
   {
      private readonly ApplicationContext _context;
      public StoryController(ApplicationContext context)
      {
         _context = context;
      }
      
      // GET: api/Stories
      [HttpGet]
      public IEnumerable<Story> GetStory()
      {
         var allStories = new List<Story>();
         allStories.Add(new Story { Id = 0, Lang = "en", Description = "First Story" });
         allStories.Add(new Story { Id = 1, Lang = "en", Description = "Second Story" });
         allStories.Add(new Story { Id = 2, Lang = "en", Description = "Third Story" });
         allStories.Add(new Story { Id = 3, Lang = "en", Description = "Fourth Story" });
         allStories.Add(new Story { Id = 4, Lang = "en", Description = "Fifth Story" });

         return allStories;
      }

      // GET: api/Stories/5
      [HttpGet("{id}")]
      public async Task<IActionResult> GetStory([FromRoute] int id)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         var workout = await _context.Story.SingleOrDefaultAsync(m => m.Id == id);

         if (workout == null)
         {
            return NotFound();
         }

         return Ok(workout);
      }

      // PUT: api/Stories/5
      [HttpPut("{id}")]
      public async Task<IActionResult> PutStory([FromRoute] int id, [FromBody] Story story)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         if (id != story.Id)
         {
            return BadRequest();
         }

         _context.Entry(story).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!StoryExists(id))
            {
               return NotFound();
            }
            else
            {
               throw;
            }
         }

         return NoContent();
      }

      // POST: api/Stories
      [HttpPost]
      public async Task<IActionResult> PostStory([FromBody] Story story)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         _context.Story.Add(story);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetStory", new { id = story.Id }, story);
      }

      // DELETE: api/Stories/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteStory([FromRoute] int id)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }

         var story = await _context.Story.SingleOrDefaultAsync(m => m.Id == id);
         if (story == null)
         {
            return NotFound();
         }

         _context.Story.Remove(story);
         await _context.SaveChangesAsync();

         return Ok(story);
      }

      private bool StoryExists(int id)
      {
         return _context.Story.Any(e => e.Id == id);
      }
   }
}

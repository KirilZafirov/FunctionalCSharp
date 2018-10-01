using Company.Models;
using Company.Repositories;
using Company.Repositories.Interfaces;

namespace Company.Core
{
   public class UnitOfWork : IUnitOfWork
   {
      readonly ApplicationDbContext _context;

      IStoryRepository _storyRepository;

      public UnitOfWork(ApplicationDbContext context)
      {
         _context = context;
      }
      

      public IStoryRepository Stories
      {
         get
         {
            if (_storyRepository == null)
               _storyRepository = new StoryRepository(_context);

            return _storyRepository;
         }
      }

      public int SaveChanges()
      {
         return _context.SaveChanges();
      }
   }
}

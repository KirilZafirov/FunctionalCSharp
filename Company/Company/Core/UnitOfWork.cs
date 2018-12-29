using Company.Models;
using Company.Repositories;
using Company.Repositories.Interfaces;

namespace Company.Core
{
   public class UnitOfWork : IUnitOfWork
   {
      readonly ApplicationDbContext _context;

      IStoryRepository _storyRepository;
      IFileRepository _fileRepository;
      IProductRepository _productRepository;
      IBrochureRepository _brochuresRepository;

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
      public IFileRepository Files
      {
         get
         {
            if (_fileRepository == null)
               _fileRepository = new FileRepository(_context);

            return _fileRepository;
         }
      }
      public IProductRepository Products
      {
         get
         {
            if (_productRepository == null)
               _productRepository = new ProductRepository(_context);

            return _productRepository;
         }
      }
      public IBrochureRepository Brochures
      {
         get
         {
            if (_brochuresRepository == null)
               _brochuresRepository = new BrochureRepository(_context);

            return _brochuresRepository;
         }
      }
      public int SaveChanges()
      {
         return _context.SaveChanges();
      }
   }
}

using Company.Repositories;
using Company.Repositories.Interfaces;

namespace Company.Core
{
   public interface IUnitOfWork
   {
      IStoryRepository Stories { get; }

      IFileRepository Files { get; }

      IProductRepository Products { get; }

      IBrochureRepository Brochures { get; }

      int SaveChanges();
   }
}

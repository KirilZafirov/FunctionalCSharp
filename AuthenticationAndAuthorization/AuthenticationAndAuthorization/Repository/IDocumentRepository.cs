using AuthenticationAndAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization.Repository
{
   public interface IDocumentRepository
   {
      IEnumerable<Document> Get();

      Document Get(int id);
   }
}

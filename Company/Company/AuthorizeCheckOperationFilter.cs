﻿using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Company
{
   internal class AuthorizeCheckOperationFilter : IOperationFilter
   {
      public void Apply(Operation operation, OperationFilterContext context)
      {
         // Check for authorize attribute
         var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
             .Union(context.MethodInfo.GetCustomAttributes(true))
             .OfType<AuthorizeAttribute>()
             .Any();

         if (hasAuthorize)
         {
            operation.Responses.Add("401", new Response { Description = "Unauthorized" });

            operation.Security = new List<IDictionary<string, IEnumerable<string>>>
                {
                    new Dictionary<string, IEnumerable<string>>
                    {
                        { "oauth2", new string [] { } }
                    }
                };
         }
      }
   }
}

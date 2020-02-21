// <copyright file="ServiceCollectionExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
   using System;
   using Microsoft.EntityFrameworkCore;
   using SomeUser.Persistence.SqlServer;

   /// <summary>
   /// Extensions to add SqlServer persistence to a service collection.
   /// </summary>
   public static class ServiceCollectionExtensions
   {
      /// <summary>
      /// Adds Sql Server persistence to the given service collection with the given options.
      /// </summary>
      /// <param name="services">The service collection to register services with.</param>
      /// <param name="options">Options for the Db context.</param>
      /// <returns>The service collection with services configured.</returns>
      public static IServiceCollection AddSomeUserSqlServer(this IServiceCollection services, Action<DbContextOptionsBuilder> options = null)
      {
         services.AddDbContext<SomeUserDbContext>(options);

         return services;
      }

      public static IServiceCollection AddSomeUserSqlServerInMemory(this IServiceCollection services)
      {
         var options = new DbContextOptionsBuilder<SomeUserDbContext>()
                          .UseInMemoryDatabase(databaseName: "TestDb")
                          .Options;
         services.AddSingleton(x => new SomeUserDbContext(options));

         return services;
      }
   }
}

// <copyright file="ServiceCollectionExtensions.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace Microsoft.Extensions.DependencyInjection
{
   using System;
   using Microsoft.EntityFrameworkCore;
   using Microsoft.Extensions.Configuration;
   using SomeUser.Persistence.SqlServer;

   /// <summary>
   /// Extensions to add SqlServer persistence to a service collection.
   /// </summary>
   public static class ServiceCollectionExtensions
   {
      /// <summary>
      /// Adds Sql Server persistence to the given service collection with the given configuration.
      /// </summary>
      /// <param name="services">The service collection to register services with.</param>
      /// <param name="configuration">Configuration to get the connection string from.</param>
      /// <returns>The service collection with services configured.</returns>
      public static IServiceCollection AddSomeUserSqlServer(this IServiceCollection services, IConfiguration configuration)
      {
         string connectionString = configuration.GetConnectionString("SomeUser");
         services.AddDbContext<SomeUserDbContext>((builder) => builder.UseSqlServer(connectionString));

         return services;
      }

      /// <summary>
      /// Adds in memory persistence to the given service collection.
      /// </summary>
      /// <param name="services">The service collection to register services with.</param>
      /// <returns>The service collection with in memory services configured.</returns>
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

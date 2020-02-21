// <copyright file="Program.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api
{
   using Microsoft.AspNetCore.Hosting;
   using Microsoft.Extensions.Hosting;

   /// <summary>
   /// Application entry point.
   /// </summary>
   public class Program
   {
      /// <summary>
      /// Starts the application with the given <paramref name="args"/>.
      /// </summary>
      /// <param name="args">Arguments to configure the behaviour of the application.</param>
      public static void Main(string[] args)
      {
         CreateHostBuilder(args).Build().Run();
      }

      private static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                 webBuilder.UseStartup<Startup>();
              });
   }
}

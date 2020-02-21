// <copyright file="Startup.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api
{
   using AutoMapper;
   using FluentValidation.AspNetCore;
   using Microsoft.AspNetCore.Builder;
   using Microsoft.AspNetCore.Hosting;
   using Microsoft.EntityFrameworkCore;
   using Microsoft.Extensions.Configuration;
   using Microsoft.Extensions.DependencyInjection;
   using Microsoft.Extensions.Hosting;
   using SomeUser.Core;
   using SomeUser.Persistence.SqlServer;
   using SomeUser.Persistence.SqlServer.Mapping;

   /// <summary>
   /// Configures the application.
   /// </summary>
   public class Startup
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="Startup"/> class.
      /// </summary>
      /// <param name="configuration">Application configuration.</param>
      public Startup(IConfiguration configuration)
      {
         this.Configuration = configuration;
      }

      /// <summary>
      /// Gets the application configuration.
      /// </summary>
      public IConfiguration Configuration { get; }

      /// <summary>
      /// Configures the services available in the application.
      /// </summary>
      /// <param name="services">The collection of services to configure.</param>
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddControllers();
         services.AddMvc()
                 .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
         services.AddAutoMapper(typeof(Startup).Assembly, typeof(UserEntityMappingProfile).Assembly);
         services.AddScoped<IUserRepository, UserRepository>();
         services.AddSomeUserSqlServerInMemory();
      }

      /// <summary>
      /// Configures the HTTP request pipeline.
      /// </summary>
      /// <param name="app">The application builder instance to configure.</param>
      /// <param name="env">The host environment.</param>
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}

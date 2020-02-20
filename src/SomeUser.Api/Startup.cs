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

   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         this.Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddControllers();
         services.AddMvc()
                 .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
         services.AddAutoMapper(typeof(Startup));
         services.AddScoped<IUserRepository, UserRepository>();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

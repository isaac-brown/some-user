using System.Collections.Generic;
using System;
// <copyright file="UsersController.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api.Controllers
{
   using System.Threading.Tasks;
   using AutoMapper;
   using Microsoft.AspNetCore.Mvc;
   using SomeUser.Api.Models;
   using SomeUser.Core;

   [ApiController]
   [Route("users")]
   public class UsersController : ControllerBase
   {
      private readonly IUserRepository userRepository;
      private readonly IMapper mapper;

      public UsersController(IUserRepository userRepository, IMapper mapper)
      {
         this.userRepository = userRepository;
         this.mapper = mapper;
      }

      [HttpGet]
      public async Task<IActionResult> FindManyAsync(int? limit)
      {
         if (limit.HasValue && limit < 1)
         {
            return this.BadRequest(new { message = "limit must be greater than 0" });
         }

         IEnumerable<User> users = await this.userRepository.FindManyAsync();
         var response = this.mapper.Map<IEnumerable<FindUserResponse>>(users);

         return this.Ok(response);
      }

      [HttpPost]
      public Task<IActionResult> CreateOneAsync(CreateUserRequest user)
      {
         CreateUserResponse createdUser = new CreateUserResponse
         {
            Id = Guid.NewGuid(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
         };
         return Task.FromResult<IActionResult>(this.CreatedAtRoute(string.Empty, string.Empty, createdUser));
      }

      [HttpPut("{userId:guid}")]
      public async Task<IActionResult> UpdateOneAsync(UpdateUserRequest user, Guid userId)
      {
         if (!await this.userRepository.ExistsAsync(userId))
         {
            return this.NotFound();
         }

         return this.NoContent();
      }

      [HttpGet("{userId:guid}")]
      public async Task<IActionResult> FindOneAsync(Guid userId)
      {
         if (!await this.userRepository.ExistsAsync(userId))
         {
            return this.NotFound();
         }

         return this.Ok();
      }

      [HttpDelete("{userId:guid}")]
      public async Task<IActionResult> DeleteOneAsync(Guid userId)
      {
         if (!await this.userRepository.ExistsAsync(userId))
         {
            return this.NotFound();
         }

         return this.NoContent();
      }
   }
}

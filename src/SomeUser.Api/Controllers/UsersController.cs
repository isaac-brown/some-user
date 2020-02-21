// <copyright file="UsersController.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api.Controllers
{
   using System;
   using System.Collections.Generic;
   using System.Threading.Tasks;
   using AutoMapper;
   using Microsoft.AspNetCore.Mvc;
   using SomeUser.Api.Models;
   using SomeUser.Core;

   /// <summary>
   /// Provides HTTP operations for working with <see cref="User"/> objects.
   /// </summary>
   [ApiController]
   [Route("users")]
   public class UsersController : ControllerBase
   {
      private readonly IUserService userService;
      private readonly IMapper mapper;

      /// <summary>
      /// Initializes a new instance of the <see cref="UsersController"/> class.
      /// </summary>
      /// <param name="userRepository">The repository of users.</param>
      /// <param name="mapper">The mapper to be used.</param>
      public UsersController(IUserService userRepository, IMapper mapper)
      {
         this.userService = userRepository;
         this.mapper = mapper;
      }

      /// <summary>
      /// Retrieves many users.
      /// </summary>
      /// <param name="limit">The maximum number of records to use.</param>
      /// <returns>200 Ok with users in the body.</returns>
      [HttpGet]
      public async Task<IActionResult> FindManyAsync(int limit = 1000)
      {
         if (limit < 1)
         {
            return this.BadRequest(new { message = "limit must be greater than 0" });
         }

         FindManyUsersContext findManyUsersContext = new FindManyUsersContext
         {
            Limit = limit,
         };

         IEnumerable<User> users = await this.userService.FindManyAsync(findManyUsersContext);
         var response = this.mapper.Map<IEnumerable<FindUserResponse>>(users);

         return this.Ok(response);
      }

      /// <summary>
      /// Creates a new user.
      /// </summary>
      /// <param name="createUserRequest">The user to create.</param>
      /// <returns>201 Created if the user was created.</returns>
      [HttpPost]
      public async Task<IActionResult> CreateOneAsync(ModifyUserRequest createUserRequest)
      {
         createUserRequest.Id = Guid.NewGuid();
         var user = this.mapper.Map<User>(createUserRequest);

         await this.userService.CreateOneAsync(user);

         var createUserResponse = this.mapper.Map<CreateUserResponse>(user);

         return this.CreatedAtRoute(string.Empty, string.Empty, createUserResponse);
      }

      /// <summary>
      /// Updates a single user with the given id.
      /// </summary>
      /// <param name="updateRequest">The updated reference of the user.</param>
      /// <param name="userId">The id of the user to be updated.</param>
      /// <returns>204 No Content if the user was updated.</returns>
      [HttpPut("{userId:guid}")]
      public async Task<IActionResult> UpdateOneAsync(ModifyUserRequest updateRequest, Guid userId)
      {
         if (!await this.userService.ExistsAsync(userId))
         {
            return this.NotFound();
         }

         updateRequest.Id = userId;

         User user = await this.userService.FindOneAsync(userId);
         this.mapper.Map(updateRequest, user);
         await this.userService.UpdateOneAsync(user);

         return this.NoContent();
      }

      /// <summary>
      /// Retrieves a single user with the given id.
      /// </summary>
      /// <param name="userId">The id of the user to be retrieved.</param>
      /// <returns>200 Ok if the user is found.</returns>
      [HttpGet("{userId:guid}")]
      public async Task<IActionResult> FindOneAsync(Guid userId)
      {
         if (!await this.userService.ExistsAsync(userId))
         {
            return this.NotFound();
         }

         User user = await this.userService.FindOneAsync(userId);
         var response = this.mapper.Map<FindUserResponse>(user);

         return this.Ok(response);
      }

      /// <summary>
      /// Deletes a single user with the given id.
      /// </summary>
      /// <param name="userId">The id of the user to be deleted.</param>
      /// <returns>204 No Content if the user was deleted.</returns>
      [HttpDelete("{userId:guid}")]
      public async Task<IActionResult> DeleteOneAsync(Guid userId)
      {
         if (!await this.userService.ExistsAsync(userId))
         {
            return this.NotFound();
         }

         await this.userService.DeleteOneAsync(userId);

         return this.NoContent();
      }
   }
}

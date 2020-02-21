// <copyright file="IUserRepository.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Core
{
   using System;
   using System.Collections.Generic;
   using System.Threading.Tasks;

   /// <summary>
   /// Repository for working with <see cref="User"/> objects.
   /// </summary>
   public interface IUserRepository
   {
      /// <summary>
      /// Retrieves a list of <see cref="User"/> objects which match the given <see cref="FindManyUsersContext"/>.
      /// </summary>
      /// <param name="context">Provides context for filtering users.</param>
      /// <returns>A list of <see cref="User"/> objects which match the given <see cref="FindManyUsersContext"/>.</returns>
      Task<IEnumerable<User>> FindManyAsync(FindManyUsersContext context);

      /// <summary>
      /// Checks if a <see cref="User"/> with the given <paramref name="userId"/> exists.
      /// </summary>
      /// <param name="userId">The id of the user to check.</param>
      /// <returns>True if the user exists, false otherwise.</returns>
      Task<bool> ExistsAsync(Guid userId);

      /// <summary>
      /// Creates a single user.
      /// </summary>
      /// <param name="user">THe user to create.</param>
      /// <returns>A <see cref="Task"/> representing the result of the operation.</returns>
      Task CreateOneAsync(User user);

      /// <summary>
      /// Deletes a single user with the given id.
      /// </summary>
      /// <param name="userId">The id of the user to delete.</param>
      /// <returns>True if the user was deleted, false otherwise.</returns>
      Task<bool> DeleteOneAsync(Guid userId);

      /// <summary>
      /// Retrieves a single use with the given id.
      /// </summary>
      /// <param name="userId">The id of the user to retrieve.</param>
      /// <returns>The user with the given id, if the user doesn't exist then null.</returns>
      Task<User> FindOneAsync(Guid userId);

      /// <summary>
      /// Updates a single user.
      /// </summary>
      /// <param name="user">THe user to update.</param>
      /// <returns>A <see cref="Task"/> representing the result of the operation.</returns>
      Task UpdateOneAsync(User user);
   }
}

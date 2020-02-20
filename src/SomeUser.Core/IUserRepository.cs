// <copyright file="IUserRepository.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Core
{
   using System;
   using System.Collections.Generic;
   using System.Threading.Tasks;

   public interface IUserRepository
   {

      Task<IEnumerable<User>> FindManyAsync(FindManyUsersContext context);

      /// <summary>
      /// Checks if a <see cref="User"/> with the given <paramref name="userId"/> exists.
      /// </summary>
      /// <param name="userId">The id to check.</param>
      /// <returns>True if the user exists, false otherwise.</returns>
      Task<bool> ExistsAsync(Guid userId);

      Task CreateOneAsync(User user);

      Task<bool> DeleteOneAsync(Guid userId);

      Task<User> FindOneAsync(Guid userId);
   }
}

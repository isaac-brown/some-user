// <copyright file="UserEntity.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Persistence.SqlServer.Entities
{
   using System;

   public class UserEntity
   {
      public Guid Id { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Email { get; set; }
   }
}

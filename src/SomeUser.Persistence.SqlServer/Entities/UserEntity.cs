// <copyright file="UserEntity.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Persistence.SqlServer.Entities
{
   using System;

   /// <summary>
   /// Represents a user entity for some application which can be persisted.
   /// </summary>
   public class UserEntity
   {


      /// <summary>
      /// Gets or sets the user's unique identifier.
      /// </summary>
      public Guid Id { get; set; }

      /// <summary>
      /// Gets or sets the first name of the user.
      /// </summary>
      public string FirstName { get; set; }

      /// <summary>
      /// Gets or sets the last name of the user.
      /// </summary>
      public string LastName { get; set; }

      /// <summary>
      /// Gets or sets the email address for the user.
      /// </summary>
      public string Email { get; set; }

      public string PhoneNumber { get; set; }

      public string DateOfBirth { get; set; }

      public string ProfileImageSmall { get; set; }

      public string ProfileImageLarge { get; set; }
   }
}

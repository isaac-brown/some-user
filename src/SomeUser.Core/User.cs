// <copyright file="User.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Core
{
   using System;

   /// <summary>
   /// Represents a user for some application.
   /// </summary>
   public class User
   {
      /// <summary>
      ///  Gets or sets the user's title.
      /// </summary>
      public string Title { get; set; }

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

      /// <summary>
      /// Gets or sets the user's phone number.
      /// </summary>
      public string PhoneNumber { get; set; }

      /// <summary>
      /// Gets or sets the user's date of birth.
      /// </summary>
      public DateTime DateOfBirth { get; set; }

      public ProfileImages ProfileImages { get; set; }
   }
}

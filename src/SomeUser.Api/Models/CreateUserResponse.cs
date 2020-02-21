// <copyright file="CreateUserResponse.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api.Models
{
   using System;

   /// <summary>
   /// Represents the response to create a single user.
   /// </summary>
   public class CreateUserResponse
   {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented
      public Guid Id { get; set; }

      public string Title { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Email { get; set; }

      public string PhoneNumber { get; set; }

      public string DateOfBirth { get; set; }

      public ProfileImagesDto ProfileImages { get; set; }
   }
}

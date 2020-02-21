// <copyright file="UpdateUserRequest.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api.Models
{
   using System;
   using System.Text.Json.Serialization;

   /// <summary>
   /// Represents a request to update a single user.
   /// </summary>
   public class UpdateUserRequest
   {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented

      [JsonIgnore]
      public Guid Id { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Email { get; set; }
   }
}

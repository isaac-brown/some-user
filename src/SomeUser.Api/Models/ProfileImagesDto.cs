// <copyright file="ProfileImagesDto.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api.Models
{
   /// <summary>
   /// Represents a user's profile images.
   /// </summary>
   public class ProfileImagesDto
   {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented

      public string UrlSmall { get; set; }

      public string UrlLarge { get; set; }
   }
}

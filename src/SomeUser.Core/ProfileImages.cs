// <copyright file="ProfileImages.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Core
{
   /// <summary>
   /// Represents a user's profile images.
   /// </summary>
   public class ProfileImages
   {
      /// <summary>
      /// Gets or sets the url to the small version of the user's profile image.
      /// </summary>
      public string UrlSmall { get; set; }

      /// <summary>
      /// Gets or sets the url to the large version of the user's profile image.
      /// </summary>
      public string UrlLarge { get; set; }
   }
}

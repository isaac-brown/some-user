// <copyright file="UserTitle.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Core
{
   /// <summary>
   /// Represents a user's title.
   /// </summary>
   public class UserTitle : Enumeration
   {
      private UserTitle(string keyCode, string displayName)
          : base(keyCode, displayName)
      {
      }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented

      public static UserTitle Dr => new UserTitle(nameof(Dr).ToUpperInvariant(), nameof(Dr));

      public static UserTitle Mr => new UserTitle(nameof(Mr).ToUpperInvariant(), nameof(Mr));

      public static UserTitle Mrs => new UserTitle(nameof(Mrs).ToUpperInvariant(), nameof(Mrs));

      public static UserTitle Ms => new UserTitle(nameof(Ms).ToUpperInvariant(), nameof(Ms));
   }
}

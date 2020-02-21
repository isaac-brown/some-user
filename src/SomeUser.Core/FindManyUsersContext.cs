// <copyright file="FindManyUsersContext.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Core
{
   using System;

   /// <summary>
   /// Context for querying <see cref="IUserService.FindManyAsync(FindManyUsersContext)"/>.
   /// </summary>
   public class FindManyUsersContext
   {
      private int limit = 100;

      /// <summary>
      /// Gets or sets maximum number of records to retrieve. Default is 100.
      /// </summary>
      public int Limit
      {
         get => this.limit;
         set
         {
            value = Math.Max(1, value);
            value = Math.Min(value, 1000);

            this.limit = value;
         }
      }

      /// <summary>
      /// Gets or sets the first name to filter results.
      /// </summary>
      public string FirstName { get; set; }

      /// <summary>
      /// Gets or sets the last name to filter results.
      /// </summary>
      public string LastName { get; set; }
   }
}

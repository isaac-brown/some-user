// <copyright file="UserMappingProfile.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api.Mapping
{
   using SomeUser.Api.Models;
   using SomeUser.Core;

   public class UserMappingProfile : AutoMapper.Profile
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="UserMappingProfile"/> class.
      /// </summary>
      public UserMappingProfile()
      {
         this.CreateMap<User, FindUserResponse>();
      }
   }
}

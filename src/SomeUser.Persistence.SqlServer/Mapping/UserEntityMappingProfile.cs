// <copyright file="UserEntityMappingProfile.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Persistence.SqlServer.Mapping
{
   using AutoMapper;
   using SomeUser.Core;
   using SomeUser.Persistence.SqlServer.Entities;

   /// <summary>
   /// AutoMapper profile for mapping to and from <see cref="User"/> objects.
   /// </summary>
   public class UserEntityMappingProfile : Profile
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="UserEntityMappingProfile"/> class.
      /// </summary>
      public UserEntityMappingProfile()
      {
         this.CreateMap<UserEntity, User>()
            .ReverseMap();
      }
   }
}

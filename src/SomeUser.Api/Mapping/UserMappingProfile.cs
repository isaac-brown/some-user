// <copyright file="UserMappingProfile.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api.Mapping
{
   using System;
   using AutoMapper;
   using SomeUser.Api.Dtos;
   using SomeUser.Core;

   /// <summary>
   /// AutoMapper profile for mapping to and from <see cref="User"/> objects.
   /// </summary>
   public class UserMappingProfile : Profile
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="UserMappingProfile"/> class.
      /// </summary>
      public UserMappingProfile()
      {
         this.CreateMap<DateTime?, string>().ConvertUsing<NullableDateTimeConverter>();
         this.CreateMap<User, FindUserResponse>();
         this.CreateMap<User, CreateUserResponse>();
         this.CreateMap<ModifyUserRequest, User>()
             .ForMember(dest => dest.Title, opts => opts.MapFrom(src => Enumeration.FromKeyCode<UserTitle>(src.Title)));

         this.CreateMap<ProfileImagesDto, ProfileImages>()
             .ReverseMap();
      }
   }
}

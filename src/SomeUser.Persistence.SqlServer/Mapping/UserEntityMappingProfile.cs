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
             .ForMember(dest => dest.ProfileImages, opts => opts.MapFrom(src => src));

         this.CreateMap<UserEntity, ProfileImages>()
             .ForMember(dest => dest.UrlSmall, opts => opts.MapFrom(src => src.ProfileImageSmall))
             .ForMember(dest => dest.UrlLarge, opts => opts.MapFrom(src => src.ProfileImageLarge));

         this.CreateMap<User, UserEntity>()
             .ForMember(dest => dest.ProfileImageSmall, opts => opts.MapFrom(src => src.ProfileImages.UrlSmall))
             .ForMember(dest => dest.ProfileImageLarge, opts => opts.MapFrom(src => src.ProfileImages.UrlLarge));
      }
   }
}

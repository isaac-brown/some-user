using AutoMapper;
using SomeUser.Core;
using SomeUser.Persistence.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SomeUser.Persistence.SqlServer.Mapping
{
   public class UserEntityMappingProfile : Profile
   {
      public UserEntityMappingProfile()
      {
         this.CreateMap<UserEntity, User>()
            .ReverseMap();
      }
   }
}

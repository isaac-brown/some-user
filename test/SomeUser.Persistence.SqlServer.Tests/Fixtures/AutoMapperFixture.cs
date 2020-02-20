// <copyright file="AutoMapperFixture.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Persistence.SqlServer.Tests.Fixtures
{
   using AutoMapper;
   using Moq;
   using SomeUser.Persistence.SqlServer.Mapping;

   /// <summary>
   /// Test fixture to be used by test classes that require an <see cref="IMapper"/> collaborator.
   /// </summary>
   public static class AutoMapperFixture
   {
      /// <summary>
      /// Gets a new instance of <see cref="IMapper"/> that is configured with all required profiles.
      /// </summary>
      public static IMapper Mapper
      {
         get
         {
            var configuration = new MapperConfiguration(cfg => cfg.AddMaps(typeof(UserEntityMappingProfile).Assembly));
            IMapper mapper = new Mapper(configuration);
            return mapper;
         }
      }

      /// <summary>
      /// Gets an new instance of <see cref="IMapper"/> which has no implementation.
      /// </summary>
      public static IMapper Dummy => Mock.Object;

      /// <summary>
      /// Gets a new instance of <see cref="IMapper"/> which can be used for mocking.
      /// </summary>
      public static Mock<IMapper> Mock => new Mock<IMapper>();
   }
}

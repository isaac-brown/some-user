// <copyright file="UserRepositoryTests.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Persistence.SqlServer.Tests
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Threading.Tasks;
   using AutoFixture;
   using AutoMapper;
   using FluentAssertions;
   using Microsoft.EntityFrameworkCore;
   using MoreLinq;
   using SomeUser.Core;
   using SomeUser.Persistence.SqlServer;
   using SomeUser.Persistence.SqlServer.Entities;
   using SomeUser.Persistence.SqlServer.Tests.Fixtures;
   using Xunit;

   /// <summary>
   /// Unit tests for the <see cref="UserService"/> class.
   /// </summary>
   [Trait("Category", "Unit")]
   public class UserRepositoryTests
   {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented

      [Fact]
      public async Task Given_an_id_of_a_user_which_exists_When_ExistsAsync_is_called_Then_true_should_be_returned()
      {
         // Arrange.
         IFixture fixture = AutoMoqFixture.Create();
         fixture.Inject<IMapper>(AutoMapperFixture.Mapper);
         InjectEmptyDbContext(fixture);

         var user = fixture.Create<User>();

         var sut = fixture.Create<UserService>();

         // Act.
         await sut.CreateOneAsync(user);

         var userExists = await sut.ExistsAsync(user.Id);

         // Assert.
         userExists.Should().BeTrue();
      }

      [Fact]
      public async Task Given_an_id_of_a_user_which_does_not_exist_When_ExistsAsync_is_called_Then_false_should_be_returned()
      {
         // Arrange.
         IFixture fixture = AutoMoqFixture.Create();
         fixture.Inject<IMapper>(AutoMapperFixture.Mapper);
         InjectEmptyDbContext(fixture);

         Guid userId = fixture.Create<Guid>();

         var sut = fixture.Create<UserService>();

         // Act.
         var userExists = await sut.ExistsAsync(userId);

         // Assert.
         userExists.Should().BeFalse();
      }

      [Theory]
      [InlineData(1, 1, 1)]
      [InlineData(1, 0, 0)]
      [InlineData(1, 2, 1)]
      public async Task Given_limit_is_positive_When_FindManyAsync_is_called_Then_result_should_contain_at_most_the_limit_specified(int limit, int countUsers, int expectedCount)
      {
         // Arrange.
         IFixture fixture = AutoMoqFixture.Create();
         fixture.Inject<IMapper>(AutoMapperFixture.Mapper);
         InjectDbContextWithUsers(fixture, countUsers);
         FindManyUsersContext findManyUsersContext = new FindManyUsersContext { Limit = limit };

         var sut = fixture.Create<UserService>();

         // Act.
         var users = await sut.FindManyAsync(findManyUsersContext);

         // Assert.
         users.Count().Should().Be(expectedCount);
      }

      [Theory]
      [InlineData(999, 999)]
      [InlineData(1000, 1000)]
      [InlineData(1001, 1000)]
      public async Task Given_limit_is_not_specified_When_FindManyAsync_is_called_Then_result_should_contain_at_most_1000_users(int countUsers, int expectedCount)
      {
         // Arrange.
         IFixture fixture = AutoMoqFixture.Create();
         fixture.Inject<IMapper>(AutoMapperFixture.Mapper);
         InjectDbContextWithUsers(fixture, countUsers);

         var sut = fixture.Create<UserService>();

         // Act.
         var users = await sut.FindManyAsync();

         // Assert.
         users.Count().Should().Be(expectedCount);
      }

      [Fact]
      public async Task Given_firstName_of_existing_user_When_FindManyAsync_is_called_Then_result_should_contain_only_users_with_the_given_firstName()
      {
         // Arrange.
         IFixture fixture = AutoMoqFixture.Create();
         fixture.Inject<IMapper>(AutoMapperFixture.Mapper);
         var userEntities = InjectDbContextWithUsers(fixture);
         var userEntity = userEntities.First();
         var expectedFirstName = userEntity.FirstName;
         FindManyUsersContext findManyUsersContext = new FindManyUsersContext { FirstName = expectedFirstName };

         var sut = fixture.Create<UserService>();

         // Act.
         var users = await sut.FindManyAsync(findManyUsersContext);

         // Assert.
         users.All(user => user.FirstName.Equals(expectedFirstName)).Should().BeTrue();
      }

      [Fact]
      public async Task Given_lastName_of_existing_user_When_FindManyAsync_is_called_Then_result_should_contain_only_users_with_the_given_firstName()
      {
         // Arrange.
         IFixture fixture = AutoMoqFixture.Create();
         fixture.Inject<IMapper>(AutoMapperFixture.Mapper);
         var userEntities = InjectDbContextWithUsers(fixture);
         var userEntity = userEntities.First();
         string expectedLastName = userEntity.LastName;
         FindManyUsersContext findManyUsersContext = new FindManyUsersContext { LastName = expectedLastName };

         var sut = fixture.Create<UserService>();

         // Act.
         var users = await sut.FindManyAsync(findManyUsersContext);

         // Assert.
         users.All(user => user.LastName.Equals(expectedLastName)).Should().BeTrue();
      }

      [Fact]
      public async Task Given_an_id_of_a_user_which_does_not_exist_When_DeleteOneAsync_is_called_Then_false_should_be_returned()
      {
         // Arrange.
         IFixture fixture = AutoMoqFixture.Create();
         fixture.Inject<IMapper>(AutoMapperFixture.Mapper);
         InjectEmptyDbContext(fixture);

         Guid userId = fixture.Create<Guid>();

         var sut = fixture.Create<UserService>();

         // Act.
         var userDeleted = await sut.DeleteOneAsync(userId);

         // Assert.
         userDeleted.Should().BeFalse();
      }

      [Fact]
      public async Task Given_an_id_of_a_user_which_exists_When_DeleteOneAsync_is_called_Then_true_should_be_returned_and_the_user_removed_from_the_repository()
      {
         // Arrange.
         IFixture fixture = AutoMoqFixture.Create();
         fixture.Inject<IMapper>(AutoMapperFixture.Mapper);
         var entities = InjectDbContextWithUsers(fixture);

         Guid userId = entities.First().Id;

         var sut = fixture.Create<UserService>();

         // Act.
         bool userDeleted = await sut.DeleteOneAsync(userId);
         bool userStillExists = await sut.ExistsAsync(userId);

         // Assert.
         userDeleted.Should().BeTrue();
         userStillExists.Should().BeFalse();
      }

      [Fact]
      public async Task Given_a_valid_user_When_CreateOneAsync_is_called_Then_user_should_be_added_to_repository()
      {
         // Arrange.
         IFixture fixture = AutoMoqFixture.Create();
         fixture.Inject<IMapper>(AutoMapperFixture.Mapper);
         InjectEmptyDbContext(fixture);

         var user = fixture.Create<User>();

         var sut = fixture.Create<UserService>();

         // Act.
         await sut.CreateOneAsync(user);
         bool userExists = await sut.ExistsAsync(user.Id);

         // Assert.
         userExists.Should().BeTrue();
      }

      [Fact]
      public async Task Given_an_id_of_a_user_which_exists_When_FindOneAsync_is_called_Then_user_should_be_returned()
      {
         // Arrange.
         IFixture fixture = AutoMoqFixture.Create();
         fixture.Inject<IMapper>(AutoMapperFixture.Mapper);
         InjectEmptyDbContext(fixture);

         var user = fixture.Create<User>();

         var sut = fixture.Create<UserService>();

         // Act.
         await sut.CreateOneAsync(user);
         User foundUser = await sut.FindOneAsync(user.Id);

         // Assert.
         foundUser.Should().BeEquivalentTo(user);
      }

      // TODO: UpdateOneAsync
      private static void InjectEmptyDbContext(IFixture fixture)
      {
         var options = new DbContextOptionsBuilder<SomeUserDbContext>()
             .UseInMemoryDatabase(fixture.Create<string>())
             .Options;

         var context = new SomeUserDbContext(options);

         fixture.Inject<SomeUserDbContext>(context);
      }

      private static IEnumerable<UserEntity> InjectDbContextWithUsers(IFixture fixture, int countUsers = 10)
      {
         var options = new DbContextOptionsBuilder<SomeUserDbContext>()
             .UseInMemoryDatabase(fixture.Create<string>())
             .Options;

         var userEntities = fixture.CreateMany<UserEntity>(countUsers);

         using (var ctx = new SomeUserDbContext(options))
         {
            ctx.AddRange(userEntities);
            ctx.SaveChanges();
         }

         var context = new SomeUserDbContext(options);

         fixture.Inject<SomeUserDbContext>(context);
         return userEntities;
      }
   }
}

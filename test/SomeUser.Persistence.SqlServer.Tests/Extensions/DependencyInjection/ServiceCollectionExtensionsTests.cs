// <copyright file="ServiceCollectionExtensionsTests.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Persistence.SqlServer.Tests.Extensions.DependencyInjection
{
   using AutoFixture;
   using FluentAssertions;
   using Microsoft.Extensions.Configuration;
   using Microsoft.Extensions.DependencyInjection;
   using Moq;
   using SomeUser.Persistence.SqlServer.Tests.Fixtures;
   using Xunit;

   /// <summary>
   /// Unit tests for <see cref="ServiceCollectionExtensions"/>.
   /// </summary>
   public class ServiceCollectionExtensionsTests
   {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented

      [Fact]
      public void When_AddSomeUserSqlServer_is_called_Then_services_should_be_added_to_container()
      {
         // Arrange.
         IFixture fixture = AutoMoqFixture.Create();

         // Mock GetSection as GetConnectionString is an extension method so cannot be mocked directly.
         var mockConfiguration = new Mock<IConfiguration>();
         mockConfiguration.Setup(config => config.GetSection("ConnectionStrings")["SomeUser"])
                          .Returns(fixture.Create<string>());

         ServiceCollection services = new ServiceCollection();

         // Act.
         services.AddSomeUserSqlServer(mockConfiguration.Object);
         var provider = services.BuildServiceProvider();

         // Assert.
         provider.GetService<SomeUserDbContext>()
                 .Should()
                 .NotBeNull();
      }

      [Fact]
      public void When_AddSomeUserSqlServerInMemory_is_called_Then_test_services_should_be_added_to_container()
      {
         // Arrange.
         ServiceCollection services = new ServiceCollection();

         // Act.
         services.AddSomeUserSqlServerInMemory();
         var provider = services.BuildServiceProvider();

         // Assert.
         provider.GetService<SomeUserDbContext>()
                 .Should()
                 .NotBeNull();
      }
   }
}

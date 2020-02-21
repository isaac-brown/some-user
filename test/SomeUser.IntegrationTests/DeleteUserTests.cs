// <copyright file="DeleteUserTests.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.IntegrationTests
{
   using System;
   using System.Linq;
   using System.Net;
   using System.Threading.Tasks;
   using FluentAssertions;
   using Microsoft.AspNetCore.Mvc.Testing;
   using Newtonsoft.Json;
   using SomeUser.Api.Models;
   using Xunit;

   /// <summary>
   /// Integration tests for retrieving a single user.
   /// </summary>
   [Trait("Category", "Integration")]
   public class DeleteUserTests
     : IClassFixture<WebApplicationFactory<Api.Startup>>
   {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented

      private readonly WebApplicationFactory<Api.Startup> factory;

      public DeleteUserTests(WebApplicationFactory<Api.Startup> factory)
      {
         this.factory = factory;
      }

      [Fact]
      public async Task Given_a_userId_which_does_not_exist_When_FindOneUser_is_called_Then_a_response_with_status_404_Not_Found_should_be_returned()
      {
         // Arrange.
         var client = this.factory.CreateClient();
         var userId = Guid.NewGuid();

         // Act.
         var response = await client.DeleteAsync($"/users/{userId}");

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.NotFound);
      }

      [Fact]
      public async Task Given_a_userId_which_exists_When_FindOneUser_is_called_Then_a_response_with_status_204_No_Content_should_be_returned()
      {
         // Arrange.
         var client = this.factory.CreateClient();
         var body = await (await client.GetAsync("/users")).Content.ReadAsStringAsync();
         var userId = JsonConvert.DeserializeObject<FindUserResponse[]>(body).First().Id;

         // Act.
         var response = await client.DeleteAsync($"/users/{userId}");

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.NoContent);
      }
   }
}

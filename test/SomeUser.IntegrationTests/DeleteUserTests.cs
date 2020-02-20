using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SomeUser.Api.Models;
using Xunit;

namespace SomeUser.IntegrationTests
{
   /// <summary>
   /// Integration tests for retrieving a single user.
   /// </summary>
   [Trait("Category", "Integration")]
   public class DeleteUserTests
     : IClassFixture<WebApplicationFactory<Api.Startup>>
   {
      private readonly WebApplicationFactory<Api.Startup> factory;

      public DeleteUserTests(WebApplicationFactory<Api.Startup> factory)
      {
         this.factory = factory;
      }

      [Fact]
      public async Task Given_a_userId_which_does_not_exist_When_FindOneUser_is_called_Then_a_response_with_status_404_Not_Found_should_be_returned()
      {
         // Arrange.
         var client = factory.CreateClient();
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
         var client = factory.CreateClient();
         var body = await (await client.GetAsync("/users")).Content.ReadAsStringAsync();
         var userId = JsonConvert.DeserializeObject<FindUserResponse[]>(body).First().Id;

         // Act.
         var response = await client.DeleteAsync($"/users/{userId}");

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.NoContent);
      }
   }
}

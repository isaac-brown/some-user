using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace SomeUser.IntegrationTests
{
   /// <summary>
   /// Integration tests for retrieving many users.
   /// </summary>
   public class FindManyUsersTests
      : IClassFixture<WebApplicationFactory<Api.Startup>>
   {
      private readonly WebApplicationFactory<Api.Startup> factory;

      public FindManyUsersTests(WebApplicationFactory<Api.Startup> factory)
      {
         this.factory = factory;
      }

      [Theory]
      [InlineData(0)]
      [InlineData(-1)]
      [InlineData(int.MinValue)]
      public async Task Given_limit_is_negative_When_FindManyUsers_is_called_Then_a_response_with_status_400_Bad_Request_should_be_returned(int limit)
      {
         // Arrange.
         var client = factory.CreateClient();

         // Act.
         var response = await client.GetAsync($"/users?limit={limit}");

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
      }

      [Theory]
      [InlineData(1)]
      [InlineData(int.MaxValue)]
      public async Task Given_limit_is_greater_than_0_When_FindManyUsers_is_called_Then_a_response_with_status_200_OK_should_be_returned(int limit)
      {
         // Arrange.
         var client = factory.CreateClient();

         // Act.
         var response = await client.GetAsync($"/users?limit={limit}");
         var content = await response.Content.ReadAsStringAsync();

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.OK);
         content.Should().MatchRegex(@"\[.*\]", because: "We expect a json array to be returned");
      }

      [Fact]
      public async Task Given_no_query_params_are_provided_When_FindManyUsers_is_called_Then_a_response_with_status_200_OK_should_be_returned()
      {
         // Arrange.
         var client = factory.CreateClient();

         // Act.
         var response = await client.GetAsync($"/users");
         var content = await response.Content.ReadAsStringAsync();

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.OK);
         content.Should().MatchRegex(@"\[.*\]", because: "We expect a json array to be returned");
      }

      // TODO: Integration tests around adding a user with a specific name and then querying for them by first and last name.
   }
}

// <copyright file="FindManyUsersTests.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.IntegrationTests
{
   using System.Linq;
   using System.Net;
   using System.Net.Http;
   using System.Net.Http.Formatting;
   using System.Threading.Tasks;
   using FluentAssertions;
   using Microsoft.AspNetCore.Mvc.Testing;
   using SomeUser.Api.Dtos;
   using Xunit;

   /// <summary>
   /// Integration tests for retrieving many users.
   /// </summary>
   public class FindManyUsersTests
      : IClassFixture<WebApplicationFactory<Api.Startup>>
   {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented

      private readonly WebApplicationFactory<Api.Startup> factory;
      private readonly JsonMediaTypeFormatter jsonFormatter;

      public FindManyUsersTests(WebApplicationFactory<Api.Startup> factory)
      {
         this.factory = factory;
         this.jsonFormatter = new JsonMediaTypeFormatter();
      }

      [Fact]
      public async Task Given_no_query_params_are_provided_When_FindManyUsers_is_called_Then_a_response_with_status_200_OK_should_be_returned()
      {
         // Arrange.
         var client = this.factory.CreateClient();

         // Act.
         var response = await client.GetAsync($"/users");
         var content = await response.Content.ReadAsStringAsync();

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.OK);
         content.Should().MatchRegex(@"\[.*\]", because: "We expect a json array to be returned");
      }

      [Fact]
      public async Task Given_firstName_and_lastName_are_provided_When_FindManyUsers_is_called_Then_all_users_in_the_response_should_have_the_given_first_and_last_name()
      {
         // Arrange.
         var client = this.factory.CreateClient();
         ModifyUserRequest request = new ModifyUserRequest
         {
            FirstName = "Bob",
            LastName = "Hawkins",
         };

         // Act.
         await client.PostAsync("/users", request, this.jsonFormatter);
         var response = await client.GetAsync($"/users?firstName=Bob&lastName=Hawkins");
         var users = await response.Content.ReadAsAsync<FindUserResponse[]>();

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.OK);
         users.All(user => user.FirstName == "Bob" && user.LastName == "Hawkins");
      }

      [Fact]
      public async Task Given_limit_is_less_than_1_When_FindManyUsers_is_called_Then_there_should_be_at_most_1_result()
      {
         // Arrange.
         var client = this.factory.CreateClient();
         ModifyUserRequest request = new ModifyUserRequest
         {
            FirstName = "Bob",
            LastName = "Hawkins",
         };

         // Act.
         await client.PostAsync("/users", request, this.jsonFormatter);
         var response = await client.GetAsync($"/users?limit=0");
         var users = await response.Content.ReadAsAsync<FindUserResponse[]>();

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.OK);
         users.Count().Should().Be(1);
      }
   }
}

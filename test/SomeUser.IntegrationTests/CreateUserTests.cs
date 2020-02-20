using System.Net;
using System.Net.Mime;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SomeUser.Api.Models;

namespace SomeUser.IntegrationTests
{
   /// <summary>
   /// Integration tests for creating a single user.
   /// </summary>
   [Trait("Category", "Integration")]
   public class CreateUserTests
      : IClassFixture<WebApplicationFactory<Api.Startup>>
   {
      private readonly WebApplicationFactory<Api.Startup> factory;

      public CreateUserTests(WebApplicationFactory<Api.Startup> factory)
      {
         this.factory = factory;
      }

      [Fact]
      public async Task Given_an_empty_request_body_When_CreateUser_is_called_Then_a_response_with_status_400_Bad_Request_should_be_returned()
      {
         // Arrange.
         var client = this.factory.CreateClient();
         using HttpContent body = new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json);

         // Act.
         var response = await client.PostAsync("users", body);
         var responseBody = await response.Content.ReadAsStringAsync();

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
         responseBody.Should().ContainAll(
            new[]
            {
               "'First Name' must not be empty.",
               "'Last Name' must not be empty.",
               "'Email' must not be empty.",
         });
      }

      [Fact]
      public async Task Given_a_valid_request_body_When_CreateUser_is_called_Then_a_response_with_status_201_Created_should_be_returned()
      {
         // Arrange.
         var client = this.factory.CreateClient();
         CreateUserRequest userToCreate = new CreateUserRequest
         {
            FirstName = "Alice",
            LastName = "Hall",
            Email = "alice.hall@example.com"
         };
         string json = JsonConvert.SerializeObject(userToCreate);
         using HttpContent body = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

         // Act.
         var httpResponse = await client.PostAsync("users", body);
         var responseBody = await httpResponse.Content.ReadAsStringAsync();
         var createdUser  = JsonConvert.DeserializeObject<CreateUserResponse>(responseBody);

         // Assert.
         httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
         createdUser.Should().BeEquivalentTo(userToCreate);
      }
   }
}

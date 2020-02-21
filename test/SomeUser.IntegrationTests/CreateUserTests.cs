// <copyright file="CreateUserTests.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.IntegrationTests
{
   using System.Net;
   using System.Net.Http;
   using System.Net.Http.Formatting;
   using System.Net.Mime;
   using System.Text;
   using System.Threading.Tasks;
   using FluentAssertions;
   using Microsoft.AspNetCore.Mvc.Testing;
   using Newtonsoft.Json;
   using SomeUser.Api.Models;
   using Xunit;

   /// <summary>
   /// Integration tests for creating a single user.
   /// </summary>
   [Trait("Category", "Integration")]
   public class CreateUserTests
      : IClassFixture<WebApplicationFactory<Api.Startup>>
   {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented

      private readonly WebApplicationFactory<Api.Startup> factory;
      private readonly JsonMediaTypeFormatter jsonFormatter;

      public CreateUserTests(WebApplicationFactory<Api.Startup> factory)
      {
         this.factory = factory;
         this.jsonFormatter = new JsonMediaTypeFormatter();
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
            Email = "alice.hall@example.com",
         };

         // Act.
         var httpResponse = await client.PostAsync("users", userToCreate, this.jsonFormatter);
         var createdUser = await httpResponse.Content.ReadAsAsync<CreateUserResponse>();

         // Assert.
         httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
         createdUser.Should().BeEquivalentTo(userToCreate);
      }
   }
}

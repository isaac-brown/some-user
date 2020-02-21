// <copyright file="FindOneUserTests.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.IntegrationTests
{
   using System;
   using System.Linq;
   using System.Net;
   using System.Net.Http;
   using System.Net.Http.Formatting;
   using System.Threading.Tasks;
   using FluentAssertions;
   using Microsoft.AspNetCore.Mvc.Testing;
   using Newtonsoft.Json;
   using SomeUser.Api.Models;
   using Xunit;

   /// <summary>
   /// Integration tests for retrieving a single user.
   /// </summary>
   public class FindOneUserTests
     : IClassFixture<WebApplicationFactory<Api.Startup>>
   {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented

      private readonly WebApplicationFactory<Api.Startup> factory;
      private readonly JsonMediaTypeFormatter jsonFormatter;

      public FindOneUserTests(WebApplicationFactory<Api.Startup> factory)
      {
         this.factory = factory;
         this.jsonFormatter = new JsonMediaTypeFormatter();
      }

      [Fact]
      public async Task Given_a_userId_which_does_not_exist_When_FindOneUser_is_called_Then_a_response_with_status_404_Not_Found_should_be_returned()
      {
         // Arrange.
         var client = this.factory.CreateClient();
         var userId = Guid.NewGuid();

         // Act.
         var response = await client.GetAsync($"/users/{userId}");

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.NotFound);
      }

      [Fact]
      public async Task Given_a_userId_which_exists_When_FindOneUser_is_called_Then_a_response_with_status_200_OK_should_be_returned()
      {
         // Arrange.
         var client = this.factory.CreateClient();

         ModifyUserRequest createUserRequest = new ModifyUserRequest
         {
            FirstName = "Alice",
            LastName = "Hall",
            Email = "alice.hall@example.com",
            DateOfBirth = "2000-01-01",
         };

         var createUserHttpResponse = await client.PostAsync("/users", createUserRequest, this.jsonFormatter);
         var createUserResponse = await createUserHttpResponse.Content.ReadAsAsync<CreateUserResponse>();
         Guid userId = createUserResponse.Id;

         // Act.
         var response = await client.GetAsync($"/users/{userId}");

         // Assert.
         response.StatusCode.Should().Be(HttpStatusCode.OK);
      }
   }
}

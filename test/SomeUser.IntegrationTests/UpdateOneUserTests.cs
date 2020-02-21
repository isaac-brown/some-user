// <copyright file="UpdateOneUserTests.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.IntegrationTests
{
   using System;
   using System.Linq;
   using System.Net;
   using System.Net.Http;
   using System.Net.Http.Formatting;
   using System.Net.Mime;
   using System.Text;
   using System.Threading.Tasks;
   using FluentAssertions;
   using Microsoft.AspNetCore.Mvc.Testing;
   using Newtonsoft.Json;
   using SomeUser.Api.Dtos;
   using Xunit;

   /// <summary>
   /// Integration tests for updating a single user.
   /// </summary>
   public class UpdateOneUserTests
      : IClassFixture<WebApplicationFactory<Api.Startup>>
   {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements must be documented

      private readonly WebApplicationFactory<Api.Startup> factory;
      private readonly JsonMediaTypeFormatter jsonFormatter;

      public UpdateOneUserTests(WebApplicationFactory<Api.Startup> factory)
      {
         this.factory = factory;
         this.jsonFormatter = new JsonMediaTypeFormatter();
      }

      [Fact]
      public async Task Given_a_user_id_which_does_not_exist_When_UpdateUser_is_called_Then_a_response_with_status_404_Not_Found_should_be_returned()
      {
         // Arrange.
         var client = this.factory.CreateClient();
         var userId = Guid.NewGuid();

         var userToUpdate = new ModifyUserRequest
         {
            FirstName = "Alice",
            LastName = "Hall",
            Email = "alice.hall@example.com",
         };

         // Act.
         var httpResponse = await client.PutAsync($"users/{userId}", userToUpdate, this.jsonFormatter);

         // Assert.
         httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
      }

      [Fact]
      public async Task Given_an_invalid_body_When_UpdateUser_is_called_Then_a_response_with_status_400_Bad_Request_should_be_returned()
      {
         // Arrange.
         var client = this.factory.CreateClient();
         var userId = Guid.NewGuid();

         var request = new ModifyUserRequest
         {
            Title = "Invalid",
            DateOfBirth = "Also invalid",
         };

         // Act.
         var httpResponse = await client.PutAsync($"users/{userId}", request, this.jsonFormatter);
         var responseBody = await httpResponse.Content.ReadAsStringAsync();

         // Assert.
         httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
         responseBody.Should().ContainAll(
            new[]
            {
               "'First Name' must not be empty.",
               "'Last Name' must not be empty.",
               "'Email' must not be empty.",
               "'Title' must be one of DR, MR, MRS, MS",
               "'Date Of Birth' must be a valid date (e.g. yyyy-MM-dd).",
            });
      }

      [Fact]
      public async Task Given_a_user_id_which_exists_When_UpdateUser_is_called_Then_a_response_with_status_204_No_Content_should_be_returned()
      {
         // Arrange.
         var client = this.factory.CreateClient();

         var createUserRequest = new ModifyUserRequest
         {
            FirstName = "Alice",
            LastName = "Hall",
            Email = "alice.hall@example.com",
            DateOfBirth = "2000-01-01",
         };

         var createUserResponse = await client.PostAsync("users", createUserRequest, this.jsonFormatter);
         var user = await createUserResponse.Content.ReadAsAsync<CreateUserResponse>();

         var userId = user.Id;

         var updatedUser = new ModifyUserRequest
         {
            FirstName = "Alice",
            LastName = "Hall",
            Email = "alice.hall@example.com",
            Title = "Ms",
         };

         // Act.
         var httpResponse = await client.PutAsync($"users/{userId}", updatedUser, this.jsonFormatter);

         // Assert.
         httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
      }
   }
}

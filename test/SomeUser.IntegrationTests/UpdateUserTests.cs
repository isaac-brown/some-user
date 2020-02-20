using System.Linq;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;
using System.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Newtonsoft.Json;
using SomeUser.Api.Models;

namespace SomeUser.IntegrationTests
{
   /// <summary>
   /// Integration tests for updating a single user.
   /// </summary>
   public class UpdateUserTests
      : IClassFixture<WebApplicationFactory<Api.Startup>>
   {
      private readonly WebApplicationFactory<Api.Startup> factory;

      public UpdateUserTests(WebApplicationFactory<Api.Startup> factory)
      {
         this.factory = factory;
      }

      [Fact]
      public async Task Given_a_user_id_which_does_not_exist_When_UpdateUser_is_called_Then_a_response_with_status_404_Not_Found_should_be_returned()
      {
         // Arrange.
         var client = factory.CreateClient();
         var userId = Guid.NewGuid();

         var userToUpdate = new UpdateUserRequest
         {
            FirstName = "Alice",
            LastName = "Hall",
            Email = "alice.hall@exampl.com",
         };
         string json = JsonConvert.SerializeObject(userToUpdate);
         using HttpContent body = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

         // Act.
         var httpResponse = await client.PutAsync($"users/{userId}", body);

         // Assert.
         httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
      }

      [Fact]
      public async Task Given_an_invalid_body_When_UpdateUser_is_called_Then_a_response_with_status_400_Bad_Request_should_be_returned()
      {
         // Arrange.
         var client = factory.CreateClient();
         var userId = Guid.NewGuid();

         using HttpContent body = new StringContent("{}", Encoding.UTF8, MediaTypeNames.Application.Json);

         // Act.
         var httpResponse = await client.PutAsync($"users/{userId}", body);
         var responseBody = await httpResponse.Content.ReadAsStringAsync();

         // Assert.
         httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
         responseBody.Should().ContainAll(
            new[]
            {
               "'First Name' must not be empty.",
               "'Last Name' must not be empty.",
               "'Email' must not be empty.",
         });
      }

      [Fact]
      public async Task Given_a_user_id_which_exists_When_UpdateUser_is_called_Then_a_response_with_status_204_No_Content_should_be_returned()
      {
         // Arrange.
         var client = factory.CreateClient();

         var findUsersResponse = await client.GetAsync("users");
         var users = JsonConvert.DeserializeObject<FindUserResponse[]>(await findUsersResponse.Content.ReadAsStringAsync());

         var userId = users.First().Id;

         var updatedUser = new UpdateUserRequest
         {
            FirstName = "Alice",
            LastName = "Hall",
            Email = "alice.hall@example.com",
         };

         using HttpContent body = new StringContent(
            JsonConvert.SerializeObject(updatedUser),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);

         // Act.
         var httpResponse = await client.PutAsync($"users/{userId}", body);

         // Assert.
         httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
      }
   }
}

// <copyright file="CreateUserRequestValidator.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api.Validators
{
   using FluentValidation;
   using SomeUser.Api.Models;

   /// <summary>
   /// Validator for <see cref="CreateUserRequest"/> objects.
   /// </summary>
   public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="CreateUserRequestValidator"/> class.
      /// </summary>
      public CreateUserRequestValidator()
      {
         this.RuleFor(x => x.FirstName).NotEmpty();
         this.RuleFor(x => x.LastName).NotEmpty();
         this.RuleFor(x => x.Email).NotEmpty().EmailAddress();
      }
   }
}

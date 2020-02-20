// <copyright file="UpdateUserRequestValidator.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api.Validators
{
   using FluentValidation;
   using SomeUser.Api.Models;

   /// <summary>
   /// Validator for <see cref="UpdateUserRequest"/> objects.
   /// </summary>
   public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="UpdateUserRequestValidator"/> class.
      /// </summary>
      public UpdateUserRequestValidator()
      {
         this.RuleFor(x => x.FirstName).NotEmpty();
         this.RuleFor(x => x.LastName).NotEmpty();
         this.RuleFor(x => x.Email).NotEmpty().EmailAddress();
      }
   }
}

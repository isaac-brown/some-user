// <copyright file="CreateUserRequestValidator.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api.Validators
{
   using System;
   using System.Linq;
   using FluentValidation;
   using SomeUser.Api.Models;

   /// <summary>
   /// Validator for <see cref="ModifyUserRequest"/> objects.
   /// </summary>
   public class ModifyUserRequestValidator : AbstractValidator<ModifyUserRequest>
   {
      private static readonly string[] TitleOptions = new[]
      {
            "Mr",
            "Mrs",
            "Dr",
      };

      /// <summary>
      /// Initializes a new instance of the <see cref="ModifyUserRequestValidator"/> class.
      /// </summary>
      public ModifyUserRequestValidator()
      {
         this.RuleFor(x => x.FirstName).NotEmpty();
         this.RuleFor(x => x.LastName).NotEmpty();
         this.RuleFor(x => x.Email).NotEmpty().EmailAddress();
         this.RuleFor(x => x.DateOfBirth)
             .Must(BeAValidDate)
             .WithMessage("'{PropertyName} must be a valid date (e.g. yyyy-MM-dd).");
         this.RuleFor(x => x.Title).Must(BeValidTitle).WithMessage($"'{{PropertyName}}' must be one of {string.Join(", ", TitleOptions)}");
      }

      private static bool BeValidTitle(string title)
      {
         if (title is null)
         {
            return true;
         }

         return TitleOptions.Any(o => o.Equals(title, StringComparison.InvariantCultureIgnoreCase));
      }

      private static bool BeAValidDate(string value)
      {
         return DateTime.TryParse(value, out DateTime date);
      }
   }
}

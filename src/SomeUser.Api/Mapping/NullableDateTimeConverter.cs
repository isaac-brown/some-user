// <copyright file="NullableDateTimeConverter.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Api.Mapping
{
   using System;
   using AutoMapper;

   /// <summary>
   /// Converts DateTime? to string.
   /// </summary>
   public class NullableDateTimeConverter : ITypeConverter<DateTime?, string>
   {
      /// <inheritdoc/>
      public string Convert(DateTime? source, string destination, ResolutionContext context)
      {
         if (source.HasValue)
         {
            return source.Value.ToString("yyyy-MM-dd");
         }
         else
         {
            return default;
         }
      }
   }
}

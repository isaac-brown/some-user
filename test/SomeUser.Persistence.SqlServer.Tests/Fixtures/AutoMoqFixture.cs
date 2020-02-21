// <copyright file="AutoMoqFixture.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Persistence.SqlServer.Tests.Fixtures
{
   using AutoFixture;
   using AutoFixture.AutoMoq;

   /// <summary>
   /// Helper class to create an <see cref="IFixture"/> with an <see cref="AutoMoqCustomization"/>.
   /// </summary>
   internal static class AutoMoqFixture
   {
      /// <summary>
      /// Creates a new <see cref="IFixture"/> instance with an <see cref="AutoMoqCustomization"/> configured.
      /// </summary>
      /// <returns>A new <see cref="IFixture"/> instance with an <see cref="AutoMoqCustomization"/> configured.</returns>
      internal static IFixture Create()
      {
         return new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true });
      }
   }
}

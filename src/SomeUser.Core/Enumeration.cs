// <copyright file="Enumeration.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Core
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;

  /// <summary>
  /// A base class that is used to create type safe enums. From https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/.
  /// </summary>
  public abstract class Enumeration : IComparable
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Enumeration"/> class
    /// using the given <paramref name="keyCode"/> and <paramref name="displayName"/>.
    /// </summary>
    /// <param name="keyCode">Shorthand identifier for the enumeration.</param>
    /// <param name="displayName">Longer more descriptive identifier for the enumeration, should be suitable for end users.</param>
    protected Enumeration(string keyCode, string displayName)
    {
      if (string.IsNullOrWhiteSpace(keyCode))
      {
        throw new ArgumentException("Argument cannot be null, empty or whitespace", nameof(keyCode));
      }

      if (string.IsNullOrWhiteSpace(displayName))
      {
        throw new ArgumentException("Argument cannot be null, empty or whitespace", nameof(displayName));
      }

      this.KeyCode = keyCode;
      this.DisplayName = displayName;
    }

    /// <summary>
    /// Gets the key code for this enumeration.
    /// </summary>
    public string KeyCode { get; }

    /// <summary>
    /// Gets the display name for this enumeration.
    /// </summary>
    public string DisplayName { get; }

    /// <summary>
    /// Checks if two <see cref="Enumeration"/>s are equivalent.
    /// </summary>
    /// <param name="obj1">The first <see cref="Enumeration"/> to compare.</param>
    /// <param name="obj2">The second <see cref="Enumeration"/> to compare.</param>
    /// <returns>true if the given <see cref="Enumeration"/>s are equivalent, otherwise false.</returns>
    public static bool operator ==(Enumeration obj1, Enumeration obj2)
    {
      if (obj1 is null || obj2 is null)
      {
        return false;
      }

      return obj1.Equals(obj2);
    }

    /// <summary>
    /// Checks if two <see cref="Enumeration"/>s are not equivalent.
    /// </summary>
    /// <param name="obj1">The first <see cref="Enumeration"/> to compare.</param>
    /// <param name="obj2">The second <see cref="Enumeration"/> to compare.</param>
    /// <returns>true if the given <see cref="Enumeration"/>s are not equivalent, otherwise false.</returns>
    public static bool operator !=(Enumeration obj1, Enumeration obj2)
    {
      return !(obj1 == obj2);
    }

    /// <summary>
    /// Gets all <see cref="Enumeration"/>s of the given type.
    /// </summary>
    /// <typeparam name="T">A super type of <see cref="Enumeration"/>.</typeparam>
    /// <returns>All <see cref="Enumeration"/>s of the given type.</returns>
    public static IEnumerable<T> GetAll<T>()
        where T : Enumeration
    {
      var propertyValues = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static)
                                    .Select(p => p.GetValue(null, null))
                                    .OfType<T>();

      return propertyValues;
    }

    /// <summary>
    /// Creates an <see cref="Enumeration"/> of the given type from the specified <paramref name="keyCode"/>.
    /// </summary>
    /// <typeparam name="T">A super type of <see cref="Enumeration"/>.</typeparam>
    /// <param name="keyCode">The value to construct the <see cref="Enumeration"/> from.</param>
    /// <returns>An <see cref="Enumeration"/> of the given type.</returns>
    public static T FromKeyCode<T>(string keyCode)
        where T : Enumeration
    {
      if (keyCode is null)
      {
        throw new ArgumentNullException(nameof(keyCode));
      }

      var matchingItem = Parse<string, T>(keyCode, nameof(keyCode), item => item.KeyCode == keyCode.ToUpperInvariant());
      return matchingItem;
    }

    /// <summary>
    /// Creates an <see cref="Enumeration"/> of the given type from the specified <paramref name="displayName"/>.
    /// </summary>
    /// <typeparam name="T">A super type of <see cref="Enumeration"/>.</typeparam>
    /// <param name="displayName">The value to construct the <see cref="Enumeration"/> from.</param>
    /// <returns>An <see cref="Enumeration"/> of the given type.</returns>
    public static T FromDisplayName<T>(string displayName)
        where T : Enumeration
    {
      if (displayName is null)
      {
        throw new ArgumentNullException(nameof(displayName));
      }

      var matchingItem = Parse<string, T>(displayName, nameof(displayName), item => item.DisplayName == displayName);
      return matchingItem;
    }

    /// <inheritdoc/>
    public override string ToString() => this.DisplayName;

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
      var otherValue = obj as Enumeration;
      bool otherValueIsEnumeration = otherValue is Enumeration;

      if (!otherValueIsEnumeration)
      {
        return false;
      }

      var typeMatches = this.GetType().Equals(obj.GetType());
      var valueMatches = this.KeyCode.Equals(otherValue.KeyCode);

      return typeMatches && valueMatches;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
      return this.KeyCode.GetHashCode();
    }

    /// <inheritdoc/>
    public int CompareTo(object other)
    {
      if (other is null)
      {
        throw new ArgumentNullException(nameof(other));
      }

      if (other is Enumeration enumeration)
      {
        return this.KeyCode.CompareTo(enumeration.KeyCode);
      }

      throw new ArgumentException(
        message: $"argument expected to be of type [{typeof(Enumeration)}], but found [{other.GetType()}]",
        paramName: nameof(other));
    }

    private static TEnumeration Parse<TValue, TEnumeration>(TValue value, string description, Func<TEnumeration, bool> predicate)
        where TEnumeration : Enumeration
    {
      TEnumeration matchingItem = GetAll<TEnumeration>().FirstOrDefault(predicate);

      if (matchingItem is null)
      {
        var message = $"'{value}' is not a valid {description} of {typeof(TEnumeration)}";
        throw new ApplicationException(message);
      }

      return matchingItem;
    }
  }
}

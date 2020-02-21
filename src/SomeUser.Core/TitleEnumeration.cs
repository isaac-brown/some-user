namespace SomeUser.Core
{
   public class UserTitle : Enumeration
   {
        private UserTitle(string keyCode, string displayName)
            : base(keyCode, displayName)
        {
        }

        public static UserTitle Dr => new UserTitle(nameof(Dr).ToUpperInvariant(), nameof(Dr));

        public static UserTitle Mr => new UserTitle(nameof(Mr).ToUpperInvariant(), nameof(Mr));

        public static UserTitle Mrs => new UserTitle(nameof(Mrs).ToUpperInvariant(), nameof(Mrs));

        public static UserTitle Ms => new UserTitle(nameof(Ms).ToUpperInvariant(), nameof(Ms));
   }
}

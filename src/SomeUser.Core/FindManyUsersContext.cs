using System;
using System.Collections.Generic;
using System.Text;

namespace SomeUser.Core
{
   public class FindManyUsersContext
   {
      public int Limit { get; set; } = 1000;

      public string FirstName { get; set; }

      public string LastName { get; set; }
   }
}

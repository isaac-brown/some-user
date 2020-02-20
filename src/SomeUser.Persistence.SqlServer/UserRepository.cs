// <copyright file="UserRepository.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Persistence.SqlServer
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Threading.Tasks;
   using AutoMapper;
   using Microsoft.EntityFrameworkCore;
   using SomeUser.Core;
   using SomeUser.Persistence.SqlServer.Entities;

   /// <summary>
   /// SQL Server implementation of <see cref="IUserRepository"/>.
   /// </summary>
   public class UserRepository : IUserRepository
   {
      private readonly SomeUserDbContext context;
      private readonly IMapper mapper;

      public UserRepository(SomeUserDbContext context, IMapper mapper)
      {
         this.context = context;
         this.mapper = mapper;
      }

      /// <inheritdoc/>
      public async Task CreateOneAsync(User user)
      {
         var userEntity = this.mapper.Map<UserEntity>(user);
         await this.context.AddAsync<UserEntity>(userEntity);
         await this.context.SaveChangesAsync();
      }

      /// <inheritdoc/>
      public async Task<bool> ExistsAsync(Guid userId)
      {
         return this.context.Users.Any(user => user.Id == userId);
      }

      /// <inheritdoc/>
      public async Task<IEnumerable<User>> FindManyAsync(FindManyUsersContext findManyUserContext = null)
      {
         findManyUserContext = findManyUserContext ?? new FindManyUsersContext();
         var query = this.context.Users.AsQueryable();

         if (!string.IsNullOrWhiteSpace(findManyUserContext.FirstName))
         {
            query = query.Where(user => user.FirstName.Equals(findManyUserContext.FirstName));
         }

         if (!string.IsNullOrWhiteSpace(findManyUserContext.LastName))
         {
            query = query.Where(user => user.LastName.Equals(findManyUserContext.LastName));
         }

         IList<UserEntity> userEntities = await query.Take(findManyUserContext.Limit).ToListAsync();
         return this.mapper.Map<User[]>(userEntities);
      }

      public async Task<bool> DeleteOneAsync(Guid userId)
      {
         if (!await this.ExistsAsync(userId))
         {
            return false;
         }

         UserEntity entity = await InternalFindOneAsync(userId);

         this.context.Remove<UserEntity>(entity);

         await this.context.SaveChangesAsync();

         return true;
      }

      private Task<UserEntity> InternalFindOneAsync(Guid userId)
      {
         return this.context.Users.Where(user => user.Id == userId).SingleAsync();
      }

      public async Task<User> FindOneAsync(Guid userId)
      {
         UserEntity entity = await this.InternalFindOneAsync(userId);
         User user = this.mapper.Map<User>(entity);
         return user;
      }
   }
}

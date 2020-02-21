// <copyright file="SomeUserDbContext.cs" company="Isaac Brown">
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SomeUser.Persistence.SqlServer
{
   using System;
   using Microsoft.EntityFrameworkCore;
   using SomeUser.Persistence.SqlServer.Entities;

   /// <summary>
   /// <see cref="DbContext"/> for users.
   /// </summary>
   public class SomeUserDbContext : DbContext
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="SomeUserDbContext"/> class.
      /// </summary>
      public SomeUserDbContext()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="SomeUserDbContext"/> class.
      /// </summary>
      /// <param name="options">The options for configuring this context.</param>
      public SomeUserDbContext(DbContextOptions options)
         : base(options)
      {
      }

      /// <summary>
      /// Gets or sets the set of users.
      /// </summary>
      public DbSet<UserEntity> Users { get; set; }

      /// <inheritdoc/>
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         base.OnConfiguring(optionsBuilder);
         if (optionsBuilder.IsConfigured)
         {
            return;
         }

         optionsBuilder.UseSqlServer("Server=localhost;User=sa;Password=Welcome12;Database=SomeUser");
      }

      /// <inheritdoc/>
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<UserEntity>()
                     .ToTable("User")
                     .HasKey(u => u.Id)
                     .IsClustered(false);

         modelBuilder.Entity<UserEntity>()
                     .Property(u => u.FirstName)
                     .IsRequired();

         modelBuilder.Entity<UserEntity>()
                     .Property(u => u.LastName)
                     .IsRequired();

         modelBuilder.Entity<UserEntity>()
                     .Property(u => u.Email)
                     .IsRequired();
      }
   }
}

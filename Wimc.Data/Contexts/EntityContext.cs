using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Mx.EntityFramework.Contracts;
using Wimc.Domain.Models;

namespace Wimc.Data.Contexts
{
    public class EntityContext : DbContext, IEntityContext
    {
        private readonly string _connectionString;
        private const string DefaultSchema = "dbo";

        public EntityContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Expose the underlying database context
        /// </summary>
        public DbContext Context => this;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: new Collection<int>());
                });


            base.OnConfiguring(optionsBuilder);
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Initialize the database model mapping
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapEntitiesToTable(modelBuilder);
        }

        private static void MapEntitiesToTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceContainer>()
                .ToTable("ResourceContainer", DefaultSchema)
                .Property(properties => properties.ContainerName)
                .IsUnicode(false);

            modelBuilder.Entity<ResourceContainer>()
                .Property(properties => properties.RawJson)
                .IsUnicode(false);
            
            modelBuilder.Entity<Resource>()
                .ToTable("Resource", DefaultSchema)
                .Property(properties => properties.ResourceName)
                .IsUnicode(false);

            modelBuilder.Entity<Resource>()
                .Property(properties => properties.ResourceType)
                .IsUnicode(false);

            modelBuilder.Entity<Resource>()
                .Property(properties => properties.ResourceDefinition)
                .IsUnicode(false);

            modelBuilder.Entity<Resource>()
                .Property(properties => properties.ResourceLocation)
                .IsUnicode(false);
            
            modelBuilder.Entity<Resource>()
                .Property(properties => properties.Notes)
                .IsUnicode(false);
        }
    }
}
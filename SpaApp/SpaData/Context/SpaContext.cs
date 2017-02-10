using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpaData.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaData.FluentMappings;
using SpaData.Context;

namespace SpaData.Context
{
    public class SpaContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public SpaContext(DbContextOptions<SpaContext> options)
        : base(options)
        { }

        public SpaContext() { }
        
        // Entity declarations
        public DbSet<Person> Person{ get; set; }

        // Entity Fluent Mappings
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .AddConfiguration(new PersonMapping())
                .AddConfiguration(new PersonMapping());
        }
    }

    internal static class ModelBuilderExtensions
    {
        public static ModelBuilder AddConfiguration<TEntity>(
          this ModelBuilder modelBuilder,
          EntityTypeConfiguration<TEntity> entityConfiguration) where TEntity : class
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Map);
            return modelBuilder;
        }
    }
}

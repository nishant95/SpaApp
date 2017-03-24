using Microsoft.EntityFrameworkCore;
using SpaData.Models;
using SpaData.FluentMappings;

namespace SpaData.Context
{
    public class SpaContext: DbContext
    {
        #region Constructors

        public SpaContext(DbContextOptions<SpaContext> options)
        : base(options)
        { }

        public SpaContext() { }

        #endregion

        #region Properties

        // Entity declarations
        public DbSet<Person> Person{ get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Overrides model creating to add Entity Fluent Mappings
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .AddConfiguration(new PersonMapping());
        }

        #endregion
    }

    /// <summary>
    /// Extension class to allow the mapping to be defined in separate classes.
    /// </summary>
    internal static class ModelBuilderExtensions
    {
        #region Methods

        /// <summary>
        /// Applies mapping to model builder.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="modelBuilder">ModelBuilder</param>
        /// <param name="entityConfiguration">EntityTypeConfiguration</param>
        /// <returns>ModelBuilder</returns>
        public static ModelBuilder AddConfiguration<TEntity>(
          this ModelBuilder modelBuilder,
          EntityTypeConfiguration<TEntity> entityConfiguration) where TEntity : class
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Map);
            return modelBuilder;
        }

        #endregion
    }
}

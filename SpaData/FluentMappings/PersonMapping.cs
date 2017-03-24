using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaData.Models;

namespace SpaData.FluentMappings
{
    /// <summary>
    /// Fluent mapping for Person Entity
    /// </summary>
    internal class PersonMapping: EntityTypeConfiguration<Person>
    {
        #region Methods

        /// <summary>
        /// Specifies the mappings to be applied on the Entity Type Builder.
        /// </summary>
        /// <param name="builder">EntityTypeBuilder</param>
        public override void Map(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(value => value.Id);
            builder.Property(value => value.Id).ValueGeneratedOnAdd();
            builder.Property(value => value.FirstName).HasMaxLength(50);
            builder.Property(value => value.MiddleName).HasMaxLength(50);
            builder.Property(value => value.LastName).HasMaxLength(50);
            builder.Property(value => value.Email).HasMaxLength(255);
            builder.Property(value => value.Dob);
        }

        #endregion
    }
}

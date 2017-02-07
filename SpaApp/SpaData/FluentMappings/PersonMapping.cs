using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaData.FluentMappings
{
    internal class PersonMapping: EntityTypeConfiguration<Person>
    {
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
    }
}

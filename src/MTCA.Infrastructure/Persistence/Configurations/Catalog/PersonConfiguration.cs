using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTCA.Domain.Models;
using MTCA.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Persistence.Configurations.Catalog;

internal class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable(TableNames.People, SchemaNames.Catalog);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.FirstName)
            .HasMaxLength(Person.FirstNameMaxLength)
            .IsRequired(true);

        builder.Property(p => p.LastName)
            .HasMaxLength(Person.LastNameMaxLength)
            .IsRequired(true);

    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MTCA.Domain.Constants;
using MTCA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Persistence.Configurations.Catalog;
internal class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable(TableNames.Countries, SchemaNames.Catalog);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Longitude)
            .HasColumnType("decimal(18,7)");

        builder.Property(x => x.Latitude)
            .HasColumnType("decimal(18,7)");

        builder
        .HasOne(x => x.Region)
            .WithMany(x => x.Countries)
            .HasForeignKey(x => x.RegionId);
    }
}
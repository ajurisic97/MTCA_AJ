using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MTCA.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCA.Domain.Models;

namespace MTCA.Infrastructure.Persistence.Configurations.Catalog;
internal class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable(TableNames.Cities, SchemaNames.Catalog);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Longitude)
            .HasColumnType("decimal(18,7)");

        builder.Property(x => x.Latitude)
            .HasColumnType("decimal(18,7)");

        builder
            .HasOne(x => x.Country)
            .WithMany(x => x.Cities)
            .HasForeignKey(x => x.CountryId);

        builder
            .HasOne(x => x.Region)
            .WithMany(x => x.Cities)
            .HasForeignKey(x => x.RegionId);
    }
}

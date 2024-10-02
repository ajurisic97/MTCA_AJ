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
internal class StreetConfiguration : IEntityTypeConfiguration<Street>
{
    public void Configure(EntityTypeBuilder<Street> builder)
    {
        builder.ToTable(TableNames.Streets, SchemaNames.Catalog);
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Longitude)
            .HasColumnType("decimal(18,7)");

        builder.Property(x => x.Latitude)
            .HasColumnType("decimal(18,7)");

        builder
            .HasOne(x => x.City)
            .WithMany(x => x.Streets)
            .HasForeignKey(x => x.CityId);

        builder
        .HasOne(x => x.Region)
            .WithMany(x => x.Streets)
            .HasForeignKey(x => x.RegionId);

    }
}
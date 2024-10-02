using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTCA.Domain.Constants;
using MTCA.Domain.Models;
using MTCA.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Persistence.Configurations.Catalog;
internal class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.ToTable(TableNames.Regions,SchemaNames.Catalog);
        builder.HasKey(x=>x.Id);

        builder.Property(x => x.Longitude)
            .HasColumnType("decimal(18,7)");

        builder.Property(x => x.Latitude)
            .HasColumnType("decimal(18,7)");

        builder.Property(x => x.RegionType).HasConversion(
        v => v.ToString(),
        v => (RegionTypeEnum)Enum.Parse(typeof(RegionTypeEnum), v));

        builder
            .HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .IsRequired(false);

    }
}

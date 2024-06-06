using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTCA.Domain.Models;
using MTCA.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Infrastructure.Persistence.Configurations.Identity;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Roles, SchemaNames.Identity);

        builder.HasKey(x => x.Id);


        builder.HasMany(x => x.Permissions)
                .WithMany()
                .UsingEntity<RolePermission>();

        builder.HasMany(x => x.Users)
                .WithMany(x => x.Roles)
                .UsingEntity<UserRole>();

    }
}

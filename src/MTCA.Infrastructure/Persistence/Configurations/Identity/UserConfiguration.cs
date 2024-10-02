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

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users, SchemaNames.Identity);

        builder.HasKey(t => t.Id);
        builder.Property(x => x.Username)
            .HasMaxLength(User.UsernameMaxLength)
            .IsRequired(true);

        builder.Property(x => x.Password)
            .HasMaxLength(User.PasswordMaxLength)
            .IsRequired(true);

        builder.HasOne(x => x.Person)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.PersonId)
            .IsRequired(false);

    }
}

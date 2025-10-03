using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder
                .HasIndex(user => user.Email)
                .IsUnique();

            builder
                .HasIndex(user => user.Username)
                .IsUnique();

            builder
                .Property(user => user.Role)
                .HasConversion(new EnumToStringConverter<UserRole>());
        }
    }
}

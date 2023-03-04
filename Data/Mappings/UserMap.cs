using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAPI.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            //Primary Key
            builder.HasKey(x=>x.Id);

            //Identity
            builder.Property(x=>x.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            //Propriedades específicas
            builder.Property(x=>x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Bio)
                .IsRequired(false);
            
            builder.Property(x=>x.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);
            
            builder.Property(x=>x.Image)
                .IsRequired(false);
            
            builder.Property(x=>x.PasswordHash)
                .IsRequired(false)
                .HasColumnName("PasswordHash")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            
            builder.Property(x=>x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            //Índices
            builder.HasIndex(x=>x.Slug, "IX_User_Slug")
                .IsUnique();
            
            builder
                .HasMany(x=>x.Roles)
                .WithMany(x=>x.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    role => role
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRole_RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    user => user
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRole_UserId")
                        .OnDelete(DeleteBehavior.Cascade));

        }
    }
}
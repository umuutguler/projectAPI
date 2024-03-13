using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "User",     // üye olan kullanıcılar
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Editor",    // Belirli alanları güncelleyen düzenleyen kullanıcılar, Sınırlı yetki
                    NormalizedName = "EDITOR"
                },
                new IdentityRole
                {
                    Name = "Admin",    // Sayfanın yönrtiminden sorumlu olan kullanıcı
                    NormalizedName = "ADMIN"
                }
            );
        }
    }
}
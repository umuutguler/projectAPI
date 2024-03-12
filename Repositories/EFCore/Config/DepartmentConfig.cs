using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.DepartmentId); // Primery Key
            builder.Property(d => d.DepartmentName).IsRequired();

            builder.HasData(
                new Department()
                {
                    DepartmentId = 1,
                    DepartmentName = "Yazılım Geliştirme"
                },
                new Department()
                {
                    DepartmentId = 2,
                    DepartmentName = "Test ve Kalite Güvence"
                },
                new Department()
                {
                    DepartmentId = 3,
                    DepartmentName = "Proje Yönetimi"
                },
                new Department()
                {
                    DepartmentId = 4,
                    DepartmentName = "Ürün Yönetimi"
                },
                new Department()
                {
                    DepartmentId = 5,
                    DepartmentName = "Satış ve Pazarlama"
                },
                new Department()
                {
                    DepartmentId = 6,
                    DepartmentName = "İnsan Kaynakları"
                },
                new Department()
                {
                    DepartmentId = 7,
                    DepartmentName = "Finans ve Muhasebe"
                },
                new Department()
                {
                    DepartmentId = 8,
                    DepartmentName = "Bilgi Teknolojileri (BT)"
                }
            );
        }
    }
}

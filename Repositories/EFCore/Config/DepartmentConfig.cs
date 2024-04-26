using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;

namespace Repositories.EFCore.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {


            builder.HasData(
                new Department()
                {
                    DepartmentId = ObjectId.GenerateNewId(),
                    DepartmentName = "Yazılım Geliştirme"
                },
                new Department()
                {
                    DepartmentId = ObjectId.GenerateNewId(),
                    DepartmentName = "Test ve Kalite Güvence"
                },
                new Department()
                {
                    DepartmentId = ObjectId.GenerateNewId(),
                    DepartmentName = "Proje Yönetimi"
                },
                new Department()
                {
                    DepartmentId = ObjectId.GenerateNewId(),
                    DepartmentName = "Ürün Yönetimi"
                },
                new Department()
                {
                    DepartmentId = ObjectId.GenerateNewId(),
                    DepartmentName = "Satış ve Pazarlama"
                },
                new Department()
                {
                    DepartmentId = ObjectId.GenerateNewId(),
                    DepartmentName = "İnsan Kaynakları"
                },
                new Department()
                {
                    DepartmentId = ObjectId.GenerateNewId(),
                    DepartmentName = "Finans ve Muhasebe"
                },
                new Department()
                {
                    DepartmentId = ObjectId.GenerateNewId(),
                    DepartmentName = "Bilgi Teknolojileri (BT)"
                }
            );
        }
    }
}

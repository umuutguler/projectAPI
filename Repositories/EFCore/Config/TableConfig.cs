using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Repositories.EFCore.Config
{
    public class TableConfig : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            // Table tablosunun yapılandırması
            builder.HasKey(t => t.Id); // Primary key belirleme
            builder.Property(t => t.Status).IsRequired(); // Örneğin, Status alanı zorunlu olabilir

            // Table ve Department arasındaki ilişki
            builder.HasOne(t => t.Department) // Bir Table bir Department'e ait olacak
                   .WithMany(d => d.Tables) // Bir Department birden fazla Table'a sahip olabilir
                   .HasForeignKey(t => t.DepartmentId) // Table tablosunda DepartmentId alanı ile dış anahtar ilişkisi
                   .OnDelete(DeleteBehavior.Cascade); // Opsiyonel: Department silindiğinde ilgili table'lar da silinir

            // Veritabanına önceden tanımlanmış Table verilerini ekleyelim
            builder.HasData(
                new Table { Id = 1, Status = false, DepartmentId = 1 },
                new Table { Id = 2, Status = false, DepartmentId = 2 },
                new Table { Id = 3, Status = false, DepartmentId = 2 }
                // Diğer Table verileri
            );
        }
    }
}
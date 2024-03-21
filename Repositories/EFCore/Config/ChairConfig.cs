using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class ChairConfig : IEntityTypeConfiguration<Chair>
    {
        public void Configure(EntityTypeBuilder<Chair> builder)
        {
            // Table tablosunun yapılandırması
            builder.HasKey(c => c.Id); // Primary key belirleme
            builder.Property(c => c.Status).IsRequired(); // Örneğin, Status alanı zorunlu olabilir

            // Table ve Department arasındaki ilişki
            builder.HasOne(c => c.Table) // Bir Table bir Department'e ait olacak
                   .WithMany(t => t.Chairs) // Bir Department birden fazla Table'a sahip olabilir
                   .HasForeignKey(t => t.TableId) // Table tablosunda DepartmentId alanı ile dış anahtar ilişkisi
                   .OnDelete(DeleteBehavior.Cascade); // Opsiyonel: Department silindiğinde ilgili table'lar da silinir

            // Veritabanına önceden tanımlanmış Table verilerini ekleyelim
            builder.HasData(
                new Chair { Id = 1, Status = false, Price = 10, TableId = 1 },
                new Chair { Id = 2, Status = false, Price = 5, TableId = 1 },
                new Chair { Id = 3, Status = false, Price = 15, TableId = 1 },
                new Chair { Id = 4, Status = false, Price = 12, TableId = 2 },
                new Chair { Id = 5, Status = false, Price = 15, TableId = 2 },
                new Chair { Id = 6, Status = false, Price = 10, TableId = 2 },
                new Chair { Id = 7, Status = false, Price = 8, TableId = 3 },
                new Chair { Id = 8, Status = false, Price = 10, TableId = 3 },
                new Chair { Id = 9, Status = false, Price = 5, TableId = 3 }
                // Diğer Table verileri
            );
        }
    }
}

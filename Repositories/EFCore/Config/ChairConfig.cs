using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson;
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
            // Veritabanına önceden tanımlanmış Table verilerini ekleyelim
            builder.HasData(
                new Chair { Id = ObjectId.GenerateNewId(), Status = false, Price = 10, TableId = 1 },
                new Chair { Id = ObjectId.GenerateNewId(), Status = false, Price = 5, TableId = 1 },
                new Chair { Id = ObjectId.GenerateNewId(), Status = false, Price = 15, TableId = 1 },
                new Chair { Id = ObjectId.GenerateNewId(), Status = false, Price = 12, TableId = 2 },
                new Chair { Id = ObjectId.GenerateNewId(), Status = false, Price = 15, TableId = 2 },
                new Chair { Id = ObjectId.GenerateNewId(), Status = false, Price = 10, TableId = 2 },
                new Chair { Id = ObjectId.GenerateNewId(), Status = false, Price = 8, TableId = 3 },
                new Chair { Id = ObjectId.GenerateNewId(), Status = false, Price = 10, TableId = 3 },
                new Chair { Id = ObjectId.GenerateNewId(), Status = false, Price = 5, TableId = 3 }
                // Diğer Table verileri
            );
        }
    }
}

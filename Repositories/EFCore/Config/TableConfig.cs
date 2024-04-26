using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using MongoDB.Bson;

namespace Repositories.EFCore.Config
{
    public class TableConfig : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {

            // Veritabanına önceden tanımlanmış Table verilerini ekleyelim
            builder.HasData(
                new Table { Id = ObjectId.GenerateNewId(), Status = false, DepartmentId = 1 },
                new Table { Id = ObjectId.GenerateNewId(), Status = false, DepartmentId = 2 },
                new Table { Id = ObjectId.GenerateNewId(), Status = false, DepartmentId = 2 }
                // Diğer Table verileri
            );
        }
    }
}
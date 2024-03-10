using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.LogModel
{
    public class LogDetails
    {
        public Object? ModelName { get; set; } // Obje olarak tanımlıyoruz. Çünkü context üzerinden kullanacağız
        public Object? Controller { get; set; }
        public Object? Action { get; set; }
        public Object? Id { get; set; }
        public Object? CreateAt { get; set; } // Log zamanı

        public LogDetails()
        {
            CreateAt = DateTime.UtcNow;
        }
        public override string ToString() =>
            JsonSerializer.Serialize(this); //Serialize

    }
}

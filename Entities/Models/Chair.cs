using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Entities.Models
{
    public class Chair
    {
        public ObjectId Id { get; set; }
        public Boolean Status { get; set; }
        public Decimal Price { get; set; }
        public int TableId { get; set; }

        public ICollection<ReservationInfo> ReservationInfos { get; set; }

    }
}

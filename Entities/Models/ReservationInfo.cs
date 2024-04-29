using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Entities.Models
{
    public class ReservationInfo
    {
        public ObjectId Id { get; set; }
        public String Status { get; set; }
        public Decimal ReservationPrice { get; set; }
        public int Duration { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public List<DateTime> Updatdate { get; set; }


        public string UserId { get; set; }
        public string ChairId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ReservationInfo
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public List<DateTime> Updatdate { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }

        public User User { get; set; }

        public int ChairId { get; set; }
        public Chair Chair { get; set; }

        public Boolean Status { get; set; }

 


    }
}

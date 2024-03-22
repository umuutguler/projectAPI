using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ReservationInfo
    {
        [Key]
        public int Id { get; set; }
        public String Status { get; set; }
        public Decimal ReservationPrice { get; set; }
        public int Duration { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public DateTime CreateDate { get; set; }
        public List<DateTime> Updatdate { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Chair")]
        public int ChairId { get; set; }
        public Chair Chair { get; set; }

        

 


    }
}

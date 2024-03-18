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
        public DateTime CreateDate { get; set; }
        public List<DateTime> Updatdate { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Chair")]
        public int ChairId { get; set; }
        public Chair Chair { get; set; }

        public Boolean Status { get; set; }

 


    }
}

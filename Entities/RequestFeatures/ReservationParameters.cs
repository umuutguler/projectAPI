using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class ReservationParameters : RequestParameters
    {
        public uint MinPrice { get; set; }  // uint negatif değer alamaz
        public uint MaxPrice { get; set; } = 1000;
        public bool ValidPriceRange => MaxPrice > MinPrice;
        
        public const string CurrentStatus  = "current";
        public const string NonCurrentStatus = "nonCurrent";
        public const string CancelledStatus = "cancelled";

        public string Status { get; set; } // Represents the status: "Current", "NonCurrent", "Cancelled"

    }
}

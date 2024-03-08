using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record ProductDtoForUpdate(int Id, String Title, decimal Price, string Description, DateTime CreatedDate, DateTime LastUpdate); // Bu tanımda da kullanılabilir. Bu şekilde de readonly'dir.

    /*
    public record ProductDtoForUpdate{
        public int Id { get; set; }
        public String Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
    */

}

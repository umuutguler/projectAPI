using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record ProductDtoForUpdate(int Id, String Title, decimal Price, string Description, DateTime CreatedDate, DateTime LastUpdate); // Bu tanımda da kullanılabilir. Bu şekilde de readonly'dir.


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Table
    {
        public int Id { get; set; }
        public Boolean Status { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}

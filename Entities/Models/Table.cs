using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Entities.Models
{
    public class Table
    {
        public ObjectId Id { get; set; }
        public Boolean Status { get; set; }

        public int DepartmentId { get; set; }

        public ICollection<Chair> Chairs { get; set; }
    }
}

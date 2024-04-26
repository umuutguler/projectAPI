using MongoDB.Bson;

namespace Entities.Models
{
    public class Department
    {
        public ObjectId DepartmentId { get; set; }
        public String? DepartmentName { get; set; }
        public ICollection<Table> Tables { get; set; }
    }
}

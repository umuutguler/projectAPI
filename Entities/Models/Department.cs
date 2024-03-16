namespace Entities.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public String? DepartmentName { get; set; }

        // Ref : Collection navigation property
        // public ICollection<User> Users { get; set; }
        public ICollection<Table> Tables { get; set; }
        
    }
}

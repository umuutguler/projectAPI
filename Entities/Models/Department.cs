namespace Entities.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public String? DepartmentName { get; set; }

        // Ref : Collection navigation property
        // public ICollection<User> Users { get; set; }
        public ICollection<Table> Tables { get; set; }
      //  public ICollection<Chair> Chairs { get; set; } bu ifade eklenirse eğer chairs kısmı ayrıca geliyor get methodu çağırılınca [
   /* {
        "departmentId": 1,
        "departmentName": "Yazılım Geliştirme",
        "tables": [
            {
                "id": 1,
                "status": false,
                "departmentId": 1,
                "chairs": null
            }
        ],
        "chairs": null
    },*/
    }
}

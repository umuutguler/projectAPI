namespace Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}

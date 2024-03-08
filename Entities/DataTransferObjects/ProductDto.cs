namespace Entities.DataTransferObjects
{
    public record ProductDto
    { 
        public int Id { get; init; }
        public String Title { get; set; }
        public decimal Price { get; init; }
        public string Description { get; init; }
        public DateTime CreatedDate { get; init; }
        public DateTime LastUpdate { get; init; }
    }

}

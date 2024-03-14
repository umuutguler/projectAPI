namespace Entities.RequestFeatures
{
    public class ProductParameters : RequestParameters
    {
        public uint MinPrice { get; set; }  // uint negatif değer alamaz
        public uint MaxPrice { get; set; } = 1000;
        public bool ValidPriceRange => MaxPrice > MinPrice;
        public String? SearchTerm { get; set; } // searching
        public uint? Price { get; set; }
        public ProductParameters()
        {
            OrderBy = "id";
        }
    }
}
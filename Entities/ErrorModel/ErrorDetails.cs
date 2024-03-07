using System.Text.Json;

namespace Entities.ErrorModel
{
    // Bu sınıf handler içerisinde kullanılacak
    public class ErrorDetails
    {
        // Bir hata olduğunda bizim için Status Code u önemli
        public int StatusCode { get; set; }
        public string? Message { get; set; } // ? -> null check yap

        public override string ToString()
        {
            return JsonSerializer.Serialize(this); // Serilaze ediyoruz. Bu ifade class ın tamamını ilgilendirdiği için "this" anahtar kelimesini kullanıyoruz
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record ProductDtoForManipulation // Dto lar record
    {
        // abstract record, base bir type new'lenmesi mümkün değil buradan türetilen sınıflar abstract olmamak koşuluyla newlenebilir doğrudan kullanabilir.
        // id üzerinde herhangi bir manipülasyon yok
        [Required(ErrorMessage = "Title is a required field")] // Mesajları özelleştirme
        [MinLength(2, ErrorMessage = "Title must consist of at least 2 characters")]
        [MaxLength(50, ErrorMessage = "Title must consist of at maxinum 50 characters")] // Kitap başlığı min 2 max 50 karakter olacak ve boş olamayacak 
        public String Title { get; init; }
        [Required(ErrorMessage = "Title is a required field")]
        [Range(10, 1000)] // Ürün fiyatları 10 ile 1000 arasında olacak
        public decimal Price { get; init; }
    }
}


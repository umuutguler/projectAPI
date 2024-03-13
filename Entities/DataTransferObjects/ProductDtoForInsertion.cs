using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record ProductDtoForInsertion : ProductDtoForManipulation // Ekleme işlemi için de bir tip tanımı
    {
    }
}

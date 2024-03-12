using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IDataShaper<T>
    {
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString);
        // IEnumerable<T> entities kaynağınından hangi alanları seçiyorsam ( string fieldsString) bunu liste olarak dönüyoruz
        ExpandoObject ShapeData(T entity, string fieldsString);
        // Liste Olarak değil tek bir nesne olarak dönecek
    }
}

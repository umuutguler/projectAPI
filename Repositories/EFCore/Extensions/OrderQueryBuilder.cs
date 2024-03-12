using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Extensions
{
    public static class OrderQueryBuilder
    {
        public static String CreateOrderQuery<T>(String orderByQueryString) // <T> ile generik olarak çalış -> farklı farklı sınıfları destekle
        {
            var orderParams = orderByQueryString.Trim().Split(','); // trim -> boşlu atma, split, -> , den bölme
                                                                    // books?orderBy=title,price şeklinde arama yapmak istedi. , ile title,price ifadesini böleceğiz ve buradan bir array elde edeceğiz
                                                                    // Bu dizinin 0 elemanında title 1 elemanında price olacak. orderParams ifadesi query string ten gelen alanları fieldları almamızı sağlayacak

            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance); // book nesnesinin property alacağız.
            // reflection kullanacağız, ilgili ifadeleri dinamik olarak property bilgilerini alacağız

            // orderParams ile query string ifadesini aldık
            // propertyInfos ile nesne üzerinden propertylerin bilgilerini aldık
            // orderParams propertyInfos ifadelerini eşleştireceğiz. Yani sorgudan aldığımız bilgilerle property deki bilgilerin uyuşmasını sağlayacağız

            var orderQueryBuilder = new StringBuilder(); // String sorgu ifadesi.

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(' ')[0];
                // query stringlerde tite,price desc şeklinde bir sıralama olsun. boşlukla split yaparak sıralama parametresinin desc olup olmadığını anlıyoruz.
                // title a-z, price desc -> price ı sıralayacağız boşluk var desc miş o zaman z-a 

                var objectProperty = propertyInfos
                    .FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, // property sorgudan gelen name ifadesine eşit
                    StringComparison.InvariantCultureIgnoreCase)); // küçük büyük harf ayrımını görmezden gel
                //query string ile nesnenin ilişkilendirmesi


                if (objectProperty is null)
                    continue;


                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                // desc ile bitiyorsa desc(azalan), bitmiyorsa acending(aartan)

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()}  {direction},");
                // Property den name bilgisini alıp string e çevir, direction(asending, descending) ve , koy

            }
            // foreach bittiğinde elimizde orderQueryBuilder string ifadesi var
            // orderQueryBuilder -> "title ascending, price descending, id acending," şeklinde bir ifademiz oluyor

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' '); // sondaki virgülün yerine boşluk ekle

            return orderQuery;
        }
    }
}

using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DataShaper<T> : IDataShaper<T>
        where T : class // T tip parametresinin bir referans tip olması gerektiğini belirtir. - bir değer tipi değil, bir sınıf tipi 
    {
        public PropertyInfo[] Properties { get; set; } // Çalıştığım nesnenin propları, birden fazla info ifadesine ihtiyacım olduğu için array şeklinde tanımlandı
        /* Eğer kitap nesnesi üzerinde çalışıyorsam, Buradaki Proporties ifadesi bunun altındaki proplara denk gelir
        Proplar -> Id, Title, Price - Proporties ifadesi bu 3 alanı kapsayacak şekilde dinamik olarak elde etmeli
        Ve bunu çalışma zamanında yapmalı. */

        /*OOP'de PropertyInfo[] Properties gibi referans tipli bir ifade tanımlanıyor ise
        bu ifadenin ya tanımlandığı yerde ya da constructorda başlatılması gerekir. 
        Bunun için referans almalıyız. Bunu new anahtar kelimesi ya da metod yardımıyla yapabiliriz*/
        public DataShaper() // Method yardımıyla referans alma
        {
            Properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);
            // GetProperties ile bu ifadenin array dönmesini sağladık. Dönen array PropertyInfo[]
        }
        // Çalışma zamanında gerekli olan nesnenin proplarının infolarını hemen bu nesne üretildiği anda alacak
        // ---------------

        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString) // Şekillendireceğim nesne liste ise liste dönecek şekilde
        {
            var requiredFields = GetRequiredProperties(fieldsString); // ihtiyaç duyduğumuz proplar
            return FetchData(entities, requiredFields);
        }

        public ExpandoObject ShapeData(T entity, string fieldsString) // Şekillendireceğim nesne tek bir nesne ise
        {
            var requiredProperties = GetRequiredProperties(fieldsString); // ihtiyaç duyduğumuz proplar
            return FetchDataForEntity(entity, requiredProperties); // şekillendirmiş olduğu veriyi verdi
        }


        // ---------------
        //  Seçilen elemanların bir listesini üretmek gerekiyor
        /* /book?fields=id, title ->
           -Query String Id ve Title istiyor
           -Book nesnesinde Id, Price ve Title var
           Bizim id ve title ı alıp veriyi bu şekilde şekillendirmem gerekiyor.
           Bu alanları seçerken String üzerinden seçemem, PropertyInfo şeklinde reflectiondaki sınıfa uygun olarak seçeceğiz
           Birden fazla alan da olduğu için (id, title) bunu bir liste olarak tanımlayıp geri döneceğiz.
        */
        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredFields = new List<PropertyInfo>(); // Liste tanımı
            if (!string.IsNullOrWhiteSpace(fieldsString))  //Eğer Şekillendirme isteniyor ise (field varsa) - Logic işletebilmek için fieldsString ifadesinin dolu olduğundan emin olmak gerekiyor
            {
                var fields = fieldsString.Split(',',  // , den ayırarak bir array elde edeceğiz, Bu array string array
                    StringSplitOptions.RemoveEmptyEntries); // Boş olan elemanları kaldır.
                // Örnek: field = ["id","title"]

                foreach (var field in fields) // Array'in her elemanı üzerinde dolaşacağız ve ilgili elemanlar boş değil ise bir property oluşturup requiredFields listesinde ekleyeceğiz
                // fields dizisindeki her bir eleman için belirli bir özelliği bulur ve bu özelliği Properties koleksiyonunda arar.
                {
                    var property = Properties  // Kaynak, sınıf üzerinde tanımladığım Properties ifadesi
                        .FirstOrDefault(pi => pi.Name.Equals(field.Trim(),
                        StringComparison.InvariantCultureIgnoreCase));// Trim, boşlukları kaldır, StringComparison küçük büyük harf umursama 
                    if (property is null)
                        continue;
                    requiredFields.Add(property);
                }
            }
            else // Şekillendirme yok ise
            {
                requiredFields = Properties.ToList();  // Tüm elemanların üç alanını da (id, title, price) requiredFields alanınıa taşımış olacağız.
            }

            return requiredFields;
        }

        private ExpandoObject FetchDataForEntity(T entity, // ExpandoObject -> runtime'da yap
            IEnumerable<PropertyInfo> requiredProperties)
        // Hangi elemanlara(proplar) ihtiyaç varsa bu elemanların değerlerini üretip key value ifadelerini oluşturup döneceğiz
        {
            var shapedObject = new ExpandoObject(); // ExpandoObject-> runtime da üretilecek

            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity); // id, title değeri ne ise bu bilgiyi aldık
                shapedObject.TryAdd(property.Name, objectPropertyValue); // shapedObject nesnesine property.Name(id, title) ve objectPropertyValue(1, Harry Potter)
            }
            return shapedObject; // obje dönüyoruz
        }

        private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities,
            IEnumerable<PropertyInfo> requiredProperties)
        // FetchDataForEntity işlemini tek bir nesne için değil de bir liste için yapıyoruz
        // FetchDataForEntity metodunda tek bir liste varken burada bir liste söz konusu
        {
            var shapedData = new List<ExpandoObject>(); // ExpandoObject Listesi oluşturuyoruz
            foreach (var entity in entities)
            {
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }
            return shapedData; // liste dönüyoruz
        }
    }
}

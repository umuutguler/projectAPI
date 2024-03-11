using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData() // Tanım yaptığımız ifadenin newlenmesi için en iyi yer constructor dut
            {
                TotalCount = count,
                PageSize = pageSize, // parametre üzerinde geliyor
                CurrentPage = pageNumber, // parametre üzerinde geliyor
                TotalPage = (int)Math.Ceiling(count / (double)pageSize) // toplam kayıt/sayfadaki kayıt sayısı - toplam sayfa sayısı
            };
            AddRange(items); // List<T> de gelen değerler neyse onu PagedList e taşımış olacağız.
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source,
            int pageNumber,
            int pageSize)
        {
            var count = source.Count(); // 
            var items = source
                .Skip((pageNumber - 1) * pageSize) // kadar kayıt atlayacağız
                .Take(pageSize) // alacağımız kayıt sayısı
                .ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}

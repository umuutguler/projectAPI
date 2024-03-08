using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace WebApi.Utilities.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));  // Çıkış formatına gelen istekleri header da almam lazım - gelen isteğin text/csv formatında olmasını istiyoruz
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }


        // Csv desteğini bütün nesnelerim iin vermek zorunda değilim. GetAllProduct için vereceğiz ama GetOneProduct için vermeyeceğiz
        protected override bool CanWriteType(Type? type)
        {
            if (typeof(ProductDto).IsAssignableFrom(type) ||  // ProductDto ya da ProductDto listesi ise true
                typeof(IEnumerable<ProductDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        private static void FormatCsv(StringBuilder buffer, ProductDto product)  // Çıktıyı üreteceğimiz yer
        {
            buffer.AppendLine($"{product.Id}, {product.Title}, {product.Price}, {product.Description}, {product.CreatedDate}, {product.LastUpdate}");
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<ProductDto>) // liste ise
            {
                foreach (var product in (IEnumerable<ProductDto>)context.Object)
                {
                    FormatCsv(buffer, product);
                }
            }
            else  // tek nesne
            {
                FormatCsv(buffer, (ProductDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString()); // StringBuilder ifadesini build ettik
        }
    }
}
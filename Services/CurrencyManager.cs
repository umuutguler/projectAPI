using System.Globalization;
using System.Text.Json;
using Services.Contracts;
namespace Services
{
    public class CurrencyManager : ICurrencyService
    {
        public CurrencyManager()
        {
        }

        public async Task<Decimal> GetUSDRate()
        {

            string url = "https://api.genelpara.com/embed/para-birimleri.json";
            string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);

            HttpResponseMessage response = await client.GetAsync(url); // GET isteği

            if (response.IsSuccessStatusCode) // İstek Başalı mı
            {
                // Yanıtın içeriğini JSON formatında alıp string'e dönüştürme
                string jsonContent = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonDocument.Parse(jsonContent).RootElement;
                var usdObject = jsonObject.GetProperty("USD");
                string dolar = usdObject.GetProperty("alis").GetString();
                return decimal.Parse(dolar, CultureInfo.InvariantCulture);
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
    }

}

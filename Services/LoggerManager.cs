using NLog;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoggerManager : ILoggerService
    {
        // Bir tane logger enjekte edeceğiz. static yapıyoruz. Bu logger ifadesi üretildiği zaman herkes bu logger ifadesini kullanabilsin.
        private static ILogger _logger = LogManager.GetCurrentClassLogger(); // Bu metot, çağrıldığı sınıfın türüne özgü bir logger nesnesi döndürür. 
        // _logger kullanılarak log mesajları belirli bir sınıfa ait olduğu belirtilebilir.


        public void LogDebug(string message) => _logger.Debug(message);  // debug ifadesi ile bir şey yapıyorsa _logger üzerinden debug'a ilgili mesajı gönder.

        public void LogError(string message) => _logger.Error(message);

        public void LogInf(string message) => _logger.Info(message);

        public void LogWarning(string message) => _logger.Warn(message);

    }
}

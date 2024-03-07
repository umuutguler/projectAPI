using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ILoggerService
    {
        void LogInf(string message);  // Information seviyesinde log almak için.
        void LogWarning(string message);  // Uyarı seviyesinde log almak için.
        void LogError(string message);  // Error seviyesinde log almak için.
        void LogDebug(string message);  // Debug seviyesinde log almak için.
    }
}

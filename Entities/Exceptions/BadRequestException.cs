using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class BadRequestException : Exception // BadRequest ifade eden class ve Exception üzerinden kalıtılıyor
     // abstract class - newlenmiyor. Bu sınıfftan türetilen sınıfların newlenir.
    {
        protected BadRequestException(string message) : // abstract class olduğu için protected
            base(message)   // Gelen string ifadesini base'e yani Exception classına gönderiyor
        {

        }

    }
}

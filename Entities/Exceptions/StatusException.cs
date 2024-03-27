using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class StatusException : BadRequestException
    {
        public StatusException() 
            : base("Status error!")
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ChairNotFoundException : NotFoundException
    {
        public ChairNotFoundException(int id)
            : base($"Chair with id : {id} could not found.")
        {
        }
    }
}

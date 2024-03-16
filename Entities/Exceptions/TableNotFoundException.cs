using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class TableNotFoundException : NotFoundException
    {
        public TableNotFoundException(int id)
            : base($"Table with id : {id} could not found.")
        {
        }
    }
}

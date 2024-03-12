using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class DepartmentNotFoundException : NotFoundException
    {
        public DepartmentNotFoundException(int id)
            : base($"Department with id : {id} could not found.")
        {
        }
    }
}

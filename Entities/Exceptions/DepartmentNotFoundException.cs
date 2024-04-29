using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Entities.Exceptions
{
    public sealed class DepartmentNotFoundException : NotFoundException
    {
        public DepartmentNotFoundException(ObjectId id)
            : base($"Department with id : {id} could not found.")
        {
        }
    }
}

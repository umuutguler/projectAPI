using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using MongoDB.Bson;

namespace Services.Contracts
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges);
        Task<Department> GetOneDepartmentByIdAsync(ObjectId id, bool trackChanges);


    // Create and Delete
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Services.Contracts
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges);
        Task<Department> GetOneDepartmentByIdAsync(int id, bool trackChanges);


    // Create and Delete
    }
}

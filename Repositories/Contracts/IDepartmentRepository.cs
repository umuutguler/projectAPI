using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges, bool includeRelated);
        Task<Department> GetOneDepartmentByIdAsync(int id, bool trackChanges, bool includeRelated);
        void CreateOneDepartment(Department department);
        void UpdateOneDepartment(Department department);
        void DeleteOneDepartment(Department department);
    }
}

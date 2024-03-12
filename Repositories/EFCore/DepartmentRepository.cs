using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.DepartmentName)
                .ToListAsync();
       

        public async Task<Department> GetOneDepartmentByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.DepartmentId.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateOneDepartment(Department department) => Create(department);

        public void DeleteOneDepartment(Department department) => Delete(department);

        public void UpdateOneDepartment(Department department) => Update(department);
    }
}

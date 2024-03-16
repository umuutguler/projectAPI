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


        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges, bool includeRelated)
        /*  await FindAll(trackChanges)
          .OrderBy(t => t.Id)
          .ToListAsync();*/
        {
            IQueryable<Department> query = FindAll(trackChanges).OrderBy(d => d.DepartmentId);

            if (includeRelated)
            {
                query = query.Include(d => d.Tables).ThenInclude(t => t.Chairs);
            }

            return await query.ToListAsync();
        }


        public async Task<Department> GetOneDepartmentByIdAsync(int id, bool trackChanges, bool includeRelated)
        /*=>
        await FindByCondition(c => c.DepartmentId.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
        */
        {
            IQueryable<Department> query = FindByCondition(d => d.DepartmentId.Equals(id), trackChanges);

            if (includeRelated)
            {
                query = query.Include(d => d.Tables);
            }

            return await query.SingleOrDefaultAsync();
        }
        public void CreateOneDepartment(Department department) => Create(department);

        public void DeleteOneDepartment(Department department) => Delete(department);

        public void UpdateOneDepartment(Department department) => Update(department);
    }
}

using Entities.Exceptions;
using Entities.Models;
using MongoDB.Bson;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IRepositoryManager _manager;
        public DepartmentManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync(bool trackChanges)
        {
            return await _manager
                .Department.GetList();
        }

        public async Task<Department> GetOneDepartmentByIdAsync(ObjectId id, bool trackChanges)
        {
            var department = await _manager
               .Department
               .GetById(id);
            if (department is null)
                throw new DepartmentNotFoundException(id);

            return department;
        }


    }
}

﻿using Entities.Exceptions;
using Entities.Models;
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
                .Department
                .GetAllDepartmentsAsync(trackChanges);
        }

        public async Task<Department> GetOneDepartmentByIdAsync(int id, bool trackChanges)
        {
            var department = await _manager
               .Department
               .GetOneDepartmentByIdAsync(id, trackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(id);

            return department;
        }
    }
}
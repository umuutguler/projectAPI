using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TableManager : ITableService
    {
        private readonly IRepositoryManager _manager;
        public TableManager(IRepositoryManager manager)
        {
            _manager = manager;
        }
        public async Task<IEnumerable<Table>> GetAllTablesAsync(bool trackChanges)
        {
            return await _manager
               .Table
               .GetAllTablesAsync(trackChanges, includeRelated: true);
        }

        public async Task<Table> GetOneTableByIdAsync(int id, bool trackChanges)
        {
            var table = await _manager
               .Table
               .GetOneTableByIdAsync(id, trackChanges, includeRelated: true);
            if (table is null)
                throw new TableNotFoundException(id);

            return table;
        }
    }
}

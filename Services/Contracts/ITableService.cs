using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAllTablesAsync(bool trackChanges);
        Task<Table> GetOneTableByIdAsync(int id, bool trackChanges);
        Task<Table> UpdateTableByIdAsync(int id, Table table, bool trackChanges);
        Task<Table> DeleteTableByIdAsync(int id, bool trackChanges);
    }
}

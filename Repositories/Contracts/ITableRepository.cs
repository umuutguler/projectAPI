using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Repositories.Contracts
{
    public interface ITableRepository :IRepositoryBase<Table>
    {
        Task<IEnumerable<Table>> GetAllTablesAsync(bool trackChanges);
        Task<Table> GetOneTableByIdAsync(int id, bool trackChanges);
        void CreateOneTable(Table table);
        void UpdateOneTable(Table table);
        void DeleteOneTable(Table table);
    }
}

using Entities.Models;
using MongoDB.Bson;
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
        Task<Table> GetOneTableByIdAsync(ObjectId id, bool trackChanges);
        Task<Table> UpdateTableByIdAsync(ObjectId id, Table table, bool trackChanges);
        Task DeleteTableByIdAsync(ObjectId id, bool trackChanges);
    }
}

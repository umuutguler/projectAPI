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
    public class TableRepository : RepositoryBase<Table>, ITableRepository
    {
        public TableRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(t => t.Id)
            .ToListAsync();

        public async Task<Table> GetOneTableByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(t => t.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateOneTable(Table table) => Create(table);

        public void DeleteOneTable(Table table) => Delete(table);

        public void UpdateOneTable(Table table) => Update(table);
    }
}

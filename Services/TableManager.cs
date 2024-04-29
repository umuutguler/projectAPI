using Entities.Exceptions;
using Entities.Models;
using MongoDB.Bson;
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
               .GetList();
        }

        public async Task<Table> GetOneTableByIdAsync(ObjectId id, bool trackChanges)
        {
            var table = await _manager
               .Table
               .GetById(id);
            if (table is null)
                throw new TableNotFoundException(id);

            return table;
        }

        public async Task<Table> UpdateTableByIdAsync(ObjectId id, Table updatedTable, bool trackChanges)
        {
            var table = await _manager
                .Table
                .GetById(id); // Güncellenmiş sandalyeyi almak için includeRelated: false kullanıyoruz
            if (table == null)
                throw new TableNotFoundException(id);

            table.Status = updatedTable.Status; // Güncelleme işlemleri, updatedChair içindeki özelliklere göre yapılmalıdır
            table.DepartmentId = updatedTable.DepartmentId;

            _manager.Table.Update(table); // Güncelleme işlemi
            await _manager.SaveAsync(); // Değişiklikleri kaydet

            return table;
        }

        public async Task DeleteTableByIdAsync(ObjectId id, bool trackChanges)
        {
            var table = await _manager.Table.GetById(id);

           /* foreach (var chair in table.Chairs)
            {
                _manager.Chair.Delete(chair);
            }*/

            _manager.Table.Delete(table.Id);
            await _manager.SaveAsync();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.MongoDB;

namespace Repositories.EFCore
{
    public class TableRepository : RepositoryBase<Table>, ITableRepository
    {
        public TableRepository(RepositoryContext context) : base(context)
        {
        }
    }
}

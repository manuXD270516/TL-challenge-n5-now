using Microsoft.EntityFrameworkCore;
using Persistence.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.impl
{
    public class InMemoryStrategy : DatabaseStrategyBase
    {
        private readonly DbContextOptions<DbContext> _options;

        public InMemoryStrategy(string connectionString) : base(connectionString)
        {
        }

        public override void ConfigureOptions(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("InMemoryN5PermissionsDb");
        }

        public override void UseDatabase(DbContext context)
        {
            throw new NotImplementedException();
        }
    }
}

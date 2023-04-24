using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.impl
{
    public class SqlServerDatabaseStrategy : DatabaseStrategyBase
    {
        public SqlServerDatabaseStrategy(string connectionString) : base(connectionString)
        {
        }

        public override void ConfigureOptions(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
        }

        public override void UseDatabase(DbContext context)
        {
            var sqlConnection = context.Database.GetDbConnection() as SqlConnection;
            sqlConnection?.ChangeDatabase("MyOtherDatabase");
        }
    }
}

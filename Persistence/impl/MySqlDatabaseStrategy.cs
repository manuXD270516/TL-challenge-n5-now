using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Persistence.options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.impl
{
    public class MySqlDatabaseStrategy : DatabaseStrategyBase
    {
        public MySqlDatabaseStrategy(string connectionString) : base(connectionString)
        {
        }

        public override void ConfigureOptions(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        public override void UseDatabase(DbContext context)
        {
            var mySqlConnection = context.Database.GetDbConnection() as MySqlConnection;
            mySqlConnection?.ChangeDatabase("MyOtherDatabase");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.options
{
    public interface IDatabaseStrategy
    {
        string GetConnectionString();
        void ConfigureOptions(DbContextOptionsBuilder optionsBuilder);
        void UseDatabase(DbContext context);
    }
}

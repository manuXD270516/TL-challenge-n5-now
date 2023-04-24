using Application.interfaces.permission;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.repositories.impl.relationaldb
{
    public class PermissionRepository : RelationalDatabaseRepository<Permission>, IPermissionRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public PermissionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }



    }
}

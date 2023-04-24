using Application.interfaces;
using Application.interfaces.permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.repositories.impl
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        
        public IPermissionRepository _permissionRepository { get; }
        public ISearchPermissionRepository _searchPermissionRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext, IPermissionRepository permissionRepository, ISearchPermissionRepository searchPermissionRepository)
        {
            this.dbContext = dbContext;

            _permissionRepository = permissionRepository;
            _searchPermissionRepository = searchPermissionRepository;
        }

        public void Dispose() => dbContext.Dispose();

        public void SaveChanges() => dbContext.SaveChanges();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.interfaces.permission;

namespace Application.interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IPermissionRepository _permissionRepository { get; }
        ISearchPermissionRepository _searchPermissionRepository { get; }

        void SaveChanges();
    }
}

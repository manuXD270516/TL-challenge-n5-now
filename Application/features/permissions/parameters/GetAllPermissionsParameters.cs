using Application.parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.features.permissions.parameters
{
    public class GetAllPermissionsParameters  : RequestParameter
    {
        public int permissionTypeId { get; set; }

    }
}

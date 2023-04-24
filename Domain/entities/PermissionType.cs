using Domain.commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class PermissionType : BaseEntity
    {
        public string description { get; set; }

        [InverseProperty(property: "permissionType")]
        public List<Permission> permissions { get; set; }

        public PermissionType()
        {
            permissions = new List<Permission>();
        }

    }
}

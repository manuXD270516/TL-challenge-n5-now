using Domain.commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Permission : BaseEntity
    {
        public string employeeForename { get; set; }
        
        public string employeeSurname { get; set; }
        
        public DateTime permissionDate { get; set; }

        public int permissionTypeId { get; set; }

        [ForeignKey(name: "permissionTypeId")]
        public PermissionType permissionType { get; set; }


        
    }
}

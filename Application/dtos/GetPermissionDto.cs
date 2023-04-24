using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.dtos
{
    public class GetPermissionDto
    {
        public int permissionId { get; set; }

        public string employeeForename { get; set; }

        public string employeeSurname { get; set; }

        public DateTime permissionDate { get; set; }

        public string permissionType { get; set; }

    }
}

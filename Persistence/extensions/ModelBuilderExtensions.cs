using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var permisionTypeList = new List<PermissionType>
            {
                new PermissionType
                {
                    description = "Permisos familiares",
                    isDeleted = 0,
                    lastUpdated = DateTime.UtcNow,
                    createdAt = DateTime.UtcNow,
                },
                new PermissionType
                {
                    description = "Permisos por salud",
                    isDeleted = 0,
                    lastUpdated = DateTime.UtcNow,
                    createdAt = DateTime.UtcNow,
                }
            };

            modelBuilder.Entity<PermissionType>().HasData(permisionTypeList.ToArray());

            modelBuilder.Entity<Permission>().HasData(
                new Permission
                {
                    employeeForename = "Romel",
                    employeeSurname = "Duran",
                    permissionType = permisionTypeList[0],
                    isDeleted = 0,
                    permissionDate = DateTime.UtcNow,
                    createdAt = DateTime.UtcNow,
                    lastUpdated = DateTime.UtcNow,
                    
                },
                 new Permission
                 {
                     employeeForename = "Javier",
                     employeeSurname = "Gonzales",
                     permissionType = permisionTypeList[1],
                     isDeleted = 0,
                     permissionDate = DateTime.UtcNow,
                     createdAt = DateTime.UtcNow,
                     lastUpdated = DateTime.UtcNow,

                 }
            );
        }
    }
}

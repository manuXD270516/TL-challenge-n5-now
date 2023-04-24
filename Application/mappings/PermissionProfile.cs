using Application.dtos;
using Application.features.permissions.commands;
using AutoMapper;
using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.mappings
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, GetPermissionDto>()
              .ForMember(dest => dest.permissionType, opt => opt.MapFrom(src => src.permissionType.description))
              .ForMember(dest => dest.permissionId, opt => opt.MapFrom(src => src.id));

            CreateMap<CreatePermissionCommand, Permission>();
            CreateMap<UpdatePermissionCommand, Permission>();
                //.ForPath(dst => dst.permissionTypeId, opt => opt.MapFrom(src => src.permissionTypeId));
        }
    }
}

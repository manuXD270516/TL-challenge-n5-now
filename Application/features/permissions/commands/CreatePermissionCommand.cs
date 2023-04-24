using Application.dtos;
using Application.interfaces;
using Application.wrappers;
using AutoMapper;
using Domain.entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.features.permissions.commands
{
    public class CreatePermissionCommand: IRequest<Response<GetPermissionDto>>
    {

        public string employeeForename { get; set; }

        public string employeeSurname { get; set; }

        public DateTime permissionDate { get; set; }

        public int permissionTypeId { get; set; }


    }

    public class CreatePermissionCommandHanlder : IRequestHandler<CreatePermissionCommand, Response<GetPermissionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePermissionCommandHanlder(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<GetPermissionDto>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissionForCreate = _mapper.Map<Permission>(request);
            
            // db persistence
            var result = await _unitOfWork._permissionRepository.addAsync(permissionForCreate);
            
            // elastic persistence
            await _unitOfWork._searchPermissionRepository.addAsync(permissionForCreate);

            var permissionResult = _mapper.Map<GetPermissionDto>(result);

            return new Response<GetPermissionDto>
            {
                data = permissionResult,
                message = "Permission was created succcessfully",
                successed = true
            };
        }
    }
}

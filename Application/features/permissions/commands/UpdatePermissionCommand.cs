using Application.dtos;
using Application.exceptions;
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
    public class UpdatePermissionCommand : IRequest<Response<GetPermissionDto>>
    {
        public int id { get; set; }
        
        public string employeeForename { get; set; }

        public string employeeSurname { get; set; }

        public DateTime permissionDate { get; set; }
        public int permissionTypeId { get; set; }

    }


    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, Response<GetPermissionDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Response<GetPermissionDto>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            
            var permissionForUpdate = _mapper.Map<Permission>(request);

            // database persistence
            var result = await _unitOfWork._permissionRepository.updateAsync(request.id, permissionForUpdate);

            // elastic persistence
            await _unitOfWork._searchPermissionRepository.updateAsync(request.id, permissionForUpdate);


            var permissionResult = _mapper.Map<GetPermissionDto>(result);

            return new Response<GetPermissionDto>
            {
                data = permissionResult,
                message = $"Permission with Id: {request.id} updated successfully"
            };
        }
    }

}

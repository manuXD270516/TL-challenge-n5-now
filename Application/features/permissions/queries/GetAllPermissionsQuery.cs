using Application.dtos;
using Application.interfaces;
using Application.parameters;
using Application.wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.helpers.HelpersConstApplication;

namespace Application.features.permissions.queries
{
    public class GetAllPermissionsQuery: IRequest<PageResponse<List<GetPermissionDto>>>
    {

        #region "Pagination Params"
        
        public int pageNumber { get; set; }
        public int pageSize { get; set; }

        #endregion


        #region "Filter params"
        
        public int permissionTypeId { get; set; }

        #endregion

        #region "Order Params"

        public string orderByDirection { get; set; }

        #endregion


    }


    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, PageResponse<List<GetPermissionDto>>>
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public GetAllPermissionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PageResponse<List<GetPermissionDto>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var pageableParam = new RequestParameter
            (
                pageNumber: request.pageNumber,
                pageSize: request.pageSize,
                orderByDirection: request.orderByDirection
            );

            Dictionary<string, int> additionalPropsFromRequest = new Dictionary<string, int>();

            var result = await _unitOfWork._permissionRepository.getAllAsync(
                includes:p => p.permissionType,
                additionalProps: additionalPropsFromRequest,
                pagination: pageableParam,
                filter: (f) => request.permissionTypeId == 0 || f.permissionType.id == request.permissionTypeId,
                orderBy: (ord) => string.IsNullOrEmpty(pageableParam.orderByDirection) || pageableParam.orderByDirection.Equals("ASC")
                            ? ord.OrderBy(p => p.id)
                            : ord.OrderByDescending(p => p.id)
            );

            List<GetPermissionDto> mapperList = _mapper.Map<List<GetPermissionDto>>(result);

            int count = additionalPropsFromRequest.GetValueOrDefault(KEY_TOTAL_COUNT);
            return new PageResponse<List<GetPermissionDto>>(
                mapperList,
                request.pageNumber,
                request.pageSize,
                count
            );
        }
    }
}

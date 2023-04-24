using Application.exceptions;
using Application.features.permissions.commands;
using Application.features.permissions.parameters;
using Application.features.permissions.queries;
using Microsoft.AspNetCore.Mvc;
using rest_server.Middlewares;
using static rest_server.Helpers.HelpersRequests;

namespace rest_server.Controllers.v1
{
    [ApiVersion(CURRENT_VERSION_API)]
    public class PermissionController : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetPermisisions([FromQuery] GetAllPermissionsParameters parameters)
        {

            return Ok(await Mediator.Send(new GetAllPermissionsQuery
            {
                pageNumber = parameters.pageNumber,
                pageSize = parameters.pageSize,
                permissionTypeId = parameters.permissionTypeId,
                orderByDirection = parameters.orderByDirection
            }));
        }

        [HttpPost]
        public async Task<IActionResult> RequestPermission([FromBody] CreatePermissionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut(ROUTE_PARAM_ID)]
        public async Task<IActionResult> ModifyPermission([FromBody] UpdatePermissionCommand command, [FromRoute] int id)
        {
            if (command.id != id)
            {
                throw new ApiException("Id's not matching, try again.");
            }
            return Ok(await Mediator.Send(command));
        }
    }
}

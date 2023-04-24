using Application.dtos;
using Application.features.permissions.queries;
using Application.interfaces;
using Application.parameters;
using Application.wrappers;
using AutoMapper;
using Domain.entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace units_test_n5_challenge.permissions.queries
{
    public class GetAllPermissionsQueryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public GetAllPermissionsQueryTests()
        {
            _unitOfWorkMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task handle_Should_ReturnSuccessData_WhenExistsItemsByPermissionTypeIdEquals2()
        {
            var mockPermissionsResultList = new List<Permission>
            {
                new Permission
                {
                    employeeForename = "Agustin",
                    employeeSurname = "Ordoñez"
                },
                new Permission
                {
                    employeeForename = "Franco",
                    employeeSurname = "MacAllister"
                }
            };

            var mockDtoPermissionsResultList = new List<GetPermissionDto>
            {
                new GetPermissionDto
                {
                    employeeForename = "Agustin",
                    employeeSurname = "Ordoñez"
                },
                new GetPermissionDto
                {
                    employeeForename = "Franco",
                    employeeSurname = "MacAllister"
                }
            };

            var query = new GetAllPermissionsQuery { permissionTypeId = 2 };
            var handler = new GetAllPermissionsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            var additionalProps = new Dictionary<string, int>
            {
                ["totalCount"] = 2
            };
            _unitOfWorkMock.Setup(m => m._permissionRepository.getAllAsync(additionalProps,
                It.IsAny<Expression<Func<Permission, bool>>>(), It.IsAny<Func<IQueryable<Permission>, IOrderedQueryable<Permission>>>(), It.IsAny<RequestParameter>(),
                null)).ReturnsAsync(mockPermissionsResultList);

            _mapperMock.Setup(m => m.Map<List<GetPermissionDto>>(It.IsAny<List<Permission>>()))
                .Returns(mockDtoPermissionsResultList);


            PageResponse<List<GetPermissionDto>> response = await handler.Handle(query, default);

            response.data.Count.Should().Be(2);

        }

        [Fact]
        public async Task handle_Should_ReturnSuccessData_WhenNotExistsItems()
        {
            var mockPermissionsResultList = new List<Permission>{ };

            var mockDtoPermissionsResultList = new List<GetPermissionDto>{};

            var query = new GetAllPermissionsQuery { };
            var handler = new GetAllPermissionsQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            var additionalProps = new Dictionary<string, int>
            {
                ["totalCount"] = 0
            };
            _unitOfWorkMock.Setup(m => m._permissionRepository.getAllAsync(additionalProps,
                It.IsAny<Expression<Func<Permission, bool>>>(), It.IsAny<Func<IQueryable<Permission>, IOrderedQueryable<Permission>>>(), It.IsAny<RequestParameter>(),
                null)).ReturnsAsync(mockPermissionsResultList);

            _mapperMock.Setup(m => m.Map<List<GetPermissionDto>>(It.IsAny<List<Permission>>()))
                .Returns(mockDtoPermissionsResultList);


            PageResponse<List<GetPermissionDto>> response = await handler.Handle(query, default);

            response.data.Should().BeEmpty();

        }
    }
}

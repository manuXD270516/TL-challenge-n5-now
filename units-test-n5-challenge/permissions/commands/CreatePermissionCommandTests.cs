using Application.dtos;
using Application.features.permissions.commands;
using Application.interfaces;
using Application.wrappers;
using AutoMapper;
using Domain.entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace units_test_n5_challenge.permissions.commands
{
    public class CreatePermissionCommandTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreatePermissionCommandTests()
        {
            _unitOfWorkMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task handle_Should_ReturnSuccessResultId_WhenDtoIsValid()
        {
            // Arrange
            var command = new CreatePermissionCommand
            {
                employeeForename = "Manuel",
                employeeSurname = "Saavedra",
                permissionDate = DateTime.Now,
                permissionTypeId = 10
            };
            var mockPermissionResult = new Permission
            {
                employeeForename = "Manuel",
                employeeSurname = "Saavedra",
                permissionDate = DateTime.Now
            };
            var mockDtoResult = new GetPermissionDto
            {
                employeeForename = "Manuel",
                employeeSurname = "Saavedra",
                permissionDate = DateTime.Now,
                permissionId = 1
            };

            _unitOfWorkMock.Setup(m => m._permissionRepository.addAsync(It.IsAny<Permission>())).ReturnsAsync(mockPermissionResult);

            _unitOfWorkMock.Setup(m => m._searchPermissionRepository.addAsync(It.IsAny<Permission>())).ReturnsAsync(mockPermissionResult);

            _mapperMock.Setup(m => m.Map<Permission>(It.IsAny<CreatePermissionCommand>()))
                .Returns(mockPermissionResult);

            _mapperMock.Setup(m => m.Map<GetPermissionDto>(It.IsAny<Permission>()))
                .Returns(mockDtoResult);
            var handler = new CreatePermissionCommandHanlder(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act
            Response<GetPermissionDto> response = await handler.Handle(command, default);

            // Assert
            response.successed.Should().BeTrue();
            response.data.permissionId.Should().Be(1);


        }

        [Fact]
        public async Task handle_Should_ReturnSuccessResultAndResponseDto_WhenDtoIsValid()
        {
            // Arrange
            var command = new CreatePermissionCommand
            {
                employeeForename = "Manuel",
                employeeSurname = "Saavedra",
                permissionDate = DateTime.Now,
                permissionTypeId = 10
            };
            var mockPermissionResult = new Permission
            {
                employeeForename = "Manuel",
                employeeSurname = "Saavedra",
                permissionDate = DateTime.Now
            };
            var mockDtoResult = new GetPermissionDto
            {
                employeeForename = "Manuel",
                employeeSurname = "Saavedra",
                permissionDate = DateTime.Now
            };

            _unitOfWorkMock.Setup(m => m._permissionRepository.addAsync(It.IsAny<Permission>())).ReturnsAsync(mockPermissionResult);

            _unitOfWorkMock.Setup(m => m._searchPermissionRepository.addAsync(It.IsAny<Permission>())).ReturnsAsync(mockPermissionResult);

            _mapperMock.Setup(m => m.Map<Permission>(It.IsAny<CreatePermissionCommand>()))
                .Returns(mockPermissionResult);

            _mapperMock.Setup(m => m.Map<GetPermissionDto>(It.IsAny<Permission>()))
                .Returns(mockDtoResult);

            var handler = new CreatePermissionCommandHanlder(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act
            Response<GetPermissionDto> response = await handler.Handle(command, default);

            // Assert
            response.successed.Should().BeTrue();
            response.data.Should().NotBeNull();


        }
    }
}

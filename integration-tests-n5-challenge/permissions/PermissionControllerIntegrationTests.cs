using Application.features.permissions.commands;
using integration_tests_n5_challenge.config;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integration_tests_n5_challenge.permissions
{
    public class PermissionControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public PermissionControllerIntegrationTests(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        [Fact]
        public async Task GetAllPermissions_WhenCalled_ReturnsData()
        {
            var response = await _client.GetAsync("/v1.0/Permission/GetPermisisions");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Romel", responseString);
            Assert.Contains("Javier", responseString);
        }

        [Fact]
        public async Task RequestPermission_WhenCalled_ReturnsNewPermission()
        {
            var permission = new CreatePermissionCommand
            {
                employeeForename = "Tavor",
                employeeSurname = "Gonzales",
                permissionDate = DateTime.Now,
                permissionTypeId = 1
            };

            var data = new StringContent(JsonConvert.SerializeObject(permission));
            var response = await _client.PostAsync("/v1.0/Permission/RequestPermission", data);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Tavor", responseString);
        }

        [Fact]
        public async Task ModifyPermission_WhenCalled_ReturnsPermissionModified()
        {
            var permission = new CreatePermissionCommand
            {
                employeeForename = "Javier actualizacion",
                employeeSurname = "Echeverria",
                permissionDate = DateTime.Now,
                permissionTypeId = 2
            };

            var data = new StringContent(JsonConvert.SerializeObject(permission));
            var response = await _client.PutAsync("/v1.0/Permission/ModifyPermission/2", data);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Javier actualizacion", responseString);
        }
    }



}

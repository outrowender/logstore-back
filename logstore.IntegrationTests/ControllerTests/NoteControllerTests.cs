using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net;
using FluentAssertions;

namespace logstore.IntegrationTests.ControllerTests
{
    public class NoteControllerTests : IntegrationTests
    {

        [Fact]
        public async Task shoud_return_unathorized_in_index_endpoints()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await _client.GetAsync("v1/notes");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}

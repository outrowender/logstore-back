using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net;

namespace logstore.IntegrationTests
{
    public class BasicTests : IntegrationTests
    {

        [Theory]
        [InlineData("v1/user")]
        [InlineData("v1/notes")]
        [InlineData("v1/auth")]
        public async Task shoud_found_main_endpoints(string url)
        {
            // Arrange
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.False(response.StatusCode == HttpStatusCode.NotFound);
        }
    }
}

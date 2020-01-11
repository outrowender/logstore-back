using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using logstore.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using logstore.Models.ViewModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore.Storage;

namespace logstore.IntegrationTests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly HttpClient _client;
        public static readonly InMemoryDatabaseRoot InMemoryDatabaseRoot = new InMemoryDatabaseRoot();

        protected IntegrationTests()
        {
            var factory = new WebApplicationFactory<Startup>()
            .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DataContext));

                        services.AddDbContext<DataContext>((sp, opt) =>
                        {
                            opt.UseInMemoryDatabase("TestDB", InMemoryDatabaseRoot);
                        });
                    });

                });

            _client = factory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            //adiciona o header de authentication por default nos requests
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJWTAsync());
        }

        protected async Task<string> GetJWTAsync()
        {
            var response = await _client.PostAsJsonAsync("v1/user", new UserViewModel
            {
                Name = "Wender Patrick",
                Email = "outrowender@gmail.com",
                Password = "Carrera911"
            });

            if (!response.IsSuccessStatusCode) return "";

            var registerResponse = await response.Content.ReadAsAsync<dynamic>();

            return registerResponse["token"] as string;
        }

    }
}

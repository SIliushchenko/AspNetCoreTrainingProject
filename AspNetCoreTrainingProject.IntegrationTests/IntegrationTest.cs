using AspNetCoreTrainingProject.Contracts.V1;
using AspNetCoreTrainingProject.Contracts.V1.Requests;
using AspNetCoreTrainingProject.Contracts.V1.Responses;
using AspNetCoreTrainingProject.Data;
using AspNetCoreTrainingProject.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace AspNetCoreTrainingProject.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient TestClient;
        private readonly IServiceProvider _serviceProvider;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    { 
                        services.RemoveAll(typeof(DbContextOptions<DataContext>));
                        services.AddDbContext<DataContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDB1");
                        });
                    });
                });
            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<Response<PostResponse>> CreatePostAsync(CreatePostRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Base + "posts", request);

            return (await response.Content.ReadFromJsonAsync<Response<PostResponse>>())!;
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Base + "Identity/register/", new UserRegistrationRequest
            {
                Email = "Test@gmail.com",
                Password = "Som@Pass1234"
            });

            var registrationResponse = await response.Content.ReadFromJsonAsync<AuthenticationResult>();

            return registrationResponse?.Token!;
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DataContext>();
            context?.Database.EnsureDeleted();
        }
    }
}

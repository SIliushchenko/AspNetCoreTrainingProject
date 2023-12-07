using AspNetCoreTrainingProject.Contracts.V1;
using AspNetCoreTrainingProject.Contracts.V1.Requests;
using AspNetCoreTrainingProject.Contracts.V1.Responses;
using AspNetCoreTrainingProject.Domain;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace AspNetCoreTrainingProject.IntegrationTests
{
    public class PostsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnsEmptyResponse()
        {
            await AuthenticateAsync();

            var response = await TestClient.GetAsync(ApiRoutes.Base + "posts");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadFromJsonAsync<PagedResponse<Post>>())!.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_AnyPosts_ReturnsCorrectResponse()
        {
            await AuthenticateAsync();
            var expectedPost = await CreatePostAsync(new CreatePostRequest
            {
                Name = "TestPostName"
            });

            var response = await TestClient.GetAsync(ApiRoutes.Base + $"posts/{expectedPost.Data.Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var actualPost = await response.Content.ReadFromJsonAsync<Response<Post?>>();
            actualPost.Should().NotBeNull();
            actualPost?.Data!.Id.Should().Be(expectedPost.Data.Id);
            actualPost?.Data!.Name.Should().Be("TestPostName");

            // Clean up
            await TestClient.DeleteAsync(ApiRoutes.Base + $"posts/{expectedPost.Data.Id}");
        }

        
    }
}
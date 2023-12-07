using AspNetCoreTrainingProject.Contracts.V1.Requests;
using Refit;

namespace AspNetCoreTrainingProject.SDK.Sample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var cachedToken = string.Empty;
            var identityApi = RestService.For<IIdentityApi>("https://localhost:44317");
            var postApi = RestService.For<IPostsApi>("https://localhost:44317", new RefitSettings
            {
                AuthorizationHeaderValueGetter = (_, _) => Task.FromResult(cachedToken)
            });

            //var registerResponse = await identityApi.RegisterAsync(new UserRegistrationRequest
            //{
            //    Email = "serhii1111@gmail.com",
            //    Password = "$iriuS22041989"
            //});

            var loginResponse = await identityApi.LoginAsync(new UserLoginRequest
            {
                Email = "serhii1111@gmail.com",
                Password = "$iriuS22041989"
            });

            cachedToken = loginResponse.Content?.Token;

            var allPosts = await postApi.GetAllAsync();

            var createdPost = await postApi.CreateAsync(new CreatePostRequest
            {
                Name = "CreatedBySDK",
                Tags = new List<string>
                {
                    "SDK"
                }
            });

            var post = await postApi.GetAsync(createdPost.Content!.Id);
        }
    }
}
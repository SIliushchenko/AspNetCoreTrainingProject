namespace AspNetCoreTrainingProject.Contracts.V1.Requests
{
    public class CreatePostRequest
    {
        public string Name { get; set; } = null!;

        public IEnumerable<string> Tags { get; set; } = new List<string>();
    }
}

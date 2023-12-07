using Cosmonaut.Attributes;
using Newtonsoft.Json;

namespace AspNetCoreTrainingProject.Domain
{
    [CosmosCollection("posts")]
    public class CosmosPostDto
    {
        [CosmosPartitionKey]
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}

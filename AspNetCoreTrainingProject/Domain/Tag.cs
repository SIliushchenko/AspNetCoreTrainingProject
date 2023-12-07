using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AspNetCoreTrainingProject.Domain
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        [ForeignKey("Post")]
        public Guid PostId { get; set; }

        [JsonIgnore]
        public Post Post { get; set; } = null!;
    }
}

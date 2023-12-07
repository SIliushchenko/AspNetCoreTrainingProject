namespace AspNetCoreTrainingProject.Options
{
    public class JwtSettings
    {
        public string Secret{ get; set; } = null!;

        public TimeSpan TokenLifetime { get; set; }
    }
}

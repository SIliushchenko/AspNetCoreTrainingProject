namespace AspNetCoreTrainingProject.Domain
{
    public class AuthenticationResult
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; } = null!;
    }
}

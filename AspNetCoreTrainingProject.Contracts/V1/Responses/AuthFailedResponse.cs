namespace AspNetCoreTrainingProject.Contracts.V1.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}

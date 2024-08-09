namespace Fincompare.Application.Models
{
    public class ValidationErrorResponse
    {
        public bool Success { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}

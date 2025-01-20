namespace Identity.Application.Contracts.Models
{
    public class UserProfileResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public UserProfileDto? Data { get; set; }
    }
}

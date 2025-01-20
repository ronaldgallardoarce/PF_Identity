namespace Identity.Application.Contracts.Models
{
    public class UserProfileDto
    {
        public string Email { get; set; } = string.Empty;
        public string? Ci { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
    }
}

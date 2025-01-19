using Microsoft.AspNetCore.Identity;


namespace Identity.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }

        // Relación con Identity
        public ApplicationUser User { get; set; }
    }
}

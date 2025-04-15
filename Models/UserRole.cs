using Microsoft.AspNetCore.Identity;

namespace MusicAppApi.Models
{
    public class UserRole : IdentityUserRole<string>
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}

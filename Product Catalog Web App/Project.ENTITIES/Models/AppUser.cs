using Project.ENTITIES.Enums;

namespace Project.ENTITIES.Models
{
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public UserRole Role { get; set; }

        public AppUser()
        {
            IsVerified = false;
            Role = Enums.UserRole.Member;
        }
    }
}

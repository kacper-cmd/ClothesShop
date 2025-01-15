using Data;

namespace WebsiteProjectAPI.ViewModel
{
    public class UserForView
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsActive { get; set; }

        public int? UserRolesId { get; set; }

        public int AddressesId { get; set; }

        public virtual Address Addresses { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}

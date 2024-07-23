using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class User : IdentityUser
    {
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public DateTimeOffset? BirthDate { get; set; } // để giảm giá thôi
    }
}

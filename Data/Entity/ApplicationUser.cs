using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(50)")]
        public string? FullName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Address { get; set; }
        [Column(TypeName = "nvarchar(400)")]
        public string? Avatar { get; set; }
    }
}

namespace Services.Models.UserModel
{
    public class UserForView
    {
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public DateTimeOffset? BirthDate { get; set; } // để giảm giá thôi
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}

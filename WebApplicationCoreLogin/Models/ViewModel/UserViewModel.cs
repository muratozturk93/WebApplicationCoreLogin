using System.ComponentModel.DataAnnotations;

namespace WebApplicationCoreLogin.Models.ViewModel
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        [StringLength(20)]
        public string? Name { get; set; } = null;
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Activate { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";
    }
}

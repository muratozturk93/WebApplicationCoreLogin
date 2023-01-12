using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplicationCoreLogin.Models
{
	public class User
	{
		[Key]
		public Guid Id { get; set; }
		[StringLength(20)]
		public string? Name { get; set; } = null;
		[Required]
		[StringLength(30)]
		public string Username { get; set; }
		[Required,StringLength(100)]
		public string Password { get; set; }
		public bool Activate { get; set; } = false;
		public DateTime CreatedDate { get; set; }=DateTime.Now;

		[Required]
		[StringLength(50)]
		public string Role { get; set; } = "user";

	}
}

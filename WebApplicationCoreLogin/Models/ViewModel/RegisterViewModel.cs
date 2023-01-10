using System.ComponentModel.DataAnnotations;

namespace WebApplicationCoreLogin.Models.ViewModel
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Kullanıcı Adı alanı zorunludur")]
		[StringLength(30, ErrorMessage = "Kullanıcı Adı alanı maksimum 30 karakter olabilir")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Şifre alanı zorunludur")]
		[MinLength(6)]
		[MaxLength(16)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Şifre alanı zorunludur")]
		[MinLength(6)]
		[MaxLength(16)]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string Password2 { get; set; }

	}
}

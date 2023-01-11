using System.ComponentModel.DataAnnotations;

namespace WebApplicationCoreLogin.Models.ViewModel
{
	public class LoginViewModel
	{
		[Required(ErrorMessage ="Kullanıcı Adı alanı zorunludur")]
		[StringLength(30,ErrorMessage ="Kullanıcı Adı alanı maksimum 30 karakter olabilir")]
		public string Username { get; set; }
		[Required(ErrorMessage ="Şifre alanı zorunludur")]
		[MinLength(6)]
		[MaxLength(100)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

	}
}

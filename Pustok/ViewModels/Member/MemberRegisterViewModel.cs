using System.ComponentModel.DataAnnotations;

namespace Pustok.ViewModels.Member
{
	public class MemberRegisterViewModel
	{
		[Required]
		[StringLength (maximumLength:30)]
		public string FullName { get; set; }
		[Required]
		[StringLength(maximumLength: 30)]	
		public string UserName { get; set; }
		[Required]
		[StringLength(maximumLength: 100)]

		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[StringLength(maximumLength: 30,MinimumLength =8)]
		public string Password { get; set; }
		[Required]
		[StringLength(maximumLength: 30, MinimumLength = 8)]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ComfirimPassword { get; set; }
	}
}

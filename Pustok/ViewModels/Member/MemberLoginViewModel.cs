using System.ComponentModel.DataAnnotations;

namespace Pustok.ViewModels.Member
{
	public class MemberLoginViewModel
	{
		[Required]
		[StringLength(maximumLength: 30)]
		public string UserName { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[StringLength(maximumLength: 30, MinimumLength = 8)]
		public string Password { get; set; }
	}
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectManager.LibEntity;

namespace ProjectManager.Internal.Areas.UserManagement.Pages;

public class AllModel : PageModel
{
	private readonly UserManager<UserEntity> _userManager;
	
	public IEnumerable<UserEntity> Users { get; set; }

	public AllModel(UserManager<UserEntity> userManager)
	{
		_userManager = userManager;
		
		Users = _userManager.Users;
	}
}
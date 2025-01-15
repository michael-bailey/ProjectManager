namespace ProjectManager.StaticWebApplication.Models;

public class UsersModel(string[] userNames)
{
	
	public string[] UserNames { get; init; } = userNames;
}
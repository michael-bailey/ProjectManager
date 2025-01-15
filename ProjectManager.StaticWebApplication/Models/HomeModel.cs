namespace ProjectManager.StaticWebApplication.Models;

public class HomeModel(bool isSignedIn = false)
{
	public bool IsSignedIn { get; init; } = isSignedIn;
	public string Message { get; } = "Hello world!";
}
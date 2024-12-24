namespace BackEnd.Attribute;

[AttributeUsage(AttributeTargets.Class)]
public class ProjectAttribute : System.Attribute
{
	public required string Name;
}
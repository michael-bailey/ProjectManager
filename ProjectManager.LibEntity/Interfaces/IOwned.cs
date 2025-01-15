namespace ProjectManager.LibEntity;

public interface IOwned<TOwner>
{
	public TOwner Owner { get; set; }
}
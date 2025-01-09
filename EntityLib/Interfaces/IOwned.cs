namespace EntityLib;

public interface IOwned<TOwner>
{
	public TOwner Owner { get; set; }
}
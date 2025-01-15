using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using ProjectManager.StaticWebApplication.Data;

namespace ProjectManager.StaticWebApplicaiton.Tests.tmp.Utils;

public class UserTestStore : IUserStore<UserAccount>
{
	
	public readonly Dictionary<Guid, UserAccount> _users = [];
	
	public void Dispose()
	{ }

	public Task<string> GetUserIdAsync(
		UserAccount user, 
		CancellationToken cancellationToken) => Task.FromResult(user.Id.ToString());
	
	public Task<string?> GetUserNameAsync(
		UserAccount user, 
		CancellationToken cancellationToken) => Task.FromResult(user.UserName);
	
	public Task SetUserNameAsync(
		UserAccount user, 
		string? userName,
		CancellationToken cancellationToken)
	{
    ArgumentNullException.ThrowIfNull(userName);

    user.UserName = userName;

		return Task.CompletedTask;
	}

	public Task<string?> GetNormalizedUserNameAsync(UserAccount user,
		CancellationToken cancellationToken) => Task.FromResult(user.NormalizedUserName);

	public Task SetNormalizedUserNameAsync(UserAccount user, string? normalizedName,
		CancellationToken cancellationToken)
	{
	  ArgumentNullException.ThrowIfNull(normalizedName);

    user.NormalizedUserName = normalizedName;

		return Task.CompletedTask;
	}

	public Task<IdentityResult> CreateAsync(UserAccount user, CancellationToken cancellationToken)
	{
		var id = Guid.NewGuid();
		user.Id = id;
		_users.Add(id, user);

		return Task.FromResult(IdentityResult.Success);
	}

	public Task<IdentityResult> UpdateAsync(UserAccount user, CancellationToken cancellationToken)
	{
		_users.Add(user.Id, user);
		return Task.FromResult(IdentityResult.Success);
	}

	public Task<IdentityResult> DeleteAsync(UserAccount user, CancellationToken cancellationToken)
	{
		_users.Remove(user.Id);
		return Task.FromResult(IdentityResult.Success);
	}

	public Task<UserAccount?> FindByIdAsync(string userId, CancellationToken cancellationToken)
	{
		return Task.FromResult((from user in _users
														where user.Key == Guid.Parse(userId)
														select user.Value).FirstOrDefault());
	}

	public Task<UserAccount?> FindByNameAsync(string normalizedUserName,
		CancellationToken cancellationToken)
	{
		return Task.FromResult((from user in _users
														where user.Value.NormalizedUserName == normalizedUserName
														select user.Value).FirstOrDefault());
	}
}
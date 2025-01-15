using Microsoft.AspNetCore.Identity;
using ProjectManager.StaticWebApplicaiton.Tests.tmp.Utils;
using ProjectManager.StaticWebApplication.Data;

namespace ProjectManager.StaticWebApplicaiton.Tests;

public class UserTestStoreTest
{

	private readonly UserTestStore _store = new UserTestStore();
	
	[Fact]
	public async Task AddUserToStore()
	{

		var token = CancellationToken.None;
		
		var testUser = CreateUninitalisedAccount();

		var res = await _store.CreateAsync(testUser, token);

		Assert.Equal(IdentityResult.Success, res);
		Assert.Contains(testUser, _store._users.Values);
	}
	
	[Fact]
	public async Task AddAndRemoveUserFromStore()
	{

		var token = CancellationToken.None;
		
		var testUser = CreateUninitalisedAccount();

		var res = await _store.CreateAsync(testUser, token);

		Assert.Equal(IdentityResult.Success, res);
		Assert.Contains(testUser, _store._users.Values);

		res = await _store.DeleteAsync(testUser, token);
		
		Assert.Equal(IdentityResult.Success, res);
		Assert.Empty(_store._users);
	}
	
	[Fact]
	public async Task GetUserFromUsernameFromStore()
	{

		var token = CancellationToken.None;
		
		var testUser = CreateUninitalisedAccount();

		var res = await _store.CreateAsync(testUser, token);

		var user = _store.FindByNameAsync(testUser.UserName, token);
		
		Assert.Equal(IdentityResult.Success, res);
		Assert.Contains(testUser, _store._users.Values);
	}
	
	[Fact]
	public async Task GetUserFromEmailFromStore()
	{

		var token = CancellationToken.None;
		
		var testUser = CreateUninitalisedAccount();

		var res = await _store.CreateAsync(testUser, token);

		Assert.Equal(IdentityResult.Success, res);
		Assert.Contains(testUser, _store._users.Values);
	}
	
	[Fact]
	public async Task GetUserFromIdFromStore()
	{

		var token = CancellationToken.None;
		
		var testUser = CreateUninitalisedAccount();

		var res = await _store.CreateAsync(testUser, token);

		Assert.Equal(IdentityResult.Success, res);
		Assert.Contains(testUser, _store._users.Values);
	}

	private UserAccount CreateUninitalisedAccount(
		string username = "TestUser",
		string email = "TestUser@example.com")
	{
		var user = new UserAccount
		{
			UserName = username,
			NormalizedUserName = null,
			Email = email,
			NormalizedEmail = null,
			EmailConfirmed = false,
			PasswordHash = null,
			SecurityStamp = null,
			ConcurrencyStamp = null,
			PhoneNumber = null,
			PhoneNumberConfirmed = false,
			TwoFactorEnabled = false,
			LockoutEnd = null,
			LockoutEnabled = false,
			AccessFailedCount = 0
		};

		return user;
	}
}
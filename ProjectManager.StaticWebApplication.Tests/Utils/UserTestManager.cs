using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectManager.StaticWebApplication.Data;

namespace ProjectManager.StaticWebApplicaiton.Tests.tmp.Utils;

public class UserTestManager(
	IUserStore<UserAccount> store,
	IOptions<IdentityOptions> optionsAccessor,
	IPasswordHasher<UserAccount> passwordHasher,
	IEnumerable<IUserValidator<UserAccount>> userValidators,
	IEnumerable<IPasswordValidator<UserAccount>> passwordValidators,
	ILookupNormalizer keyNormalizer,
	IdentityErrorDescriber errors,
	IServiceProvider services,
	ILogger<UserManager<UserAccount>> logger)
	: UserManager<UserAccount>(store, optionsAccessor, passwordHasher,
		userValidators, passwordValidators,
		keyNormalizer, errors, services, logger)
{
	
}
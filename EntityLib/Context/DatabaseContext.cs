using EntityLib.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EntityLib.Context;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) 
	: IdentityDbContext<UserEntity, UserRole, Guid>(options)
{
	public DbSet<TaskEntity> Tasks { get; set; }
}
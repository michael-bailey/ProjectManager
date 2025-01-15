using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManager.LibEntity.Authentication;

namespace ProjectManager.LibEntity.Context;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) 
	: IdentityDbContext<UserEntity, UserRole, Guid>(options)
{
	public DbSet<TaskEntity> Tasks { get; set; }
}
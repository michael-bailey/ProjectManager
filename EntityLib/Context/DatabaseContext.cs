using Microsoft.EntityFrameworkCore;

namespace EntityLib.Context;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
	public DbSet<TaskEntity> Tasks { get; set; }
}
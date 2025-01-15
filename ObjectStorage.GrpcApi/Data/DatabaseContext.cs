using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ObjectStorage.GrpcApi.Data;

public class DatabaseContext : DbContext
{
	public DbSet<BinaryObjectData> BinaryObjectDataSet { get; set; }
}
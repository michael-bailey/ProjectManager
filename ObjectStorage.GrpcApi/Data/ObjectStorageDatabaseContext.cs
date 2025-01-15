using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ObjectStorage.GrpcApi.Data;

public class ObjectStorageDatabaseContext(DbContextOptions opts) : DbContext(opts)
{
	public DbSet<BinaryObjectData> BinaryObjectDataSet { get; set; }
}
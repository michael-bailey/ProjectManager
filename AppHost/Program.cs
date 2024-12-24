var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
	.WithDataVolume();

var db = sql.AddDatabase("database");

var migration = builder.AddProject<Projects.MigrationService>("migrations")
	.WithReference(db)
	.WaitFor(db);

var backend = builder.AddProject<Projects.BackEnd>("backend")
	.WithReference(db)
	.WaitFor(db)
	.WaitFor(migration);

var frontend = builder.AddProject<Projects.FrontEnd>("frontend")
	.WithReference(backend)
	.WaitFor(backend);

builder.Build().Run();
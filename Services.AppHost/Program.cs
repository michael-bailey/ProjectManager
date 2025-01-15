var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
	.WithDataVolume();

var mainDb = sql.AddDatabase("database");

var objectDb = sql.AddDatabase("object-database");

var migration = builder
	.AddProject<Projects.ProjectManager_MigrationWorker>("migrations")
	.WithReference(mainDb)
	.WaitFor(mainDb);

var restApi = builder
	.AddProject<Projects.ProjectManager_RestApi>("rest-api")
	.WithReference(mainDb)
	.WaitFor(mainDb)
	.WaitFor(migration);

var internalUi = builder
	.AddProject<Projects.ProjectManager_Internal>("internal")
	.WithReference(mainDb)
	.WaitFor(mainDb)
	.WaitFor(migration);

var objectService = builder
	.AddProject<Projects.ObjectStorage_GrpcApi>("object-storage")
	.WithReference(objectDb)
	.WaitFor(objectDb);

builder.Build().Run();
var builder = DistributedApplication.CreateBuilder(args);

// Mark: Databases
var sql = builder.AddSqlServer("sql")
	.WithDataVolume();

var mainDb = sql.AddDatabase("database");

var postgresSql = builder.AddPostgres("postgres-sql");

var objectDb = postgresSql.AddDatabase("object-database");

// Mark end: Databases

// Mark: Project manager
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

// Mark end: Project manager

// Mark: Object storage system
var objectServiceMigration = builder
	.AddProject<Projects.ObjectStorage_MigrationWorker>("object-storage-migrations")
	.WithReference(objectDb)
	.WaitFor(objectDb);

var objectService = builder
	.AddProject<Projects.ObjectStorage_GrpcApi>("object-storage")
	.WithReference(objectDb)
	.WaitFor(objectDb);

var objectWww = builder
	.AddProject<Projects.ObjectStorage_WWW>("object-www")
	.WithReference(objectService)
	.WaitFor(objectService)
	.WithExternalHttpEndpoints();

// - Mark end: Object storage system  

builder.Build().Run();
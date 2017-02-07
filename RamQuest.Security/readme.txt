
Create Migrations:
-----------------
dotnet ef migrations add InitialIdentityDbMigration -c IdentityDbContext -o Data/Migrations/Identity/IdentityDb
dotnet ef migrations add InitialConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
dotnet ef migrations add InitialPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb

Update Database:
---------------
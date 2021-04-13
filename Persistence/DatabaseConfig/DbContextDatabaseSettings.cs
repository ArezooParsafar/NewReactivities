using Domain.Enums;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence.DatabaseConfig
{
    public static class DbContextDatabaseSettings
    {
        public static void DatabaseConfiguration(this DbContextOptionsBuilder dbContextOptionsBuilder, SiteSettings siteSettingsValue)
        {
            var connectionString = siteSettingsValue.GetActiveDatabaseConnectionString();
            switch (siteSettingsValue.ActiveDatabase)
            {
                case ActiveDatabase.SQLite:
                    dbContextOptionsBuilder.UseSqlite(connectionString, sqlServerOptionsBuilder =>
                    {
                        sqlServerOptionsBuilder.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);

                    });
                    break;
                case ActiveDatabase.SqlServer:
                    dbContextOptionsBuilder.UseSqlServer(
                        connectionString,
                        sqlServerOptionsBuilder =>
                        {
                            sqlServerOptionsBuilder.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
                            sqlServerOptionsBuilder.EnableRetryOnFailure();
                        });
                    break;

                default:
                    throw new NotSupportedException("Please set the ActiveDatabase in appsettings.json file to `LocalDb` or `SqlServer`.");

            }

        }


    }
}

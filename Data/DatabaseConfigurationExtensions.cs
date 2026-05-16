using Microsoft.EntityFrameworkCore;

namespace quizsergipe_api.Data;

public static class DatabaseConfigurationExtensions
{
    public static DbContextOptionsBuilder ConfigureQuizDatabase(
        this DbContextOptionsBuilder optionsBuilder,
        IConfiguration configuration)
    {
        var provider = configuration.GetValue<string>("Database:Provider") ?? "Sqlite";

        if (string.Equals(provider, "PostgreSql", StringComparison.OrdinalIgnoreCase))
        {
            var connectionString = configuration.GetConnectionString("QuizDbPostgreSql");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'QuizDbPostgreSql' nao configurada.");
            }

            optionsBuilder.UseNpgsql(connectionString);
            return optionsBuilder;
        }

        var sqliteConnectionString = configuration.GetConnectionString("QuizDbSqlite") ?? "Data Source=quizsergipe.db";
        optionsBuilder.UseSqlite(sqliteConnectionString);
        return optionsBuilder;
    }
}

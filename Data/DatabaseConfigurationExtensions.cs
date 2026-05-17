using Microsoft.EntityFrameworkCore;
using Npgsql;

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

            optionsBuilder.UseNpgsql(NormalizePostgreSqlConnectionString(connectionString));
            return optionsBuilder;
        }

        var sqliteConnectionString = configuration.GetConnectionString("QuizDbSqlite") ?? "Data Source=quizsergipe.db";
        optionsBuilder.UseSqlite(sqliteConnectionString);
        return optionsBuilder;
    }

    private static string NormalizePostgreSqlConnectionString(string connectionString)
    {
        if (!connectionString.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) &&
            !connectionString.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
        {
            return connectionString;
        }

        var uri = new Uri(connectionString);
        var userInfo = uri.UserInfo.Split(':', 2);

        if (userInfo.Length != 2)
        {
            throw new InvalidOperationException("URL do PostgreSQL invalida: credenciais ausentes.");
        }

        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port > 0 ? uri.Port : 5432,
            Database = uri.AbsolutePath.Trim('/'),
            Username = Uri.UnescapeDataString(userInfo[0]),
            Password = Uri.UnescapeDataString(userInfo[1])
        };

        return builder.ConnectionString;
    }
}

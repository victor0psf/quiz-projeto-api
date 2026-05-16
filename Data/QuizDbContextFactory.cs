using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace quizsergipe_api.Data;

public class QuizDbContextFactory : IDesignTimeDbContextFactory<QuizDbContext>
{
    public QuizDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<QuizDbContext>();
        optionsBuilder.ConfigureQuizDatabase(configuration);

        return new QuizDbContext(optionsBuilder.Options);
    }
}

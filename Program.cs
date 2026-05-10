using Microsoft.EntityFrameworkCore;
using quizsergipe_api.Data;
using quizsergipe_api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("QuizDb") ?? "Data Source=quizsergipe.db"));
builder.Services.AddScoped<IQuizService, QuizService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<QuizDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();

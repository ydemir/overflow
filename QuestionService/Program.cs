using Microsoft.EntityFrameworkCore;
using QuestionService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.AddServiceDefaults();
builder.Services.AddAuthentication()
    .AddKeycloakJwtBearer("keycloak", realm: "overflow", options =>
    {
        options.RequireHttpsMetadata = false;
        options.Audience = "overflow";
    });

builder.AddNpgsqlDbContext<QuestionDbContext>("questionDb");

var app = builder.Build();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.MapControllers();
app.MapDefaultEndpoints();


using var scope= app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context=services.GetRequiredService<QuestionDbContext>();
    await context.Database.MigrateAsync();
}
catch (Exception e)
{
    var logger =services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e,"An error occured during migration or seeding the database.");
}

app.Run();
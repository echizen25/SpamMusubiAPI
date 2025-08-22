using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Models;
using SpamMusubiAPI.Repositories;
using SpamMusubiAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Load config from appsettings + environment variables
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


// ✅ Register database connection (this works locally AND in Render)
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IAddOnsRepository, AddOnsRepository>();
builder.Services.AddScoped<IMopRepository, MopRepository>();
builder.Services.AddScoped<IExpensesRepository, ExpensesRepository>();

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SpamMusubiAPI", Version = "v1" });
});

var app = builder.Build();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

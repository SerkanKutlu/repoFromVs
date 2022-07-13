using IdentityManagementInfrastructure.Extensions;
using IdentityManagementInfrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseConfiguration(configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddIdentityServerConfig(configuration);
builder.Services.AddService<AppUser>();
var app = builder.Build();
SeedData.EnsureSeedData(builder.Services.BuildServiceProvider());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseIdentityServer();
app.MapControllers();

app.Run();
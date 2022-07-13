using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AuthenticationRegion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "https://localhost:7201";
        options.Audience = "api";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Read", policy =>
    {
        policy.RequireClaim("scope", "api.read");
    });

    options.AddPolicy("Upsert", policy =>
    {
        policy.RequireClaim("scope", "api.upsert");
    });

    options.AddPolicy("Delete", policy =>
    {
        policy.RequireClaim("scope", "api.delete");
    });
});
#endregion  
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
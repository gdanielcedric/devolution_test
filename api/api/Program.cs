using api.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.

builder.Services.AddControllers();

// Add authentication
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(j =>
    {
        j.RequireHttpsMetadata = false;
        j.Audience = builder.Configuration["Keycloack:Audience"];
        j.MetadataAddress = builder.Configuration["Keycloack:MetadataAddress"];
        j.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidIssuer = builder.Configuration["Keycloack:ValidIssuer"],
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
        };
    });

builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(builder.Configuration["ConnectionStrings:pgconstring"]));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api",
        Version = "V1",
        Description = "Collection Api relative au test technique de DEVOLUTION"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

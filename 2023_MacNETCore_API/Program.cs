using _2023_MacNETCore_API;
using _2023_MacNETCore_API.Interfaces;
using _2023_MacNETCore_API.Repositories;
using Microsoft.EntityFrameworkCore;
using Sane.Http.HttpResponseExceptions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using _2023_MacNETCore_API.Authentication;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();


// Add DI IoC services to the container.
builder.Services.AddScoped<IReadPattern_Repository, ReadPattern_Repository>();
builder.Services.AddScoped<IWritePattern_Repository, WritePattern_Repository>();
builder.Services.AddSingleton<IJwtAuthenticator, JwtAuthenticator>();


// Add EF & Sql Server to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"), providerOptions => providerOptions.EnableRetryOnFailure()));


// Add Implementation for JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpResponseExceptions();

app.UseAuthorization();

app.MapControllers();

// JWT Authentication
app.UseAuthentication();

app.Logger.LogInformation("Starting the API App at {DT}", DateTime.Now.ToLongTimeString());

app.Run();


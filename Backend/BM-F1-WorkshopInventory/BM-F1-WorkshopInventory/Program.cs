using BM_F1_WorkshopInventory.Data;
using BM_F1_WorkshopInventory.Interfaces;
using BM_F1_WorkshopInventory.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dBConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
var signingKey = Environment.GetEnvironmentVariable("TOKEN");
builder.Services.AddDbContext<BMF1DbContext>(options =>
    options.UseNpgsql(dBConnectionString));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {   
             options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true ,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime =true ,
            IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!)),
            ValidateIssuerSigningKey = true
        };
    });


builder.Services.AddScoped<IAuthService, AuthServiceImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

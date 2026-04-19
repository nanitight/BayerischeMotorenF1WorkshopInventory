using BM_F1_WorkshopInventory.Data;
using BM_F1_WorkshopInventory.Interfaces;
using BM_F1_WorkshopInventory.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", securityScheme:
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter the login token",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
      },
      new string[] {}
    }
  });

});

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("customPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddScoped<IAuthService, AuthServiceImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("customPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.Run();

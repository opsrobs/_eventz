using eventz.Accounts;
using eventz.Accounts.Repositorie;
using eventz.Controllers;
using eventz.Data;
using eventz.Mappings;
using eventz.Repositories;
using eventz.Repositories.Interfaces;
using eventz.SecurityServices;
using eventz.SecurityServices.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


var connectionStringMysql = builder.Configuration.GetConnectionString("myConnectionString");

builder.Services.AddDbContext<EventzDbContext>(options => options.UseMySql(
    connectionStringMysql,
    new MySqlServerVersion(new Version(8, 1, 0)),
    mysqlOptions => mysqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(10),
        errorNumbersToAdd: null
    )
));

builder.Services.AddScoped<IUserRepositorie, UserRepositorie>();
builder.Services.AddScoped<IPersonRepositorie, PersonRepositorie>();
builder.Services.AddScoped<IAuthenticate, Authenticate>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.
    TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["jwt:issuer"],
        ValidAudience = builder.Configuration["jwt:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["jwt:SecretKey"])),
        ClockSkew = TimeSpan.Zero

    };
});

builder.Services.AddAutoMapper(typeof(UserMapToDto));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

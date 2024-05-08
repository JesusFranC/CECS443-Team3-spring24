using Team3.ThePollProject.ConfigService;
using Team3.ThePollProject.DAO.DataAccessObjects;
using Team3.ThePollProject.DataAccess;
using Team3.ThePollProject.LoggingLibrary;
using Team3.ThePollProject.Middleware;
using Team3.ThePollProject.Model;
using Team3.ThePollProject.Models;
using Team3.ThePollProject.Security;
using Team3.ThePollProject.SecurityLibrary;
using Team3.ThePollProject.SecurityLibrary.Interfaces;
using Team3.ThePollProject.SecurityLibrary.Targets;
using Team3.ThePollProject.Services;
using Team3ThePollProject.Security;
using TeamSpecs.RideAlong.LoggingLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IConfigServiceJson, ConfigServiceJson>();
builder.Services.AddScoped<IGenericDAO, SqlServerDAO>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<IAuthTarget, SQLServerAuthTarget>();
builder.Services.AddScoped<ILogTarget, SqlDbLogTarget>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISecurityManager, SecurityManager>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// This is the first middleware, as we want it to exit as early as possible if we are handling a CORS Preflight
app.useCorsPreflight();

// Token validation is not necessary here, since if we are trying to log in, that means the user does not have tokens yet
//app.useIDValidator();



// This is the last middleware, as we want to make sure it is not going to be overwritten at any point
app.useCorsMiddleware();

app.MapControllers();

app.Run();

 using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.BusinessLogic.Services.Application;
using ServicesAPI.BusinessLogic.Services.Contact;
using ServicesAPI.BusinessLogic.Services.Header;
using ServicesAPI.BusinessLogic.Services.News;
using ServicesAPI.BusinessLogic.Services.Office;
using ServicesAPI.BusinessLogic.Services.Proejct;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Users;
using System;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Create NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //Data Base
    var connectionString = builder.Configuration.GetConnectionString("ConnectionMSSQLServer");
    builder.Services.AddDbContext<ContextDB>(options => options.UseSqlServer(connectionString));


    //Dependencies
    builder.Services.AddScoped<IApplicationClient, ApplicationClinet>();
    builder.Services.AddScoped<IApplicationAdmin, ApplicationAdmin>();
    builder.Services.AddScoped<IOffice, Office>();
    builder.Services.AddScoped<IProject, Project>();
    builder.Services.AddScoped<IHeader, Header>();
    builder.Services.AddScoped<IContact, Contact>();
    builder.Services.AddScoped<ITiding, Tiding>();

    //Identity
    //builder.Services.AddMediatR(typeof(LoginHandler).Assembly);
    builder.Services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<ContextDB>()
                    .AddDefaultTokenProviders();

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.SlidingExpiration = true;
    });

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
}
catch(Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}

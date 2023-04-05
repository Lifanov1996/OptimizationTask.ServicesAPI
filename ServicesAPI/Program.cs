using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using NLog;
using NLog.Web;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.BusinessLogic.Services.Application;
using ServicesAPI.BusinessLogic.Services.Contact;
using ServicesAPI.BusinessLogic.Services.Header;
using ServicesAPI.BusinessLogic.Services.Image;
using ServicesAPI.BusinessLogic.Services.News;
using ServicesAPI.BusinessLogic.Services.Office;
using ServicesAPI.BusinessLogic.Services.Proejct;
using ServicesAPI.BusinessLogic.Tokens;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Users;
using System;
using System.Data;
using System.Drawing;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    ConfigurationManager configuration = builder.Configuration;

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
    builder.Services.AddScoped<IToken, Token>();
    builder.Services.AddScoped<IImage, ServicesAPI.BusinessLogic.Services.Image.Image>();


    //Identity
    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ContextDB>()
                    .AddDefaultTokenProviders();

    builder.Services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = true;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidAudience = configuration["JWT:ValidAudience"],
                            ValidIssuer = configuration["JWT:ValidIssuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
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

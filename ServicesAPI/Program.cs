using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServicesAPI.BusinessLogic.Contracts;
using ServicesAPI.BusinessLogic.Services.Application;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Data Base
var connectionString = builder.Configuration.GetConnectionString("ConnectionMSSQLServer");
builder.Services.AddDbContext<ContextDB>(options => options.UseSqlServer(connectionString));


//Dependencies
builder.Services.AddScoped<IApplicationClient, ApplicationClinet>();
builder.Services.AddScoped<IApplicationAdmin, ApplicationAdmin>();

//Identity
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

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();

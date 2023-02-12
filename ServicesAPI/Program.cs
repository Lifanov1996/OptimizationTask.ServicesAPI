using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Data Base
var connectionString = builder.Configuration.GetConnectionString("ConnectionMSSQLServer");
builder.Services.AddDbContext<ContextDB>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

//Identity
builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ContextDB>()
                .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = true;
});

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

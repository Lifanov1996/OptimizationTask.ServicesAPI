using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServicesAPI.Models.Applications;
using ServicesAPI.Models.Contacts;
using ServicesAPI.Models.Headers;
using ServicesAPI.Models.News;
using ServicesAPI.Models.Projects;
using ServicesAPI.Models.Offices;
using ServicesAPI.Models.Users;

namespace ServicesAPI.Data.Entity
{
    public class ContextDB : IdentityDbContext<User>
    {
        public DbSet<Applications> Applications { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Headers> Headers { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Offices> Offices { get; set; }
        
        public ContextDB(DbContextOptions options) :base(options) { }
    }
}

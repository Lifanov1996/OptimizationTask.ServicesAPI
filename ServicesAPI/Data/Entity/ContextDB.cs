﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServicesAPI.Models.Applications;
using ServicesAPI.Models.Contacts;
using ServicesAPI.Models.Headers;
using ServicesAPI.Models.Tidings;
using ServicesAPI.Models.Projects;
using ServicesAPI.Models.Offices;
using ServicesAPI.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace ServicesAPI.Data.Entity
{
    public class ContextDB : IdentityDbContext<IdentityUser>
    {
        public DbSet<Applications> Applications { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Headers> Headers { get; set; }
        public DbSet<Tidings> Tidings { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Offices> Offices { get; set; }
        
        public ContextDB(DbContextOptions<ContextDB> options) :base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

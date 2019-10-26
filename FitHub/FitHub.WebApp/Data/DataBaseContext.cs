using FitHub.WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitHub.WebApp.Data
{
    public class DataBaseContext : IdentityDbContext<Customer,IdentityRole<Guid>, Guid>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<SportExercise> SportExercises { get; set; }
        public DbSet<Approach> Approaches { get; set; }

    }
}

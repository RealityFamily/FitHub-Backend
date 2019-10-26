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

        DbSet<Customer> Customers { get; set; }

    }
}

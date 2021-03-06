using Microsoft.EntityFrameworkCore;
using ParkBahceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkBahceAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<MilletBahcesi> MilletBahcesis { get; set; }
        public DbSet<Trail> Trails { get; set; }
        public DbSet<SosyalTesis> SosyalTesiss { get; set; }
        public DbSet <User> Users { get; set; }
    }
  
}

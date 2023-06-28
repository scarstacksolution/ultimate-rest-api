using System;
using _2023_MacNETCore_API.Models;
using Microsoft.EntityFrameworkCore;

namespace _2023_MacNETCore_API.Repositories
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<JwtClients> JwtClients { get; set; }
        public DbSet<Managers> Managers { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<NewEmployees> NewEmployees { get; set; }

    }
}
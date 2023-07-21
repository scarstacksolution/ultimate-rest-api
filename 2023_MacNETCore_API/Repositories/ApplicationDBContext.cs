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

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Managers> Managers { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Worklocation> Worklocation { get; set; }
        public DbSet<NewEmployees> NewEmployees { get; set; }
        public DbSet<JwtClients> JwtClients { get; set; }
        public DbSet<LoginModel> LoginModel { get; set; }
    }
}
﻿using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFModels
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataContext
{
    public class MenuDbContext : DbContext
    {
        public DbSet<FoodServedType> FoodServedTypes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMenuItem> OrderMenuItems { get; set; }

        public MenuDbContext() : base()
        {

        }

        private readonly string _connectionString = @"Data Source=MenuDb.db;";

        public MenuDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        public void SeedData()
        {
            Console.WriteLine("Clearing database if data exists...");
            bool dataCleared = false;
            if (MenuItems.Any())
            {
                this.MenuItems.RemoveRange(this.MenuItems);
                dataCleared = true;
            }

            if (this.FoodServedTypes.Any())
            {
                this.FoodServedTypes.RemoveRange(this.FoodServedTypes);
                dataCleared = true;
            }

            if (this.OrderMenuItems.Any())
            {
                this.OrderMenuItems.RemoveRange(this.OrderMenuItems);
                dataCleared = true;
            }

            if (this.Orders.Any())
            {
                this.Orders.RemoveRange(this.Orders);
                dataCleared = true;
            }

            if (dataCleared)
                this.SaveChanges();

            Console.WriteLine("Inserting FoodServedTypes...");
            this.FoodServedTypes.AddRange(new List<FoodServedType>() {
                    new FoodServedType {
                        Id = (int)FoodServedTypeEnum.Hot,
                        Name = "hot"
                    },
                    new FoodServedType
                    {
                        Id = (int)FoodServedTypeEnum.Cold,
                        Name = "cold"
                    }
                });
            this.SaveChanges();

            Console.WriteLine("Inserting menu items...");
            this.MenuItems.AddRange(new List<MenuItem>(){
                new MenuItem()
                {
                   Id = 1,
                   Name = "Cola",
                   FoodServedTypeId = (int)FoodServedTypeEnum.Cold,
                   IsAFoodItem = false,
                   Price = 0.50
                },
                new MenuItem{
                   Id = 2,
                   Name = "Coffee",
                   FoodServedTypeId = (int)FoodServedTypeEnum.Hot,
                   IsAFoodItem = false,
                   Price = 1.00
                },
                new MenuItem{
                   Id = 3,
                   Name = "Cheese Sandwich",
                   FoodServedTypeId = (int)FoodServedTypeEnum.Cold,
                   IsAFoodItem = true,
                   Price = 2.00
                },
                new MenuItem{
                   Id = 4,
                   Name = "Steak Sandwich",
                   FoodServedTypeId = (int)FoodServedTypeEnum.Hot,
                   IsAFoodItem = true,
                   Price = 4.50
                },
            });
            this.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodServedType>(x => x.ToTable("FoodServedTypes"));
            modelBuilder.Entity<MenuItem>(x => {
                x.ToTable("MenuItems");
                x.HasIndex(u => u.Name)
                 .HasDatabaseName("UC_MenuItem_Name")
                 .IsUnique();
            });
            modelBuilder.Entity<Order>(x => {
                x.ToTable("Orders");
                x.Property(x => x.DatePurchased).HasDefaultValue(DateTime.Now);
            });
            modelBuilder.Entity<OrderMenuItem>(x => {
                x.ToTable("OrderMenuItems");
                x.HasIndex(omi => new { omi.OrderId, omi.MenuItemId })
                 .HasDatabaseName("UC_OrderMenuItems_OrderId_MenuItemId")
                 .IsUnique();
            });
            
        }
    }
}

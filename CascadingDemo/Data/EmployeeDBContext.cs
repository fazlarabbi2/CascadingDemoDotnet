using CascadingDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CascadingDemo.Data
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Employee->Country relationship to restrict delete
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Country)
                .WithMany() // or .WithMany(c => c.Employees) if you have that navigation property
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Configure Employee->State relationship to restrict delete
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.State)
                .WithMany()
                .HasForeignKey(e => e.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Employee->City relationship to restrict delete
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.City)
                .WithMany()
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Countries: India, USA, UK
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryId = 1, CountryName = "India" },
                new Country { CountryId = 2, CountryName = "USA" },
                new Country { CountryId = 3, CountryName = "UK" }
            );

            // Seed States with more initial data
            modelBuilder.Entity<State>().HasData(
                // India
                new State { StateId = 1, StateName = "Maharashtra", CountryId = 1 },
                new State { StateId = 2, StateName = "Gujarat", CountryId = 1 },
                new State { StateId = 3, StateName = "Delhi", CountryId = 1 },
                new State { StateId = 4, StateName = "Karnataka", CountryId = 1 },
                // USA
                new State { StateId = 5, StateName = "California", CountryId = 2 },
                new State { StateId = 6, StateName = "Texas", CountryId = 2 },
                new State { StateId = 7, StateName = "New York", CountryId = 2 },
                // UK
                new State { StateId = 8, StateName = "England", CountryId = 3 },
                new State { StateId = 9, StateName = "Scotland", CountryId = 3 }
            );

            // Seed Cities with more initial data
            modelBuilder.Entity<City>().HasData(
                // Maharashtra
                new City { CityId = 1, CityName = "Mumbai", StateId = 1 },
                new City { CityId = 2, CityName = "Pune", StateId = 1 },
                // Gujarat
                new City { CityId = 3, CityName = "Ahmedabad", StateId = 2 },
                new City { CityId = 4, CityName = "Surat", StateId = 2 },
                // Delhi
                new City { CityId = 5, CityName = "New Delhi", StateId = 3 },
                // Karnataka
                new City { CityId = 6, CityName = "Bangalore", StateId = 4 },
                new City { CityId = 7, CityName = "Mysore", StateId = 4 },
                // California
                new City { CityId = 8, CityName = "Los Angeles", StateId = 5 },
                new City { CityId = 9, CityName = "San Francisco", StateId = 5 },
                // Texas
                new City { CityId = 10, CityName = "Houston", StateId = 6 },
                new City { CityId = 11, CityName = "Dallas", StateId = 6 },
                // New York
                new City { CityId = 12, CityName = "New York City", StateId = 7 },
                new City { CityId = 13, CityName = "Buffalo", StateId = 7 },
                // England
                new City { CityId = 14, CityName = "London", StateId = 8 },
                new City { CityId = 15, CityName = "Manchester", StateId = 8 },
                // Scotland
                new City { CityId = 16, CityName = "Edinburgh", StateId = 9 },
                new City { CityId = 17, CityName = "Glasgow", StateId = 9 }
            );
        }

        // DbSets for all models
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
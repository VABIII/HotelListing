using Microsoft.EntityFrameworkCore;

// To add seeded data to tables after they are created run the command: Add-Migration <MigrationName> 

namespace HotelListing.Data
{
    public class HotelListingDbContext : DbContext
    {
        // The options obj parameter will be coming from the db context func in the program.cs file
        public HotelListingDbContext(DbContextOptions options) : base(options)
        {

        }

        // After creating the classes that setup your db tables, add them below your dbContext function
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        //Override function involved with seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Sometimes it is neccessary to call the base class first before the override

            // Code that allows you to configure how you want data to go into the database

            // Allows you to load data into your tables and will accept and array of countries as an argument
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = 2,
                    Name = "Sicily",
                    ShortName = "SC"
                },
                new Country
                {
                    Id = 3,
                    Name = "Cayman Island",
                    ShortName = "CI"
                }
                );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Hyatt",
                    Address = "123 Anytown, USA",
                    Rating = 4.8,
                    CountryId = 1,
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Wyndam",
                    Address = "321 Anytown, USA",
                    Rating = 5,
                    CountryId = 2,
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Oliday Inn",
                    Address = "213 Anytown, USA",
                    Rating = 4,
                    CountryId = 3,
                }
                );
        }

    }
}



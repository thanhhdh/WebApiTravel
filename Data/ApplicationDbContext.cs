using Microsoft.EntityFrameworkCore;
using WebApiTravel.Models;

namespace WebApiTravel.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<TravelNumber> TravelNumbers { get; set; }

        //Seed Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Travel>().HasData(
                new Travel()
                {
                    Id = 1,
                    Name = "Royal Hotel",
                    Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                 new Travel()
                 {
                     Id = 2,
                     Name = "Diamond Hotel",
                     Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                     ImageUrl = "",
                     Occupancy = 5,
                     Rate = 300,
                     Sqft = 1100,
                     Amenity = "",
                     CreatedDate = DateTime.Now,
                 },
                  new Travel()
                  {
                      Id = 3,
                      Name = "Gold Hotel",
                      Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                      ImageUrl = "",
                      Occupancy = 5,
                      Rate = 300,
                      Sqft = 1050,
                      Amenity = "",
                      CreatedDate = DateTime.Now,
                  });
        }
    }
}

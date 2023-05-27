using System;
using System.Collections.Generic;
using System.Linq;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.EntityFrameworkCore;

namespace LittleBigTraveler.Models.DataBase
{
    public class BddContext : DbContext
    {
        // Tables Users
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }

        // Table Travels
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<AllInclusiveTravel> AllInclusiveTravels { get; set; }
        public DbSet<CustomMadeTravel> CustomMadeTravels { get; set; }
        public DbSet<SurpriseTravel> SurpriseTravels { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCatalog> ServiceCatalogs { get; set; }

        // Connexion avec la database MySql
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=Zachary3529<;database=LittleBigTravelDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destination>()
                .Property(d => d.Images)
                .HasConversion(
                    images => string.Join(";", images),
                    imagesString => imagesString.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
        }

        // Méthode d'initialisation (remplissage de donnée)
        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.Destinations.AddRange(
                new Destination
                {
                    Id = 1,
                    Country = "Canada",
                    City = "Brest",
                    Description = "Un voyage en Finistère",
                    Style = "Nature",
                    Images = new List<string>
                    {
                        "image1.jpg",
                        "image2.jpg",
                        "image3.jpg",
                        "image4.jpg"
                    },
                    ExternalLinks = "UnLien",
                },
                new Destination
                {
                    Id = 2,
                    Country = "France",
                    City = "Pau",
                    Description = "Un voyage dans le Béarn",
                    Style = "Decouverte",
                    Images = new List<string>
                    {
                        "image5.jpg",
                        "image6.jpg",
                        "image7.jpg",
                        "image8.jpg"
                    },
                    ExternalLinks = "UnLien",
                }
            );
            this.SaveChanges();
        }
    }
}

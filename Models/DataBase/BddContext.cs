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

        // Méthode permettant de convertir Images=List<string>  en une seule chaîne de caractères séparée par des points-virgules dans la database ; la conversion inverse est effectuée lorsque la donnée sera récupérée depuis la database.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destination>()
                .Property(d => d.Images)
                .HasConversion(
                    images => string.Join(";", images),
                    imagesString => imagesString.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            modelBuilder.Entity<Service>()
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
                        "/ImagesTest/Brest1.jpg",
                        "/ImagesTest/Brest2.jpg",
                        "/ImagesTest/Brest3.jpg",
                        "/ImagesTest/Brest4.jpg"
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
                        "/ImagesTest/Pau1.jpg",
                        "/ImagesTest/Pau2.jpg",
                        "/ImagesTest/Pau3.jpg",
                        "/ImagesTest/Pau4.jpg"
                    },
                    ExternalLinks = "UnLien",
                }
            );
            this.Services.AddRange(
                new Service
                {
                    Id = 1,
                    Name = "Service de transport à Brest",
                    Price = 20.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Brest",
                    Type = "Transport",
                    MaxCapacity = 100,
                    Images = new List<string>
                        {
                            "/ImagesTest/BrestBateau.jpg",
                        },
                    ExternalLinks = "UnLien"
                },
                    new Service
                    {
                        Id = 2,
                        Name = "Activité à Brest",
                        Price = 30.0,
                        Schedule = DateTime.Now.AddDays(2),
                        Location = "Brest",
                        Type = "Activité",
                        MaxCapacity = 50,
                        Images = new List<string>
                        {
                            "/ImagesTest/BrestVoile.jpg",

                        },
                        ExternalLinks = "UnLien"
                    },
                    new Service
                    {
                        Id = 3,
                        Name = "Restaurant à Brest",
                        Price = 40.0,
                        Schedule = DateTime.Now.AddDays(3),
                        Location = "Brest",
                        Type = "Restaurant",
                        MaxCapacity = 30,
                        Images = new List<string>
                        {
                            "/ImagesTest/RestaurantBrest.jpg",

                        },
                        ExternalLinks = "UnLien"
                    },
                    // Services pour Pau
                    new Service
                    {
                        Id = 4,
                        Name = "Service de transport à Pau",
                        Price = 25.0,
                        Schedule = DateTime.Now.AddDays(1),
                        Location = "Pau",
                        Type = "Transport",
                        MaxCapacity = 80,
                        Images = new List<string>
                        {
                            "/ImagesTest/PauTrain.jpg",
                        },
                        ExternalLinks = "UnLien"
                    },
                    new Service
                    {
                        Id = 5,
                        Name = "Activité à Pau",
                        Price = 35.0,
                        Schedule = DateTime.Now.AddDays(2),
                        Location = "Pau",
                        Type = "Activité",
                        MaxCapacity = 60,
                        Images = new List<string>
                        {
                            "/ImagesTest/PauRando.jpg",
                        },
                        ExternalLinks = "UnLien"
                    },
                    new Service
                    {
                        Id = 6,
                        Name = "Restaurant à Pau",
                        Price = 45.0,
                        Schedule = DateTime.Now.AddDays(3),
                        Location = "Pau",
                        Type = "Restaurant",
                        MaxCapacity = 40,
                        Images = new List<string>
                        {
                            "/ImagesTest/PauRestaurant.jpg",

                        },
                        ExternalLinks = "UnLien"
                    }
                );

            this.SaveChanges();
        }
    }
}
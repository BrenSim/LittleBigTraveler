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

        public DbSet<User> GetUsers()
        {
            return Users;
        }

        // Méthode d'initialisation (remplissage de donnée)
        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            // Création des destinations
            this.Destinations.AddRange(
                new Destination
                {
                    Id = 1,
                    Country = "Canada",
                    City = "Brest",
                    Description = "Un voyage en Finistère",
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

            // Création des services
            this.Services.AddRange(
                new Service
                {
                    Id = 1,
                    Name = "Service de transport à Brest",
                    Price = 20.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Brest",
                    Type = "Transport",
                    Style = null,
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
                        Style = "Sport",
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
                        Style = null,
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
                        Name = "Service de transport ",
                        Price = 25.0,
                        Schedule = DateTime.Now.AddDays(1),
                        Location = "",
                        Type = "Transport",
                        Style = null,
                        MaxCapacity = 80,
                        Images = new List<string>
                        {
                            "/ImagesTest/Train.jpg",
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
                        Style = "Culture",
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
                        Style = null,
                        MaxCapacity = 40,
                        Images = new List<string>
                        {
                            "/ImagesTest/PauRestaurant.jpg",

                        },
                        ExternalLinks = "UnLien"
                    }
                );

      
            // Création des rôles
            this.Roles.AddRange(
    
                    new Role { Id = 1, Name = "Air France", Type = "Transport" },

                    new Role { Id = 2, Name = "Chez Toto Pizza", Type = "Restaurant" }
            // Autre role ci-après
                );

            // Création des users
            //User users = new List<User> {
            this.Users.AddRange(
                new User
                {
                    Id = 1,
                    LastName = "Dupont",
                    FirstName = "Alice",
                    Email = "alice@example.com",
                    Password = UserDAL.EncodeMD5("password123"),
                    Address = "123 Rue des Fleurs",
                    PhoneNumber = "1234567890",
                    BirthDate = new DateTime(1990, 1, 1),
                    ProfilePicture = "/path/to/profile_picture.jpg"
                },
                new User
                {
                    Id = 2,
                    LastName = "Martin",
                    FirstName = "Bob",
                    Email = "bob@example.com",
                    Password = "password456",
                    Address = "456 Rue des Arbres",
                    PhoneNumber = "9876543210",
                    BirthDate = new DateTime(1995, 5, 5),
                    ProfilePicture = "/path/to/profile_picture.jpg"
                },
                new User
                {
                    Id = 3,
                    LastName = "Dubois",
                    FirstName = "Charlie",
                    Email = "charlie@example.com",
                    Password = "password789",
                    Address = "789 Rue des Montagnes",
                    PhoneNumber = "5678901234",
                    BirthDate = new DateTime(1985, 10, 10),
                    ProfilePicture = "/path/to/profile_picture.jpg"
                },
                new User
                {
                    Id = 4,
                    LastName = "Leclerc",
                    FirstName = "David",
                    Email = "david@example.com",
                    Password = "passwordabc",
                    Address = "321 Rue des Champs",
                    PhoneNumber = "0123456789",
                    BirthDate = new DateTime(1980, 3, 15),
                    ProfilePicture = "/path/to/profile_picture.jpg"
                },
                new User
                {
                    Id = 5,
                    LastName = "Lefebvre",
                    FirstName = "Emma",
                    Email = "emma@example.com",
                    Password = "passworddef",
                    Address = "654 Rue des Rivières",
                    PhoneNumber = "6789012345",
                    BirthDate = new DateTime(1992, 7, 20),
                    ProfilePicture = "/path/to/profile_picture.jpg"
                },
                new User
                {
                    Id = 6,
                    LastName = "Fournier",
                    FirstName = "François",
                    Email = "francois@example.com",
                    Password = "passwordghi",
                    Address = "987 Rue des Collines",
                    PhoneNumber = "3456789012",
                    BirthDate = new DateTime(1988, 12, 25),
                    ProfilePicture = "/path/to/profile_picture.jpg"
                }
            );



            // Création des Customers
            this.Customers.AddRange(
                 new Customer
                 {
                     Id = 5,
                     User = Users.Find(1),
                     LoyaltyPoint = 100,
                     CommentPoint = 50
                 },
                new Customer
                {
                    Id = 6,
                    User = Users.Find(2),
                    LoyaltyPoint = 200,
                    CommentPoint = 75
                }
            );

             this.Administrators.AddRange(
                 new Administrator
                 {
                     Id = 3,
                     User = Users.Find(3),

                 },
                new Administrator
                {
                    Id = 4,
                    User = Users.Find(4),
                }
            );

            this.SaveChanges();
        }


    }
}
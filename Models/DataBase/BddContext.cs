using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

using System.Diagnostics;
using System.IO;

using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Http;

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
        public DbSet<Package> Packages { get; set; }
        //public DbSet<AllInclusiveTravel> AllInclusiveTravels { get; set; }
        //public DbSet<CustomMadeTravel> CustomMadeTravels { get; set; }
        //public DbSet<SurpriseTravel> SurpriseTravels { get; set; }
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
            // Méthode permettant de convertir Images=List<string>  en une seule chaîne de caractères séparée par des points-virgules dans la database ; la conversion inverse est effectuée lorsque la donnée sera récupérée depuis la database.
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
            //modelBuilder.Entity<Service>()
            //     .HasOne(s => s.Package)
            //     .WithMany(a => a.ServiceForPackage)
            //     .HasForeignKey(s => s.Package);

            //modelBuilder.Entity<Package>()
            //    .HasMany(a => a.ServiceForPackage) 
            //    .WithOne() 
            //    .OnDelete(DeleteBehavior.SetNull); 

        }

        public DbSet<User> GetUsers()
        {
            return Users;
        }


        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            // Création des destinations
            this.Destinations.AddRange(
            new Destination
            {
                Id = 1,
                Country = "France",
                City = "Paris",
                Description = "Capitale de la culture avec musées, monuments emblématiques et arts.",
                Images = new List<string>
                {

                    "/ImagesDestinations/Paris1.jpg",
                    "/ImagesDestinations/Paris2.jpg",
                    "/ImagesDestinations/Paris3.jpg",
                    "/ImagesDestinations/Paris4.jpg"
                },
                ExternalLinks = "https://www.parisinfo.com/",
            }


            );



            // Services à Paris 
            var serviceParis1 = new Service
            {
                Id = 1,
                Name = "Hôtel Prince Albert Louvre",
                Price = 120.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Accommodation",
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris1Services.jpg",
                },
                ExternalLinks = "https://www.hotelprincealbert.com/louvre",
                DestinationId = 1
            };

            var serviceParis2 = new Service
            {
                Id = 2,
                Name = "Le Village Hostel Montmartre",
                Price = 35.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Accommodation",
                MaxCapacity = 1,
                Images = new List<string>
                {
                    "/ImagesTest/Paris2Services.jpg",
                },
                ExternalLinks = "https://www.villagehostel.fr/montmartre",
                DestinationId = 1
            };

            var serviceParis3 = new Service
            {
                Id = 3,
                Name = "La Villa Paris",
                Price = 180.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Accommodation",
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris3Services.jpg",
                },
                ExternalLinks = "https://www.lavillaparis.com",
                DestinationId = 1
            };

            var serviceParis4 = new Service
            {
                Id = 4,
                Name = "Hôtel de la Herse d'Or",
                Price = 60.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Accommodation",
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris4Services.jpg",
                },
                ExternalLinks = "https://www.herse-dor.com",
                DestinationId = 1
            };

            // Ajoutez les services à la base de données
            this.Services.AddRange(
                serviceParis1,
                serviceParis2,
                serviceParis3,
                serviceParis4
            );




            // Création des rôles
            this.Roles.AddRange(

                    new Role { Id = 1, Name = "Air France", Type = "Transport" },

                    new Role { Id = 2, Name = "Chez Toto Pizza", Type = "Restaurant" },

                    new Role { Id = 3, Name = "Autres", Type = "Activité" }
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
                Password = UserDAL.EncodeMD5("password"),
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
                Password = UserDAL.EncodeMD5("password"),
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
                Password = UserDAL.EncodeMD5("password"),
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
                Password = UserDAL.EncodeMD5("password"),
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
                Password = UserDAL.EncodeMD5("password"),
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
                     Id = 1,
                     User = Users.Find(6),
                     LoyaltyPoint = 100,
                     CommentPoint = 50
                 }

            );

            this.Administrators.AddRange(
                new Administrator
                {
                    Id = 1,
                    User = Users.Find(1),
                },
               new Administrator
               {
                   Id = 2,
                   User = Users.Find(2),
               },
               new Administrator
               {
                   Id = 3,
                   User = Users.Find(3),
               },
               new Administrator
               {
                   Id = 4,
                   User = Users.Find(4),
               },
               new Administrator
               {
                   Id = 5,
                   User = Users.Find(5),
               }

            );
            //this.Partners.AddRange(
            //    new Partner
            //    {
            //        Id = 1,
            //        User = Users.Find(12),
            //        RoleId = 1,
                   
                    
            //    },
            //    new Partner
            //    {
            //        Id = 2,
            //        User = Users.Find(13),
            //        RoleId = 2,
            //    },
            //    new Partner
            //    {
            //        Id = 3,
            //        User = Users.Find(14),
            //        RoleId = 3,
            //    }

            //    );

            this.SaveChanges();
        }


    }
}
using System;
using System.Diagnostics;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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

        //Connexion avec la database MySql
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=Maia*Pereira2403;database=LittleBigTravelDB");
        }

        //Méthode d'initialisation (remplissage de donnée)
        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.Destinations.AddRange(
                new Destination
                {
                    Id = 1,
                    Country = "France",
                    City = "Paris",
                    Description = "Capitale de la culture avec musées, monuments emblématiques et arts.",
                    Style = "Cultural",
                    Images = "UneImage",
                    ExternalLinks = "https://www.parisinfo.com/",
                },
                new Destination
                {
                    Id = 2,
                    Country = "Italie",
                    City = "Rome",
                    Description = "Ville éternelle avec vestiges antiques, art religieux et gastronomie.",
                    Style = "Cultural",
                    Images = "UneImage",
                    ExternalLinks = "https://www.turismoroma.it/",
                },
                new Destination
                {
                    Id = 3,
                    Country = "République tchèque",
                    City = "Prague",
                    Description = "Ville médiévale avec châteaux, ponts, musique classique et atmosphère romantique.\"Style: Sportive",
                    Style = "",
                    Images = "UneImage",
                    ExternalLinks = "https://www.prague.eu/",
                },
                new Destination
                {
                    Id = 4,
                    Country = "Italie",
                    City = "Venise",
                    Description = "La ville romantique des canaux, riche en histoire et en arts.",
                    Style = "Culture",
                    Images = "UneImage",
                    ExternalLinks = "https://www.veneziaunica.it/",
                },
                new Destination
                {
                    Id = 5,
                    Country = "Germany",
                    City = "Munich",
                    Description = "La ville de l’Oktoberfest et ses traditions.",
                    Style = "Culture",
                    Images = "UneImage",
                    ExternalLinks = "https://www.muenchen.de/",
                },
                new Destination
                {
                    Id = 6,
                    Country = "Royaume-Uni",
                    City = "Londres",
                    Description = "Métropole cosmopolite avec musées de renommée mondiale et scène théâtrale vibrante.",
                    Style = "Culture",
                    Images = "UneImage",
                    ExternalLinks = "https://www.visitlondon.com/",
                },
                new Destination
                {
                    Id = 7,
                    Country = "Grèce",
                    City = "Athènes",
                    Description = "Berceau de la civilisation occidentale, avec l'Acropole et les sites historiques.",
                    Style = "Culture",
                    Images = "UneImage",
                    ExternalLinks = "https://www.thisisathens.org/",
                }
            );
            this.SaveChanges();
        }
    }
}


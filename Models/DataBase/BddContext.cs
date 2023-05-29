using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

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

        //Méthode d'initialisation (remplissage de donnée)
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
                    Style = "Cultural",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Paris1.jpg",
                        "/ImagesDestinations/Paris2.jpg",
                        "/ImagesDestinations/Paris3.jpg",
                        "/ImagesDestinations/Paris4.jpg"
                    },
                    ExternalLinks = "https://www.parisinfo.com/",
                },
                new Destination
                {
                    Id = 2,
                    Country = "Italie",
                    City = "Rome",
                    Description = "Ville éternelle avec vestiges antiques, art religieux et gastronomie.",
                    Style = "Cultural",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Rome1.jpg",
                        "/ImagesDestinations/Rome2.jpg",
                        "/ImagesDestinations/Rome3.jpg",
                        "/ImagesDestinations/Rome4.jpg"
                    },
                    ExternalLinks = "https://www.turismoroma.it/",
                },
                new Destination
                {
                    Id = 3,
                    Country = "République tchèque",
                    City = "Prague",
                    Description = "Ville médiévale avec châteaux, ponts, musique classique et atmosphère romantique.\"Style: Sportive",
                    Style = "",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Prague1.jpg",
                        "/ImagesDestinations/Prague2.jpg",
                        "/ImagesDestinations/Prague3.jpg",
                        "/ImagesDestinations/Prague4.jpg"
                    },
                    ExternalLinks = "https://www.prague.eu/",
                },
                new Destination
                {
                    Id = 4,
                    Country = "Italie",
                    City = "Venise",
                    Description = "La ville romantique des canaux, riche en histoire et en arts.",
                    Style = "Culture",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Venise1.jpg",
                        "/ImagesDestinations/Venise2.jpg",
                        "/ImagesDestinations/Venise3.jpg",
                        "/ImagesDestinations/Venise4.jpg"
                    },
                    ExternalLinks = "https://www.veneziaunica.it/",
                },
                new Destination
                {
                    Id = 5,
                    Country = "Germany",
                    City = "Munich",
                    Description = "La ville de l’Oktoberfest et ses traditions.",
                    Style = "Culture",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Munich1.jpg",
                        "/ImagesDestinations/Munich2.jpg",
                        "/ImagesDestinations/Munich3.jpg",
                        "/ImagesDestinations/Munich4.jpg"
                    },
                    ExternalLinks = "https://www.muenchen.de/",
                },
                new Destination
                {
                    Id = 6,
                    Country = "Royaume-Uni",
                    City = "Londres",
                    Description = "Métropole cosmopolite avec musées de renommée mondiale et scène théâtrale vibrante.",
                    Style = "Culture",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Londres1.jpg",
                        "/ImagesDestinations/Londres2.jpg",
                        "/ImagesDestinations/Londres3.jpg",
                        "/ImagesDestinations/Londres4.jpg"
                    },
                    ExternalLinks = "https://www.visitlondon.com/",
                },
                new Destination
                {
                    Id = 7,
                    Country = "Grèce",
                    City = "Athènes",
                    Description = "Berceau de la civilisation occidentale, avec l'Acropole et les sites historiques.",
                    Style = "Culture",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Athenes1.jpg",
                        "/ImagesDestinations/Athenes2.jpg",
                        "/ImagesDestinations/Athenes3.jpg",
                        "/ImagesDestinations/Athenes4.jpg"
                    },
                    ExternalLinks = "https://www.thisisathens.org/",
                },
                new Destination
                {
                    Id = 8,
                    Country = "Espagne",
                    City = "Barcelone",
                    Description = "Destination sportive avec football, basketball et sports nautiques.",
                    Style = "Sportive",
                    Images = new List<string>
                    {
                         "/ImagesDestinations/Barcelona1.jpg",
                         "/ImagesDestinations/Barcelona2.jpg",
                         "/ImagesDestinations/Barcelona3.jpg",
                         "/ImagesDestinations/Barcelona4.jpg"
                    },
                    ExternalLinks = "https://www.barcelona.cat/",
                },
                new Destination
                {
                    Id = 9,
                    Country = "Autriche",
                    City = "Innsbruck",
                    Description = "Ville alpine pour le ski, le snowboard et les sports d'hiver.",
                    Style = "Sportive",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Innsbruck1.jpg",
                        "/ImagesDestinations/Innsbruck2.jpg",
                        "/ImagesDestinations/Innsbruck3.jpg",
                        "/ImagesDestinations/Innsbruck4.jpg"
                    },
                    ExternalLinks = "https://www.innsbruck.info/",
                },
                new Destination
                {
                    Id = 10,
                    Country = "France",
                    City = "Annecy",
                    Description = "Paradis des sports nautiques entouré de magnifiques montagnes.",
                    Style = "Sportive",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Annecy1.jpg",
                        "/ImagesDestinations/Annecy2.jpg",
                        "/ImagesDestinations/Annecy3.jpg",
                        "/ImagesDestinations/Annecy4.jpg"
                    },
                    ExternalLinks = "https://www.lac-annecy.com/",
                },
                new Destination
                {
                    Id = 11,
                    Country = "Allemagne",
                    City = "Garmisch-Partenkirchen",
                    Description = "Destination alpine pour le ski, l'escalade et les sports de montagne.",
                    Style = "Sportive",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Garmisch-Partenkirchen1.jpg",
                        "/ImagesDestinations/Garmisch-Partenkirchen2.jpg",
                        "/ImagesDestinations/Garmisch-Partenkirchen3.jpg",
                        "/ImagesDestinations/Garmisch-Partenkirchen4.jpg"
                    },
                    ExternalLinks = "https://www.gapa.de/",
                },
                new Destination
                {
                    Id = 12,
                    Country = "Portugal",
                    City = "Lisbonne",
                    Description = "Ville dynamique avec des possibilités de surf et de sports nautiques.",
                    Style = "Sportive",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Lisbonne1.jpg",
                        "/ImagesDestinations/Lisbonne2.jpg",
                        "/ImagesDestinations/Lisbonne3.jpg",
                        "/ImagesDestinations/Lisbonne4.jpg"
                    },
                    ExternalLinks = "https://www.visitportugal.com/",
                },
                new Destination
                {
                    Id = 13,
                    Country = "Suisse",
                    City = "Interlaken",
                    Description = "Les majestueuses Alpes suisses entourent Interlaken pour des paysages spectaculaires.",
                    Style = "Nature",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Interlaken1.jpg",
                        "/ImagesDestinations/Interlaken2.jpg",
                        "/ImagesDestinations/Interlaken3.jpg",
                        "/ImagesDestinations/Interlaken4.jpg"
                    },
                    ExternalLinks = "https://www.interlaken.ch/",
                },
                new Destination
                {
                    Id = 14,
                    Country = "France",
                    City = "Chamonix",
                    Description = "Paradis alpin au pied du Mont Blanc pour les amoureux de montagne.",
                    Style = "Nature",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Chamonix1.jpg",
                        "/ImagesDestinations/Chamonix2.jpg",
                        "/ImagesDestinations/Chamonix3.jpg",
                        "/ImagesDestinations/Chamonix4.jpg"
                    },
                    ExternalLinks = "https://www.chamonix.com/",
                },
                new Destination
                {
                    Id = 15,
                    Country = "Croatie",
                    City = "Plitvice Lakes",
                    Description = "Cascades, eaux turquoise et végétation luxuriante aux lacs de Plitvice.",
                    Style = "Nature",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/PlitviceLakes1.jpg",
                        "/ImagesDestinations/PlitviceLakes2.jpg",
                        "/ImagesDestinations/PlitviceLakes3.jpg",
                        "/ImagesDestinations/PlitviceLakes4.jpg"
                    },
                    ExternalLinks = "https://www.np-plitvicka-jezera.hr/",
                },
                new Destination
                {
                    Id = 16,
                    Country = "Norvège",
                    City = "Tromsø",
                    Description = "Arctique norvégien : aurores boréales et nature sauvage exceptionnelle.",
                    Style = "Nature",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Tromso1.jpg",
                        "/ImagesDestinations/Tromso2.jpg",
                        "/ImagesDestinations/Tromso3.jpg",
                        "/ImagesDestinations/Tromso4.jpg"
                    },
                    ExternalLinks = "https://www.visittromso.no/",
                },
                new Destination
                {
                    Id = 17,
                    Country = "Irlande",
                    City = "Killarney",
                    Description = "Lacs, montagnes et paysages préservés au cœur de Killarney.",
                    Style = "Nature",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Killarney1.jpg",
                        "/ImagesDestinations/Killarney2.jpg",
                        "/ImagesDestinations/Killarney3.jpg",
                        "/ImagesDestinations/Killarney4.jpg"
                    },
                    ExternalLinks = "https://www.killarney.ie/",
                },
                new Destination
                {
                    Id = 18,
                    Country = "Hongrie",
                    City = "Budapest",
                    Description = "La ville des bains thermaux et des spas historiques.",
                    Style = "Relax",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Budapest1.jpg",
                        "/ImagesDestinations/Budapest2.jpg",
                        "/ImagesDestinations/Budapest3.jpg",
                        "/ImagesDestinations/Budapest4.jpg"
                    },
                    ExternalLinks = "https://www.budapestinfo.hu/",
                },
                new Destination
                {
                    Id = 19,
                    Country = "Royaume-Uni",
                    City = "Bath",
                    Description = "Ville célèbre pour ses thermes romains et son architecture géorgienne.",
                    Style = "Relax",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Bath1.jpg",
                        "/ImagesDestinations/Bath2.jpg",
                        "/ImagesDestinations/Bath3.jpg",
                        "/ImagesDestinations/Bath4.jpg"
                    },
                    ExternalLinks = "https://visitbath.co.uk/",
                },
                new Destination
                {
                    Id = 20,
                    Country = "République tchèque",
                    City = "Karlovy Vary",
                    Description = "Spas historiques et eaux thermales dans une ville pittoresque.",
                    Style = "Relax",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/KarlovyVary1.jpg",
                        "/ImagesDestinations/KarlovyVary2.jpg",
                        "/ImagesDestinations/KarlovyVary3.jpg",
                        "/ImagesDestinations/KarlovyVary4.jpg"
                    },
                    ExternalLinks = "https://www.karlovyvary.cz/en",
                },
                new Destination
                {
                    Id = 21,
                    Country = "Italie",
                    City = "Ischia",
                    Description = "Une île thermale dans le golfe de Naples avec des eaux curatives.",
                    Style = "Relax",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Ischia1.jpg",
                        "/ImagesDestinations/Ischia2.jpg",
                        "/ImagesDestinations/Ischia3.jpg",
                        "/ImagesDestinations/Ischia4.jpg"
                    },
                    ExternalLinks = "https://www.ischiareview.com/",
                },
                new Destination
                {
                    Id = 22,
                    Country = "France",
                    City = "Vichy",
                    Description = "Station thermale élégante avec des spas et une architecture Art Nouveau.",
                    Style = "Relax",
                    Images = new List<string>
                    {
                        "/ImagesDestinations/Vichy1.jpg",
                        "/ImagesDestinations/Vichy2.jpg",
                        "/ImagesDestinations/Vichy3.jpg",
                        "/ImagesDestinations/Vichy4.jpg"
                    },
                    ExternalLinks = "https://www.vichy-destinations.fr/",
                }
            );
            this.SaveChanges();
        }
    }
}


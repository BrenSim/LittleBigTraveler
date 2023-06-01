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

            // Création des services
            this.Services.AddRange(

            //Modèle à suivre
                new Service
                {
                    Id = 1,
                    Name = "Service de transport à Brest",
                    Price = 20.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Brest",
                    Type = "Transport",
                    Style = "Cultural",
                    MaxCapacity = 100,
                    Images = new List<string>
                        {
                            "/ImagesTest/BrestBateau.jpg",
                        },
                    ExternalLinks = "UnLien"
                },
            


                // Services à Paris 
                new Service
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
                    ExternalLinks = "https://www.hotelprincealbert.com/louvre"
                },

            new Service
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
                ExternalLinks = "https://www.villagehostel.fr/montmartre"
            },

            new Service
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
                ExternalLinks = "https://www.lavillaparis.com"
            },

            new Service
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
                ExternalLinks = "https://www.herse-dor.com"
            },

            new Service
            {
                Id = 5,
                Name = "Le Bristol Paris",
                Price = 200.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Accommodation",
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris5Services.jpg",
                },
                ExternalLinks = "https://www.lebristolparis.com"
            },

            // Restaurants
            new Service
            {
                Id = 6,
                Name = "Le Petit Bistro",
                Price = 30.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Accommodation",
                MaxCapacity = 4,
                Images = new List<string>
                {
                    "/ImagesTest/Paris6Services.jpg",
                },
                ExternalLinks = "https://www.lepetitbistro.fr"
            },

            new Service
            {
                Id = 7,
                Name = "Green Garden",
                Price = 25.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Accommodation",
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris7Services.jpg",
                },
                ExternalLinks = "https://www.greengardenrestaurant.fr"
            },

            new Service
            {
                Id = 8,
                Name = "La Brasserie Parisienne",
                Price = 60.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Accommodation",
                MaxCapacity = 6,
                Images = new List<string>
                {
                    "/ImagesTest/Paris8Services.jpg",
                },
                ExternalLinks = "https://www.labrasserieparisienne.com"
            },

            new Service
            {
                Id = 9,
                Name = "Le Bistrot Gourmand",
                Price = 80.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Accommodation",
                MaxCapacity = 4,
                Images = new List<string>
                {
                    "/ImagesTest/Paris9Services.jpg",
                },
                ExternalLinks = "https://www.lebistrotgourmand.com"
            },

            new Service
            {
                Id = 10,
                Name = "Le Ciel de Paris",
                Price = 100.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Accommodation",
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris10Services.jpg",
                },
                ExternalLinks = "https://www.lecieldeparis.com"
            },

            new Service
            {
                Id = 11,
                Name = "Paris Private Guides",
                Price = 80.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Paris",
                Type = "Guide",
                MaxCapacity = 1,
                Images = new List<string>
                {
                    "/ImagesTest/Paris11Services.jpg",
                },
                ExternalLinks = "https://www.parisprivateguides.com"
            },

            new Service
            {
                Id = 12,
                Name = "Historical Paris Tours",
                Price = 90.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Paris",
                Type = "Guide",
                MaxCapacity = 1,
                Images = new List<string>
                {
                    "/ImagesTest/Paris12Services.jpg",
                },
                ExternalLinks = "https://www.historicalparistours.com"
            },

            new Service
            {
                Id = 13,
                Name = "Location de voiture",
                Price = 50.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Transport",
                MaxCapacity = 4,
                Images = new List<string>
                {
                    "/ImagesTest/Paris13Services.jpg",
                },
                ExternalLinks = "https://www.location-voiture-paris.com"
            },

            new Service
            {
                Id = 14,
                Name = "TAXI G7",
                Price = 15.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Transport",
                MaxCapacity = 4,
                Images = new List<string>
                {
                    "/ImagesTest/Paris14Services.jpg",
                },
                ExternalLinks = "https://www.g7.fr"
            },

            new Service
            {
                Id = 15,
                Name = "Batobus",
                Price = 17.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Transport",
                MaxCapacity = 150,
                Images = new List<string>
                {
                    "/ImagesTest/Paris15Services.jpg",
                },
                ExternalLinks = "https://www.batobus.com"
            },






    // Services à Rome
    new Service
                {
                    Id = 16,
                    Name = "Hostel Alessandro Palace & Bar",
                    Price = 20.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Accommodation",
                    Style = null,
                    MaxCapacity = 2,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome16Hotel.jpeg",
                        },
                    ExternalLinks = "https://www.hostelrome.com"
                },
                new Service
                {
                    Id = 17,
                    Name = "The Yellow Hostel",
                    Price = 30.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Accommodation",
                    Style = null,
                    MaxCapacity = 4,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome17Hotel.jpeg",
                        },
                    ExternalLinks = "https://www.the-yellow.com"
                },
                new Service
                {
                    Id = 18,
                    Name = "Hotel de Russie",
                    Price = 350.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Accommodation",
                    Style = null,
                    MaxCapacity = 2,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome18Hotel.jpeg",
                        },
                    ExternalLinks = "https://www.hotelderussie.it"
                },
                new Service
                {
                    Id = 19,
                    Name = "Hotel Artemide",
                    Price = 150.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Accommodation",
                    Style = null,
                    MaxCapacity = 3,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome19Hotel.jpeg",
                        },
                    ExternalLinks = "https://www.hotelartemide.it"
                },
                new Service
                {
                    Id = 20,
                    Name = "Hotel Raphael",
                    Price = 300.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Accommodation",
                    Style = null,
                    MaxCapacity = 4,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome20Hotel.jpeg",
                        },
                    ExternalLinks = "https://www.raphaelhotel.com"
                },
                new Service
                {
                    Id = 21,
                    Name = "Trattoria Da Danilo",
                    Price = 40.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 50,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome21Restaurant.jpeg",
                        },
                    ExternalLinks = "https://www.trattoriadadanilo.it"
                },
                new Service
                {
                    Id = 22,
                    Name = "Pizzarium",
                    Price = 15.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 30,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome22Restaurant.jpeg",
                        },
                    ExternalLinks = "https://www.bonci.it"
                },
                new Service
                {
                    Id = 23,
                    Name = "La Pergola",
                    Price = 150.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 20,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome23Restaurant.jpeg",
                        },
                    ExternalLinks = "https://www.romecavalieri.com/lapergola"
                },
                new Service
                {
                    Id = 24,
                    Name = "Armando al Pantheon",
                    Price = 80.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 40,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome24Restaurant.jpeg",
                        },
                    ExternalLinks = "https://www.armandoalpantheon.it"
                },
                new Service
                {
                    Id = 25,
                    Name = "Antico Arco",
                    Price = 70.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 35,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome25Restaurant.jpeg",
                        },
                    ExternalLinks = "https://www.anticoarco.it"
                },
                new Service
                {
                    Id = 26,
                    Name = "Hertz Car Rental",
                    Price = 50.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Transport",
                    Style = null,
                    MaxCapacity = 1,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome26Transport.jpeg",
                        },
                    ExternalLinks = "https://www.hertz.com"
                },
                new Service
                {
                    Id = 27,
                    Name = "Avis Car Rental",
                    Price = 45.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Transport",
                    Style = null,
                    MaxCapacity = 1,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome27Transport.jpeg",
                        },
                    ExternalLinks = "https://www.avis.com"
                },
                new Service
                {
                    Id = 28,
                    Name = "Sixt MyDriver",
                    Price = 90.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Transport",
                    Style = null,
                    MaxCapacity = 3,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome28Transport.jpeg",
                        },
                    ExternalLinks = "https://www.mydriver.com"
                },
                new Service
                {
                    Id = 29,
                    Name = "Rome Private Guides",
                    Price = 80.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Guide",
                    Style = null,
                    MaxCapacity = 1,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome29Guide.jpeg",
                        },
                    ExternalLinks = "https://www.romeprivateguides.com"
                },
                new Service
                {
                    Id = 30,
                    Name = "Walks of Italy",
                    Price = 75.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Rome",
                    Type = "Guide",
                    Style = null,
                    MaxCapacity = 1,
                    Images = new List<string>
                        {
                            "/ImagesServices/Rome30Guide.jpeg",
                        },
                    ExternalLinks = "https://www.walksofitaly.com"
                },
                new Service
                {
                    Id = 31,
                    Name = "Hotel Clement Prague",
                    Price = 80.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Accommodation",
                    Style = null,
                    MaxCapacity = 2,
                    Images = new List<string>
                        {
                            "/ImagesTest/serviceVilleId.jpg",
                        },
                    ExternalLinks = "https://www.hotelclement.cz"
                },
                new Service
                {
                    Id = 32,
                    Name = "Hotel Salvator",
                    Price = 95.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Accommodation",
                    Style = null,
                    MaxCapacity = 2,
                    Images = new List<string>
                        {
                            "/ImagesTest/serviceVilleId.jpg",
                        },
                    ExternalLinks = "https://www.hotelsalvator.cz"
                },
                new Service
                {
                    Id = 33,
                    Name = "Hotel Julian",
                    Price = 90.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Accommodation",
                    Style = null,
                    MaxCapacity = 2,
                    Images = new List<string>
                        {
                            "/ImagesTest/serviceVilleId.jpg",
                        },
                    ExternalLinks = "https://www.hoteljulian.com"
                },
                new Service
                {
                    Id = 34,
                    Name = "Mosaic House",
                    Price = 40.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Accommodation",
                    Style = null,
                    MaxCapacity = 4,
                    Images = new List<string>
                        {
                            "/ImagesTest/serviceVilleId.jpg",
                        },
                    ExternalLinks = "https://www.mosaichouse.com"
                },
                new Service
                {
                    Id = 35,
                    Name = "Hostel One Home",
                    Price = 25.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Accommodation",
                    Style = null,
                    MaxCapacity = 6,
                    Images = new List<string>
                        {
                            "/ImagesTest/serviceVilleId.jpg",
                        },
                    ExternalLinks = "https://www.hostelone.com"
                },
                new Service
                {
                    Id = 36,
                    Name = "U Fleků",
                    Price = 50.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 60,
                    Images = new List<string>
                        {
                            "/ImagesTest/serviceVilleId.jpg",
                        },
                    ExternalLinks = "https://www.ufleku.cz"
                },
                new Service
                {
                    Id = 37,
                    Name = "Czech Folklore Restaurant",
                    Price = 40.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 80,
                    Images = new List<string>
                        {
                            "/ImagesTest/serviceVilleId.jpg",
                        },
                    ExternalLinks = "https://www.czechfolklore.com"
                },
                new Service
                {
                    Id = 38,
                    Name = "Lokál Dlouhááá",
                    Price = 30.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 40,
                    Images = new List<string>
                        {
                            "/ImagesTest/serviceVilleId.jpg",
                        },
                    ExternalLinks = "https://www.lokal-dlouha.ambi.cz"
                },
                new Service
                {
                    Id = 39,
                    Name = "Prague Local Guides",
                    Price = 80.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Guide",
                    MaxCapacity = 1,
                    Images = new List<string>
                        {
                            "/ImagesTest/serviceVilleId.jpg",
                        },
                    ExternalLinks = "https://www.praguelocalguides.com"
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
                Password = "password123",
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
            },
            new User
            {
                Id = 7,
                LastName = "Rousseau",
                FirstName = "Alexandre",
                Email = "alexandre.rousseau@gmail.com",
                Password = "pass12345",
                Address = "14 Rue de Rivoli, Quartier des Tuileries, Paris",
                PhoneNumber = "0145678901",
                BirthDate = new DateTime(1994, 9, 2),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 8,
                LastName = "Leclerc",
                FirstName = "Manon",
                Email = "manon.leclerc@gmail.com",
                Password = "mdp98765",
                Address = "30 Rue Sainte-Catherine, Pentes de la Croix-Rousse, Lyon",
                PhoneNumber = "046543210",
                BirthDate = new DateTime(1993, 12, 18),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 9,
                LastName = "Dubois",
                FirstName = "Émilie",
                Email = "emilie.dubois@gmail.com",
                Password = "securepass123",
                Address = "18 Rue des Augustins, Centre Ville, Perpignan",
                PhoneNumber = "043216549",
                BirthDate = new DateTime(1992, 5, 28),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 10,
                LastName = "Martin",
                FirstName = "Antoine",
                Email = "antoine.martin@gmail.com",
                Password = "password456",
                Address = "10 Quai de la Tourette, Le Panier, Marseille",
                PhoneNumber = "067891234",
                BirthDate = new DateTime(1991, 8, 10),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 11,
                LastName = "Dubois",
                FirstName = "Jean",
                Email = "jean.dubois@gmail.com",
                Password = "motdepasse123",
                Address = "12 Rue de la Paix, Quartier Latin, Paris",
                PhoneNumber = "0145678901",
                BirthDate = new DateTime(1985, 8, 20),
            },
            new User
            {
                Id = 12,
                LastName = "Leroy",
                FirstName = "Sophie",
                Email = "sophie.leroy@gmail.com",
                Password = "securepassword",
                Address = "27 Rue du Palais Grillet, Presqu'île, Lyon",
                PhoneNumber = "046543210",
                BirthDate = new DateTime(1992, 3, 10),
            },
            new User
            {
                Id = 13,
                LastName = "Martin",
                FirstName = "Pierre",
                Email = "pierre.martin@gmail.com",
                Password = "12345678",
                Address = "45 Rue des Pénitents Bleus, Vieux Lyon, Lyon",
                PhoneNumber = "0141234564",
                BirthDate = new DateTime(1991, 11, 25),
            },
            new User
            {
                Id = 14,
                LastName = "Dubois",
                FirstName = "Marie",
                Email = "marie.dubois@gmail.com",
                Password = "mdp123",
                Address = "8 Rue de la République, Part-Dieu, Lyon",
                PhoneNumber = "0149876543",
                BirthDate = new DateTime(1988, 6, 15),
            },
            new User
            {
                Id = 15,
                LastName = "Dupont",
                FirstName = "Paul",
                Email = "paul.dupont@gmail.com",
                Password = "password",
                Address = "15 Rue de l'Ancienne Porte Neuve, Centre Ville, Perpignan",
                PhoneNumber = "0145678901",
                BirthDate = new DateTime(1995, 2, 18),
            },
            new User
            {
                Id = 16,
                LastName = "Lefèvre",
                FirstName = "Sophie",
                Email = "sophie.lefevre@gmail.com",
                Password = "secure123",
                Address = "22 Avenue de Grande Bretagne, Les Coves, Perpignan",
                PhoneNumber = "0410987654",
                BirthDate = new DateTime(1987, 7, 5),
            },
            new User
            {
                Id = 17,
                LastName = "Moreau",
                FirstName = "Luc",
                Email = "luc.moreau@gmail.com",
                Password = "pass1234",
                Address = "32 Rue Saint-Ferréol, Centre Ville, Marseille",
                PhoneNumber = "0623415678",
                BirthDate = new DateTime(1993, 9, 12),
            },
            new User
            {
                Id = 18,
                LastName = "Girard",
                FirstName = "Emma",
                Email = "emma.girard@gmail.com",
                Password = "password321",
                Address = "9 Quai du Port, Vieux Port, Marseille",
                PhoneNumber = "0168765432",
                BirthDate = new DateTime(1989, 4, 27),
            },
            new User
            {
                Id = 19,
                LastName = "Dupuis",
                FirstName = "Marc",
                Email = "marc.dupuis@gmail.com",
                Password = "mdp5678",
                Address = "23 Rue Esquermoise, Vieux Lille, Lille",
                PhoneNumber = "0131234567",
                BirthDate = new DateTime(1990, 12, 5),
            },
            new User
            {
                Id = 20,
                LastName = "Lefebvre",
                FirstName = "Julie",
                Email = "julie.lefebvre@gmail.com",
                Password = "securepass",
                Address = "8 Rue de la Clef, Centre Ville, Lille",
                PhoneNumber = "0139876543",
                BirthDate = new DateTime(1986, 2, 28),
            },
            new User
            {
                Id = 21,
                LastName = "Roy",
                FirstName = "Sophie",
                Email = "sophie.roy@gmail.com",
                Password = "pass1234",
                Address = "17 Rue Sainte-Catherine, Saint-Michel, Bordeaux",
                PhoneNumber = "0123456789",
                BirthDate = new DateTime(1992, 10, 15),
            },
            new User
            {
                Id = 22,
                LastName = "Gagnon",
                FirstName = "Alexandre",
                Email = "alexandre.gagnon@gmail.com",
                Password = "securepass",
                Address = "12 Avenue de la Marne, La Négresse, Biarritz",
                PhoneNumber = "0456789123",
                BirthDate = new DateTime(1988, 5, 2),
            },
            new User
            {
                Id = 23,
                LastName = "Tremblay",
                FirstName = "Emma",
                Email = "emma.tremblay@gmail.com",
                Password = "mdp7890",
                Address = "9 Rue des Francs-Bourgeois, Centre Ville, Strasbourg",
                PhoneNumber = "0498765432",
                BirthDate = new DateTime(1995, 12, 28),
            },
            new User
            {
                Id = 24,
                LastName = "Lavoie",
                FirstName = "Pierre",
                Email = "pierre.lavoie@gmail.com",
                Password = "password789",
                Address = "6 Boulevard des Anglais, Milady, Biarritz",
                PhoneNumber = "0632147859",
                BirthDate = new DateTime(1991, 7, 10),
            },
            new User
            {
                Id = 25,
                LastName = "Bélanger",
                FirstName = "Marie",
                Email = "marie.belanger@gmail.com",
                Password = "secure1234",
                Address = "15 Rue de la Douane, Centre Ville, Strasbourg",
                PhoneNumber = "0356897412",
                BirthDate = new DateTime(1987, 3, 22),
            },
            new User
            {
                Id = 26,
                LastName = "Fortin",
                FirstName = "Marc",
                Email = "marc.fortin@gmail.com",
                Password = "mdp4567",
                Address = "32 Avenue Jean Médecin, Carré d'Or, Nice",
                PhoneNumber = "0123456789",
                BirthDate = new DateTime(1993, 9, 5),
            },
            new User
            {
                Id = 27,
                LastName = "Gauthier",
                FirstName = "Isabelle",
                Email = "isabelle.gauthier@gmail.com",
                Password = "pass456",
                Address = "8 Rue Masséna, Vieux Nice, Nice",
                PhoneNumber = "0456789123",
                BirthDate = new DateTime(1990, 4, 12),

            },
            new User
            {
                Id = 28,
                LastName = "Morin",
                FirstName = "David",
                Email = "david.morin@gmail.com",
                Password = "password123",
                Address = "17 Rue du Maréchal Foch, Petite France, Strasbourg",
                PhoneNumber = "0498765432",
                BirthDate = new DateTime(1986, 11, 25),
            },
            new User
            {
                Id = 29,
                LastName = "Lévesque",
                FirstName = "Sarah",
                Email = "sarah.levesque@gmail.com",
                Password = "securepass789",
                Address = "6 Avenue Thiers, Jean Médecin, Nice",
                PhoneNumber = "0632147859",
                BirthDate = new DateTime(1994, 2, 18),
            },
            new User
            {
                Id = 30,
                LastName = "Caron",
                FirstName = "Thomas",
                Email = "thomas.caron@gmail.com",
                Password = "mdp789",
                Address = "15 Rue de la Juiverie, Bouffay, Nantes",
                PhoneNumber = "0132546897",
                BirthDate = new DateTime(1991, 10, 20),
            }
            );


            // Création des Customers
            this.Customers.AddRange(
                 new Customer
                 {
                     Id = 1,
                     User = Users.Find(1),
                     LoyaltyPoint = 100,
                     CommentPoint = 50
                 },
                new Customer
                {
                    Id = 2,
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


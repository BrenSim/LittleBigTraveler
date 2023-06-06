using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Web;
=======
using System.Linq;


>>>>>>> 29435c6c2af7e8eed44b585d8cc77c1a74648318
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
<<<<<<< HEAD
using Microsoft.AspNetCore.Http;
=======
using System.IO.Pipelines;
using System.Xml.Linq;
>>>>>>> 29435c6c2af7e8eed44b585d8cc77c1a74648318

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

                    "/ImagesDestinations/Paris1.jpeg",
                    "/ImagesDestinations/Paris2.jpeg",
                    "/ImagesDestinations/Paris3.jpeg",
                    "/ImagesDestinations/Paris4.jpeg"
                },
                ExternalLinks = "https://www.parisinfo.com/",
<<<<<<< HEAD
=======
            },
            new Destination
            {
                Id = 2,
                Country = "Italie",
                City = "Rome",
                Description = "Ville éternelle avec vestiges antiques, art religieux et gastronomie.",
                Images = new List<string>
                    {
                        "/ImagesDestinations/Rome1.jpeg",
                        "/ImagesDestinations/Rome2.jpeg",
                        "/ImagesDestinations/Rome3.jpeg",
                        "/ImagesDestinations/Rome4.jpeg"
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
                        "/ImagesDestinations/Prague1.jpeg",
                        "/ImagesDestinations/Prague2.jpeg",
                        "/ImagesDestinations/Prague3.jpeg",
                        "/ImagesDestinations/Prague4.jpeg"
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
                        "/ImagesDestinations/Venise1.jpeg",
                        "/ImagesDestinations/Venise2.jpeg",
                        "/ImagesDestinations/Venise3.jpeg",
                        "/ImagesDestinations/Venise4.jpeg"
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
                        "/ImagesDestinations/Munich1.jpeg",
                        "/ImagesDestinations/Munich2.jpeg",
                        "/ImagesDestinations/Munich3.jpeg",
                        "/ImagesDestinations/Munich4.jpeg"
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
                        "/ImagesDestinations/Londres1.jpeg",
                        "/ImagesDestinations/Londres2.jpeg",
                        "/ImagesDestinations/Londres3.jpeg",
                        "/ImagesDestinations/Londres4.jpeg"
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
                        "/ImagesDestinations/Athenes1.jpeg",
                        "/ImagesDestinations/Athenes2.jpeg",
                        "/ImagesDestinations/Athenes3.jpeg",
                        "/ImagesDestinations/Athenes4.jpeg"
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
                         "/ImagesDestinations/Barcelona1.jpeg",
                         "/ImagesDestinations/Barcelona2.jpeg",
                         "/ImagesDestinations/Barcelona3.jpeg",
                         "/ImagesDestinations/Barcelona4.jpeg"
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
                        "/ImagesDestinations/Innsbruck1.jpeg",
                        "/ImagesDestinations/Innsbruck2.jpeg",
                        "/ImagesDestinations/Innsbruck3.jpeg",
                        "/ImagesDestinations/Innsbruck4.jpeg"
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
                        "/ImagesDestinations/Annecy1.jpeg",
                        "/ImagesDestinations/Annecy2.jpeg",
                        "/ImagesDestinations/Annecy3.jpeg",
                        "/ImagesDestinations/Annecy4.jpeg"
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
                        "/ImagesDestinations/Garmisch-Partenkirchen1.jpeg",
                        "/ImagesDestinations/Garmisch-Partenkirchen2.jpeg",
                        "/ImagesDestinations/Garmisch-Partenkirchen3.jpeg",
                        "/ImagesDestinations/Garmisch-Partenkirchen4.jpeg"
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
                        "/ImagesDestinations/Lisbonne1.jpeg",
                        "/ImagesDestinations/Lisbonne2.jpeg",
                        "/ImagesDestinations/Lisbonne3.jpeg",
                        "/ImagesDestinations/Lisbonne4.jpeg"
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
                        "/ImagesDestinations/Interlaken1.jpeg",
                        "/ImagesDestinations/Interlaken2.jpeg",
                        "/ImagesDestinations/Interlaken3.jpeg",
                        "/ImagesDestinations/Interlaken4.jpeg"
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
                        "/ImagesDestinations/Chamonix1.jpeg",
                        "/ImagesDestinations/Chamonix2.jpeg",
                        "/ImagesDestinations/Chamonix3.jpeg",
                        "/ImagesDestinations/Chamonix4.jpeg"
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
                        "/ImagesDestinations/PlitviceLakes1.jpeg",
                        "/ImagesDestinations/PlitviceLakes2.jpeg",
                        "/ImagesDestinations/PlitviceLakes3.jpeg",
                        "/ImagesDestinations/PlitviceLakes4.jpeg"
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
                        "/ImagesDestinations/Tromso1.jpeg",
                        "/ImagesDestinations/Tromso2.jpeg",
                        "/ImagesDestinations/Tromso3.jpeg",
                        "/ImagesDestinations/Tromso4.jpeg"
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
                        "/ImagesDestinations/Killarney1.jpeg",
                        "/ImagesDestinations/Killarney2.jpeg",
                        "/ImagesDestinations/Killarney3.jpeg",
                        "/ImagesDestinations/Killarney4.jpeg"
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
                        "/ImagesDestinations/Budapest1.jpeg",
                        "/ImagesDestinations/Budapest2.jpeg",
                        "/ImagesDestinations/Budapest3.jpeg",
                        "/ImagesDestinations/Budapest4.webp"
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
                        "/ImagesDestinations/Bath1.jpeg",
                        "/ImagesDestinations/Bath2.jpeg",
                        "/ImagesDestinations/Bath3.jpeg",
                        "/ImagesDestinations/Bath4.jpeg"
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
                        "/ImagesDestinations/KarlovyVary1.jpeg",
                        "/ImagesDestinations/KarlovyVary2.jpeg",
                        "/ImagesDestinations/KarlovyVary3.jpeg",
                        "/ImagesDestinations/KarlovyVary4.png"
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
                        "/ImagesDestinations/Ischia1.jpeg",
                        "/ImagesDestinations/Ischia2.jpeg",
                        "/ImagesDestinations/Ischia3.jpeg",
                        "/ImagesDestinations/Ischia4.jpeg"
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
                        "/ImagesDestinations/Vichy1.jpeg",
                        "/ImagesDestinations/Vichy2.jpeg",
                        "/ImagesDestinations/Vichy3.jpeg",
                        "/ImagesDestinations/Vichy4.jpeg"
                    },
                ExternalLinks = "https://www.vichy-destinations.fr/",
>>>>>>> 29435c6c2af7e8eed44b585d8cc77c1a74648318
            }


            );


<<<<<<< HEAD

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
=======
                // Paris:
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
                    ExternalLinks = "https://www.hotelprincealbert.com/louvre",
                },


                new Service
>>>>>>> 29435c6c2af7e8eed44b585d8cc77c1a74648318
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
<<<<<<< HEAD
                ExternalLinks = "https://www.villagehostel.fr/montmartre",
                DestinationId = 1
            };
=======
>>>>>>> 29435c6c2af7e8eed44b585d8cc77c1a74648318

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

<<<<<<< HEAD
            // Ajoutez les services à la base de données
            this.Services.AddRange(
                serviceParis1,
                serviceParis2,
                serviceParis3,
                serviceParis4
            );
=======
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
>>>>>>> 29435c6c2af7e8eed44b585d8cc77c1a74648318




<<<<<<< HEAD
=======



            // Rome:
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
                                    "/ImagesServices/16RomeHotel.jpeg",
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
                            "/ImagesServices/17RomeHotel.jpeg",
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
                            "/ImagesServices/18RomeHotel.jpeg",
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
                            "/ImagesServices/19RomeHotel.jpeg",
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
                            "/ImagesServices/20RomeHotel.jpeg",
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
                            "/ImagesServices/21RomeRestaurant.jpeg",
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
                            "/ImagesServices/22RomeRestaurant.jpeg",
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
                            "/ImagesServices/23RomeRestaurant.jpeg",
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
                            "/ImagesServices/24RomeRestaurant.jpeg",
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
                            "/ImagesServices/25RomeRestaurant.jpeg",
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
                            "/ImagesServices/26RomeTransport.jpeg",
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
                            "/ImagesServices/27RomeTransport.jpeg",
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
                            "/ImagesServices/28RomeTransport.jpeg",
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
                            "/ImagesServices/29RomeGuide.jpeg",
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
                            "/ImagesServices/30Rome0Guide.jpeg",
                        },
                    ExternalLinks = "https://www.walksofitaly.com"
                },
                // Prague :
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
                            "/ImagesServices/31PragueHotel.jpeg",
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
                            "/ImagesServices/32PragueHotel.png",
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
                            "/ImagesServices/33PragueHotel.png",
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
                            "/ImagesServices/34PragueHotel.jpeg",
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
                            "/ImagesServices/35PragueHotel.jpeg",
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
                            "/ImagesServices/36PragueRestaurant.png",
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
                            "/ImagesServices/37PragueRestaurant.png",
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
                            "/ImagesServices/38PragueRestaurant.png",
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
                            "/ImagesServices/39PragueGuide.png",
                        },
                    ExternalLinks = "https://www.praguelocalguides.com"
                },

                new Service
                {
                    Id = 40,
                    Name = "Prague Urban Adventures",
                    Price = 80.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Guide",
                    Style = null,
                    MaxCapacity = 10,
                    Images = new List<string>
                        {
                            "/ImagesServices/40PragueGuide.webp",
                        },
                    ExternalLinks = "https://www.urbanadventures.com/destination/Prague-tours"
                },

                new Service
                {
                    Id = 41,
                    Name = "City Walking Tour, Boat Cruise, and Typical Czech Lunch",
                    Price = 25.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "City Tour",
                    Style = "Culturel",
                    MaxCapacity = 15,
                    Images = new List<string>
                        {
                            "/ImagesServices/41PragueTour.png"
                        },
                    ExternalLinks = "https://www.funinprague.eu/en/"
                },

                new Service
                {
                    Id = 42,
                    Name = "Prague Castle Walking Tour Including Admission Tickets",
                    Price = 20.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Visite culturelle",
                    Style = "Culturel",
                    MaxCapacity = 50,
                    Images = new List<string>
                        {
                            "/ImagesServices/42PragueVisite.png"
                        },
                    ExternalLinks = "https://www.getpragueguide.com/"
                },

                new Service
                {
                    Id = 43,
                    Name = "Panoramic Vltava River Cruise",
                    Price = 30.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Croisière en bateau",
                    Style = "Culturel",
                    MaxCapacity = 30,
                    Images = new List<string>
                        {
                            "/ImagesServices/43PragueActivite.png"
                        },
                    ExternalLinks = "https://www.premiant.cz/fra/"
                },

                new Service
                {
                    Id = 44,
                    Name = "3-hour Complete Prague Bike Tour",
                    Price = 35.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Activité sportive",
                    Style = "Sportive",
                    MaxCapacity = 18,
                    Images = new List<string>
                    {
                        "/ImagesServices/44PragueActivite.png"
                    },
                    ExternalLinks = "https://mijntours.com/prague/"
                },

                new Service
                {
                    Id = 45,
                    Name = "Prague Kayaking Adventure",
                    Price = 40.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Prague",
                    Type = "Activité sportive",
                    Style = "Sportive",
                    MaxCapacity = 8,
                    Images = new List<string>
                    {
                        "/ImagesServices/45PragueActivite.png"
                    },
                    ExternalLinks = "https://www.padlujeme.cz/"
                },
                // Venise:
                new Service
                {
                    Id = 46,
                    Name = "Hotel Rialto",
                    Price = 125.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                        {
                            "/ImagesServices/46VeniseHotel.png",
                        },
                    ExternalLinks = "https://www.rialtohotel.com/"
                },

                new Service
                {
                    Id = 47,
                    Name = "Hotel Danieli",
                    Price = 120.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/47VeniseHotel.png",
                    },
                    ExternalLinks = "https://www.hoteldanieli.com"
                },

                new Service
                {
                    Id = 48,
                    Name = "Hotel Ca'Sagredo",
                    Price = 110.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/48VeniseHotel.webp",
                    },
                    ExternalLinks = "https://www.casagredohotel.com"
                },

                new Service
                {
                    Id = 49,
                    Name = "Hotel Londra Palace",
                    Price = 100.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/49VeniseHotel.webp",
                    },
                    ExternalLinks = "https://www.londrapalace.com"
                },

                new Service
                {
                    Id = 50,
                    Name = "Hotel Canal Grande",
                    Price = 90.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/50VeniseHotel.png",
                    },
                    ExternalLinks = "https://www.hotelcanalgrande.it"
                },

                new Service
                {
                    Id = 51,
                    Name = "Restaurant Antiche Carampane",
                    Price = 60.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/51VeniseRestaurant.png",
                    },
                    ExternalLinks = "https://www.antichecarampane.com"
                },

                new Service
                {
                    Id = 52,
                    Name = "Osteria Alle Testiere",
                    Price = 70.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 60,
                    Images = new List<string>
                    {
                        "/ImagesServices/52VeniseRestaurant.jpeg",
                    },
                    ExternalLinks = "http://www.osterialletestiere.it/"
                },

                new Service
                {
                    Id = 53,
                    Name = "Trattoria Da Romano",
                    Price = 55.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 40,
                    Images = new List<string>
                    {
                        "/ImagesServices/53VeniseRestaurant.png",
                    },
                    ExternalLinks = "https://daromano.it/"
                },

                new Service
                {
                    Id = 54,
                    Name = "Venice Tours",
                    Price = 150.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Visite Guideé",
                    Style = "Culturel",
                    MaxCapacity = 10,
                    Images = new List<string>
                    {
                        "/ImagesServices/54VeniseTour.png",
                    },
                    ExternalLinks = "https://www.venicetours.it"
                },

                new Service
                {
                    Id = 55,
                    Name = "Gondola Serenade Tour",
                    Price = 80.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Tour en Gondole",
                    Style = "Culturel",
                    MaxCapacity = 4,
                    Images = new List<string>
                    {
                        "/ImagesServices/55VeniseTour.png",
                    },
                    ExternalLinks = "https://www.gondolaserenade.com"
                },

                new Service
                {
                    Id = 56,
                    Name = "Visite de la Basilique Saint-Marc",
                    Price = 20.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Culture",
                    Style = "Culture",
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/56VeniseVisite.png",
                    },
                    ExternalLinks = "https://www.basilicasanmarco.it"
                },

                new Service
                {
                    Id = 57,
                    Name = "Museum Gallerie dell'Accademia",
                    Price = 15.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Culture",
                    Style = "Culturel",
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/57VeniseVisite.png",
                    },
                    ExternalLinks = "https://www.gallerieaccademia.it"
                },

                new Service
                {
                    Id = 58,
                    Name = "The Garden Space Wellness & Events",
                    Price = 70.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Activité",
                    Style = "Relax",
                    MaxCapacity = 20,
                    Images = new List<string>
                    {
                        "/ImagesServices/58VeniseSpa.png",
                    },
                    ExternalLinks = "https://thegardenspacevenice.it/"
                },

                new Service
                {
                    Id = 59,
                    Name = "Location de vélos à Venise",
                    Price = 25.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Activité",
                    Style = "Sportive",
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/59VeniseVelo.jpeg",
                    },
                    ExternalLinks = "https://www.noleggioscooterlido.it/"
                },

                new Service
                {
                    Id = 60,
                    Name = "Kayak sur les canaux de Venise",
                    Price = 40.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Venise",
                    Type = "Activité",
                    Style = "Sportive",
                    MaxCapacity = 10,
                    Images = new List<string>
                    {
                        "/ImagesServices/60VeniseSport.png",
                    },
                    ExternalLinks = "https://www.venicekayak.com/"
                },
                // Munich :
                new Service
                {
                    Id = 61,
                    Name = "Hotel Bayerischer Hof",
                    Price = 120.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                        {
                            "/ImagesServices/61MunichHotel.png",
                        },
                    ExternalLinks = "https://www.bayerischerhof.de/en/index.html"
                },
                new Service
                {
                    Id = 62,
                    Name = "Hotel Vier Jahreszeiten Kempinski",
                    Price = 110.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/62MunichHotel.png",
                    },
                    ExternalLinks = "https://www.kempinski.com/en/hotel-vier-jahreszeiten"
                },
                new Service
                {
                    Id = 63,
                    Name = "Hotel Munich City Hilton",
                    Price = 100.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/63MunichHotel.jpeg",
                    },
                    ExternalLinks = "https://www.hilton.com/en/hotels/mucchtw-hilton-munich-city/"
                },
                new Service
                {
                    Id = 64,
                    Name = "Wombat's City Hostel Munich",
                    Price = 30.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 50,
                    Images = new List<string>
                        {
                            "/ImagesServices/64MunichHotel.jpeg",
                        },
                    ExternalLinks = "https://www.wombats-hostels.com/de/munich"
                },
                new Service
                {
                    Id = 65,
                    Name = "Hotel Eurostars Grand Central Munich",
                    Price = 80.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/65MunichHotel.png",
                    },
                    ExternalLinks = "https://www.eurostarshotels.co.uk/eurostars-grand-central.html"
                },
                new Service
                {
                    Id = 66,
                    Name = "Restaurant Hofbräuhaus",
                    Price = 50.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 150,
                    Images = new List<string>
                    {
                        "/ImagesServices/66MunichRestaurant.png",
                    },
                    ExternalLinks = "https://www.hofbraeuhaus.de/en/welcome.html"
                },
                new Service
                {
                    Id = 67,
                    Name = "Restaurant Tantris",
                    Price = 70.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 60,
                    Images = new List<string>
                    {
                        "/ImagesServices/67MunichRestaurant.jpeg",
                    },
                    ExternalLinks = "https://tantris.de/en/"
                },
                new Service
                {
                    Id = 68,
                    Name = "Restaurant Brenner Grill",
                    Price = 55.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 80,
                    Images = new List<string>
                    {
                        "/ImagesServices/68MunichRestaurant.jpeg",
                    },
                    ExternalLinks = "https://www.brennergrill.de/en/"
                },
                new Service
                {
                    Id = 69,
                    Name = "Simply Munich Tours",
                    Price = 150.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Activité",
                    Style = "Culturel",
                    MaxCapacity = 10,
                    Images = new List<string>
                    {
                        "/ImagesServices/69MunichTours.png",
                    },
                    ExternalLinks = "https://www.munich.travel/en/"
                },
                new Service
                {
                    Id = 70,
                    Name = "Pinakothek der Moderne",
                    Price = 15.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Activité",
                    Style = "Culturel",
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/70MunichVisite.jpeg",
                    },
                    ExternalLinks = "https://www.pinakothek-der-moderne.de/en/"
                },
                new Service
                {
                    Id = 71,
                    Name = "Excursion classique de Munich à vélo",
                    Price = 70.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Activité sportive",
                    Style = "Sport",
                    MaxCapacity = 20,
                    Images = new List<string>
                    {
                        "/ImagesServices/71MunichVelo.png",
                    },
                    ExternalLinks = "https://www.mikesbiketours.com/munich/"
                },
                new Service
                {
                    Id = 72,
                    Name = "Excursion le long de la route romantique de Rothenburg à Harburg",
                    Price = 60.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Tour guidé",
                    Style = "Culturel",
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/72MunichVisite.webp",
                    },
                    ExternalLinks = "https://www.munichdaytrips.com/en"
                },
                new Service
                {
                    Id = 73,
                    Name = "Visite privée à pied de Munich",
                    Price = 40.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Tour guidé",
                    Style = "Culturel",
                    MaxCapacity = 10,
                    Images = new List<string>
                    {
                        "/ImagesServices/73MunichVisite.jpeg",
                    },
                    ExternalLinks = "https://www.inmunichtours.com/"
                },

                new Service
                {
                    Id = 74,
                    Name = "Randonnée privée dans les Alpes bavaroises",
                    Price = 289.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Activitée Sportive",
                    Style = "Sport",
                    MaxCapacity = 10,
                    Images = new List<string>
                    {
                        "/ImagesServices/74MunichRandonnee.webp",
                    },
                    ExternalLinks = "https://www.munich-wanderland.com/"
                },

                // Londres :
                new Service
                {
                    Id = 75,
                    Name = "The Ritz London",
                    Price = 250.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 200,
                    Images = new List<string>
                    {
                        "/ImagesServices/75LondresHotel.png",
                    },
                    ExternalLinks = "https://www.theritzlondon.com"
                },

                new Service
                {
                    Id = 76,
                    Name = "The Savoy",
                    Price = 220.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 150,
                    Images = new List<string>
                    {
                        "/ImagesServices/76LondresHotel.png",
                    },
                    ExternalLinks = "https://www.thesavoylondon.com"
                },

                new Service
                {
                    Id = 77,
                    Name = "Claridge's",
                    Price = 200.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/77LondresHotel.png",
                    },
                    ExternalLinks = "https://www.claridges.co.uk"
                },

                new Service
                {
                    Id = 78,
                    Name = "The Dorchester",
                    Price = 180.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 150,
                    Images = new List<string>
                    {
                        "/ImagesServices/78LondresHotel.png",
                    },
                    ExternalLinks = "https://www.dorchestercollection.com/en/london/the-dorchester"
                },

                new Service
                {
                    Id = 79,
                    Name = "Shangri-La Hotel at The Shard",
                    Price = 160.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/79LondresHotel.webp",
                    },
                    ExternalLinks = "https://www.shangri-la.com/london/shangrila"
                },

                new Service
                {
                    Id = 80,
                    Name = "The Ledbury",
                    Price = 120.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 80,
                    Images = new List<string>
                    {
                        "/ImagesServices/80LondresRestaurant.png",
                    },
                    ExternalLinks = "https://www.theledbury.com"
                },

                new Service
                {
                    Id = 81,
                    Name = "Sketch",
                    Price = 100.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 120,
                    Images = new List<string>
                    {
                        "/ImagesServices/81LondresRestaurant.webp",
                    },
                    ExternalLinks = "https://www.sketch.london"
                },

                new Service
                {
                    Id = 82,
                    Name = "Dishoom",
                    Price = 80.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 150,
                    Images = new List<string>
                    {
                        "/ImagesServices/82LondresRestaurant.png",
                    },
                    ExternalLinks = "https://www.dishoom.com"
                },

                new Service
                {
                    Id = 83,
                    Name = "London Walks",
                    Price = 150.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Visite Guidée",
                    Style = "Culturel",
                    MaxCapacity = 15,
                    Images = new List<string>
                    {
                        "/ImagesServices/83LondresService.jpeg",
                    },
                    ExternalLinks = "https://www.walks.com/london"
                },

                new Service
                {
                    Id = 84,
                    Name = "The British Museum",
                    Price = 20.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Activité Culturelle",
                    Style = "Culture",
                    MaxCapacity = 200,
                    Images = new List<string>
                    {
                        "/ImagesServices/84LondresActivite.png",
                    },
                    ExternalLinks = "https://www.britishmuseum.org"
                },

                new Service
                {
                    Id = 85,
                    Name = "Tower of London",
                    Price = 25.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Activité Culturelle",
                    Style = "Culturel",
                    MaxCapacity = 150,
                    Images = new List<string>
                    {
                        "/ImagesServices/85LondresActivite.png",
                    },
                    ExternalLinks = "https://www.hrp.org.uk/tower-of-london"
                },

                new Service
                {
                    Id = 86,
                    Name = "Thames River Cruise",
                    Price = 14.9,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Croisière",
                    Style = "Culturel",
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/86LondresActivite.png",
                    },
                    ExternalLinks = "https://www.thames-river-cruise.com/it/?msclkid=9dac8ae633d61bc42889ee9ed0ed5606"
                },

                new Service
                {
                    Id = 87,
                    Name = "Visite au Hyde Park",
                    Price = 0.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Visite",
                    Style = "Culturel",
                    MaxCapacity = 1000,
                    Images = new List<string>
                    {
                        "/ImagesServices/87LondresActivite.png",
                    },
                    ExternalLinks = "https://www.royalparks.org.uk/parks/hyde-park"
                },

                new Service
                {
                    Id = 88,
                    Name = "Excursion matinale à Stonehenge",
                    Price = 35.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Visite Guidée",
                    Style = "Culturel",
                    MaxCapacity = 30,
                    Images = new List<string>
                    {
                        "/ImagesServices/88LondresActivite.png",
                    },
                    ExternalLinks = "https://www.daytourslondon.com/half-day-stonehenge-tour-from-london/"
                },

                new Service
                {
                    Id = 89,
                    Name = "Croisière sur la Tamise en bateau à grande vitesse",
                    Price = 50.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Croisière en bateau",
                    Style = "Sportive",
                    MaxCapacity = 30,
                    Images = new List<string>
                    {
                        "/ImagesServices/89LondresActivite.png",
                    },
                    ExternalLinks = "https://thamesribexperience.com/"
                },

                new Service
                {
                    Id = 90,
                    Name = "Visite au Musée d'Histoire Naturelle de Londres",
                    Price = 30.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Londres",
                    Type = "Visite Culturelle",
                    Style = "Culturel",
                    MaxCapacity = 30,
                    Images = new List<string>
                    {
                        "/ImagesServices/90LondresActivite.png",
                    },
                    ExternalLinks = "https://www.nhm.ac.uk/visit.html"
                },
                // Athènes
                new Service
                {
                    Id = 91,
                    Name = "Athens Backpackers",
                    Price = 20.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 150,
                    Images = new List<string>
                        {
                            "/ImagesServices/91AthensHostel.png",
                        },
                    ExternalLinks = "https://backpackers.gr/"
                },

                new Service
                {
                    Id = 92,
                    Name = "City Circus Athens",
                    Price = 15.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/92AthensHostel.png",
                    },
                    ExternalLinks = "https://www.citycircus.gr"
                },

                new Service
                {
                    Id = 93,
                    Name = "Athens Studios",
                    Price = 18.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 120,
                    Images = new List<string>
                    {
                        "/ImagesServices/93AthensHostel.webp",
                    },
                    ExternalLinks = "https://www.athensstudios.gr"
                },

                new Service
                {
                    Id = 94,
                    Name = "Student & Travellers Inn",
                    Price = 17.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 80,
                    Images = new List<string>
                    {
                        "/ImagesServices/94AthensHostel.webp",
                    },
                    ExternalLinks = "https://the-student-travellers-inn.athens-greecehotels.com/en/"
                },

                new Service
                {
                    Id = 95,
                    Name = "San Remo Hostel",
                    Price = 16.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 60,
                    Images = new List<string>
                    {
                        "/ImagesServices/95AthensHostel.jpeg",
                    },
                    ExternalLinks = "http://hostelsanremoathens.com/"
                },

                new Service
                {
                    Id = 96,
                    Name = "Dionysos Zonars",
                    Price = 80.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/96AthensRestaurant.png",
                    },
                    ExternalLinks = "https://www.dionysoszonars.gr"
                },

                new Service
                {
                    Id = 97,
                    Name = "Thes Greek Creative Cuisine",
                    Price = 70.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 80,
                    Images = new List<string>
                    {
                        "/ImagesServices/97.jpeg",
                    },
                    ExternalLinks = "https://thes.katalogos.menu/#/el"
                },

                new Service
                {
                    Id = 98,
                    Name = "Liondi",
                    Price = 60.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/98AthensRestaurant.png",
                    },
                    ExternalLinks = "https://www.liondi.com/"
                },

                new Service
                {
                    Id = 99,
                    Name = "Athens Walking Tours",
                    Price = 120.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Guide",
                    Style = "",
                    MaxCapacity = 20,
                    Images = new List<string>
                    {
                        "/ImagesServices/99AthensActivite.svg",
                    },
                    ExternalLinks = "https://www.athenswalkingtours.gr"
                },

                new Service
                {
                    Id = 100,
                    Name = "Acropolis Museum",
                    Price = 15.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Culture",
                    Style = "Culture",
                    MaxCapacity = 200,
                    Images = new List<string>
                    {
                        "/ImagesServices/100AthensActivite.jpeg",
                    },
                    ExternalLinks = "https://www.theacropolismuseum.gr"
                },

                new Service
                {
                    Id = 101,
                    Name = "National Archaeological Museum",
                    Price = 10.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Culture",
                    Style = "",
                    MaxCapacity = 150,
                    Images = new List<string>
                    {
                        "/ImagesServices/101AthensActivite.png",
                    },
                    ExternalLinks = "https://www.namuseum.gr"
                },

                new Service
                {
                    Id = 102,
                    Name = "Croisière d'une journée avec déjeuner et boissons inclus",
                    Price = 135.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Visite guidée",
                    Style = "Nature",
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/102AthensActivite.png",
                    },
                    ExternalLinks = "https://alldaycruise.net/"
                },

                new Service
                {
                    Id = 103,
                    Name = "Balade l'après-midi à l'Acropole",
                    Price = 56.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Sport",
                    Style = "",
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/103AthensActivite.png",
                    },
                    ExternalLinks = "https://www.athens-walks.com/"
                },

                new Service
                {
                    Id = 104,
                    Name = "Athènes hors des sentiers battus",
                    Price = 53.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Athènes",
                    Type = "Visite Guidée",
                    Style = "Culturel",
                    MaxCapacity = 10,
                    Images = new List<string>
                    {
                        "/ImagesServices/104AthensActivite.png",
                    },
                    ExternalLinks = "https://www.withlocals.com/fr/experiences/greece/athens/"
                },
                // Barcelona
                new Service
                {
                    Id = 105,
                    Name = "Yeah Barcelona Hostel",
                    Price = 25.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 150,
                    Images = new List<string>
                    {
                        "/ImagesServices/105BarcelonaHostel.png",
                    },
                    ExternalLinks = "https://www.yeahhostels.com/barcelona"
                },

                new Service
                {
                    Id = 106,
                    Name = "Safestay Barcelona Sea",
                    Price = 20.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/106BarcelonaHostel.png",
                    },
                    ExternalLinks = "https://www.safestay.com/barcelona-sea"
                },

                new Service
                {
                    Id = 107,
                    Name = "Casa Gracia Barcelona Hostel",
                    Price = 22.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 120,
                    Images = new List<string>
                    {
                        "/ImagesServices/107BarcelonaHostel.jpeg",
                    },
                    ExternalLinks = "https://www.casagraciabcn.com"
                },

                new Service
                {
                    Id = 108,
                    Name = "Hotel Arts Barcelona",
                    Price = 250.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 200,
                    Images = new List<string>
                    {
                        "/ImagesServices/108BarcelonaHotel.png",
                    },
                    ExternalLinks = "https://www.hotelartsbarcelona.com"
                },

                new Service
                {
                    Id = 109,
                    Name = "W Barcelona",
                    Price = 200.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 180,
                    Images = new List<string>
                    {
                        "/ImagesServices/109BarcelonaHotel.webp",
                    },
                    ExternalLinks = "https://www.marriott.com/hotels/travel/bcnwh-w-barcelona"
                },

                new Service
                {
                    Id = 110,
                    Name = "Con Garcia",
                    Price = 150.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/110Barcelonarestaurant.png",
                    },
                    ExternalLinks = "https://congraciarestaurant.com/"
                },

                new Service
                {
                    Id = 111,
                    Name = "El Celler de Can Roca",
                    Price = 250.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 60,
                    Images = new List<string>
                    {
                        "/ImagesServices/111Barcelonarestaurant.png",
                    },
                    ExternalLinks = "https://www.cellercanroca.com"
                },

                new Service
                {
                    Id = 112,
                    Name = "Disfrutar",
                    Price = 180.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 40,
                    Images = new List<string>
                    {
                        "/ImagesServices/112Barcelonarestaurant.png",
                    },
                    ExternalLinks = "https://www.disfrutarbarcelona.com"
                },

                new Service
                {
                    Id = 113,
                    Name = "Visite guidée du quartier gotique",
                    Price = 106.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Visite guidée",
                    Style = "Culturel",
                    MaxCapacity = 15,
                    Images = new List<string>
                    {
                        "/ImagesServices/113BarcelonaActivite.jpeg",
                    },
                    ExternalLinks = "https://www.contexttravel.com/cities/barcelona"
                },

                new Service
                {
                    Id = 114,
                    Name = "Promenade autour du passé noir de l'histoire de Barcelona",
                    Price = 17.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Visite guidée",
                    Style = "Culturel",
                    MaxCapacity = 20,
                    Images = new List<string>
                    {
                        "/ImagesServices/114BarcelonaActivite.webp",
                    },
                    ExternalLinks = "https://www.runnerbeantours.com/barcelona"
                },

                new Service
                {
                    Id = 115,
                    Name = "Visita de la Sagrada Familia",
                    Price = 26.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Cultural Activity",
                    Style = "",
                    MaxCapacity = 1000,
                    Images = new List<string>
                    {
                        "/ImagesServices/115BarcelonaActivite.jpeg",
                    },
                    ExternalLinks = "https://sagradafamilia.org"
                },

                new Service
                {
                    Id = 116,
                    Name = "Park Güell",
                    Price = 15.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Visite",
                    Style = "Nature",
                    MaxCapacity = 500,
                    Images = new List<string>
                    {
                        "/ImagesServices/116BarcelonaActivite.png",
                    },
                    ExternalLinks = "https://parkguell.barcelona"
                },

                new Service
                {
                    Id = 117,
                    Name = "Picasso Museum",
                    Price = 10.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Visite",
                    Style = "Culture",
                    MaxCapacity = 200,
                    Images = new List<string>
                    {
                        "/ImagesServices/117BarcelonaActivite.png",
                    },
                    ExternalLinks = "https://museupicassobcn.cat/"
                },

                new Service
                {
                    Id = 118,
                    Name = "Mercat de la Boqueria",
                    Price = 30.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Visite",
                    Style = "Culture",
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/118BarcelonaActivite.jpeg",
                    },
                    ExternalLinks = "https://www.boqueria.barcelona/"
                },

                new Service
                {
                    Id = 119,
                    Name = "Barcelona Bike Tour",
                    Price = 25.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Barcelona",
                    Type = "Visite",
                    Style = "Sportive",
                    MaxCapacity = 30,
                    Images = new List<string>
                    {
                        "/ImagesServices/119BarcelonaActivite.png",
                    },
                    ExternalLinks = "https://www.buenavistatours.es/"
                },
                // Innsbruck :
                new Service
                {
                    Id = 120,
                    Name = "Hostel Marmota",
                    Price = 20.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 60,
                    Images = new List<string>
                    {
                        "/ImagesServices/120InnsbruckHostel.png",
                    },
                    ExternalLinks = "https://www.hostelmarmota.com"
                },

                new Service
                {
                    Id = 121,
                    Name = "Jugendherberge Youth Hostel Innsbruck",
                    Price = 20.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/121InnsbruckHostel.png",
                    },
                    ExternalLinks = "https://www.youth-hostel-innsbruck.at/"
                },
                // Hôtels

                new Service
                {
                    Id = 122,
                    Name = "Das Innsbruck Hotel",
                    Price = 150.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 80,
                    Images = new List<string>
                    {
                        "/ImagesServices/122InnsbruckHotel.png",
                    },
                    ExternalLinks = "https://www.hotelinnsbruck.com"
                },

                new Service
                {
                    Id = 123,
                    Name = "AC Hotel Innsbruck",
                    Price = 120.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Accomodation",
                    Style = null,
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/123InnsbruckHotel.webp",
                    },
                    ExternalLinks = "https://www.marriott.com/hotels/travel/innac-ac-hotel-innsbruck"
                },

                new Service
                {
                    Id = 124,
                    Name = "Sitzwohl Restaurant",
                    Price = 80.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/124InnsbruckRestaurant.svg",
                    },
                    ExternalLinks = "https://www.sitzwohl-innsbruck.at"
                },

                new Service
                {
                    Id = 125,
                    Name = "Lichtblick Restaurant",
                    Price = 100.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Restaurant",
                    Style = null,
                    MaxCapacity = 60,
                    Images = new List<string>
                    {
                        "/ImagesServices/125InnsbruckRestaurant.svg",
                    },
                    ExternalLinks = "https://www.restaurant-lichtblick.at/"
                },

                new Service
                {
                    Id = 126,
                    Name = "Visite des Dolomites, des lacs y compris Braies d'Innsbruck",
                    Price = 90.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Visite guidée",
                    Style = "Nature",
                    MaxCapacity = 20,
                    Images = new List<string>
                    {
                        "/ImagesServices/126InnsbruckActivite.webp",
                    },
                    ExternalLinks = "https://www.tyroltravel.net/experiences/tour-of-dolomites%2C-alpine-lakes-including-braies"
                },

                new Service
                {
                    Id = 127,
                    Name = "Zoo Alpin d'Innsbruck",
                    Price = 15.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Visite",
                    Style = "Nature",
                    MaxCapacity = 30,
                    Images = new List<string>
                    {
                        "/ImagesServices/127InnsbruckActivite.png",
                    },
                    ExternalLinks = "https://www.alpenzoo.at"
                },

                new Service
                {
                    Id = 128,
                    Name = "Visite audioguidée d'une heure et demie d'Innsbruck",
                    Price = 27.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Visite guidée",
                    Style = "Culture",
                    MaxCapacity = 30,
                    Images = new List<string>
                    {
                        "/ImagesServices/127InnsbruckActivite.png",
                    },
                    ExternalLinks = "https://www.tyroltravel.net/product-page/innsbrucktourdg"
                },

                new Service
                {
                    Id = 129,
                    Name = "Golden Roof Museum",
                    Price = 10.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Visite",
                    Style = "Culture",
                    MaxCapacity = 200,
                    Images = new List<string>
                    {
                        "/ImagesServices/129InnsbruckActivite.png",
                    },
                    ExternalLinks = "https://www.innsbruck.gv.at/en/freizeit/kultur/museen-stadtarchiv/museum-goldenes-dachl"
                },

                new Service
                {
                    Id = 130,
                    Name = "Hofburg Innsbruck Museum",
                    Price = 12.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Visite",
                    Style = "Culture",
                    MaxCapacity = 150,
                    Images = new List<string>
                    {
                        "/ImagesServices/130InnsbruckActivite.png",
                    },
                    ExternalLinks = "https://www.burghauptmannschaft.at/Betriebe/Hofburg-Innsbruck.html"
                },

                new Service
                {
                    Id = 131,
                    Name = "Bergisel Ski Jump",
                    Price = 15.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Visite",
                    Style = "Nature" + " " + "Sport",
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/131InnsbruckActivite.png",
                    },
                    ExternalLinks = "https://www.bergisel.info"
                },

                new Service
                {
                    Id = 132,
                    Name = "Nordkette Cable Car",
                    Price = 30.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Visite",
                    Style = "Nature" + " " + "Sport",
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/132InnsbruckActivite.png",
                    },
                    ExternalLinks = "https://www.nordkette.com/en"
                },

                new Service
                {
                    Id = 133,
                    Name = "Complex Olympic OlympiaWorld Innsbruck",
                    Price = 25.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Sport Activity",
                    Style = "Culture" + " " + "Sport",
                    MaxCapacity = 40,
                    Images = new List<string>
                    {
                        "/ImagesServices/133InnsbruckActivite.png",
                    },
                    ExternalLinks = "https://www.olympiaworld.at"
                },

                new Service
                {
                    Id = 134,
                    Name = "Up Stream Surfing Innsbruck",
                    Price = 25.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Innsbruck",
                    Type = "Activité",
                    Style = "Nature" + " " + "Sport",
                    MaxCapacity = 40,
                    Images = new List<string>
                    {
                        "/ImagesServices/134InnsbruckActivite.png",
                    },
                    ExternalLinks = "https://www.upstreamsurfing.com/"
                },
                //Annecy
                new Service
                {
                    Id = 135,
                    Name = "Allobroges Park Hotel",
                    Price = 30.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Accomodation",
                    Style = "",
                    MaxCapacity = 80,
                    Images = new List<string>
                    {
                        "/ImagesServices/135.jpeg",
                    },
                    ExternalLinks = "https://www.allobrogesparkhotel.com"
                },

                new Service
                {
                    Id = 136,
                    Name = "Hotel des Alpes",
                    Price = 40.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Accomodation",
                    Style = "",
                    MaxCapacity = 60,
                    Images = new List<string>
                    {
                        "/ImagesServices/136.jpeg",
                    },
                    ExternalLinks = "https://www.hoteldesalpes-annecy.com"
                },

                // Hôtels
                new Service
                {
                    Id = 137,
                    Name = "Hotel Imperial Palace",
                    Price = 120.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Accomodation",
                    Style = "",
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/137.jpeg",
                    },
                    ExternalLinks = "https://www.imperialpalace.fr"
                },

                new Service
                {
                    Id = 138,
                    Name = "Hotel Les Tresoms",
                    Price = 150.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Accomodation",
                    Style = "",
                    MaxCapacity = 80,
                    Images = new List<string>
                    {
                        "/ImagesServices/138.jpeg",
                    },
                    ExternalLinks = "https://www.lestresoms.com"
                },

                // Restaurants
                new Service
                {
                    Id = 139,
                    Name = "L'Esquisse Restaurant",
                    Price = 80.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Restaurant",
                    Style = "",
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/139.jpeg",
                    },
                    ExternalLinks = "https://www.restaurant-lesquisse.fr"
                },

                new Service
                {
                    Id = 140,
                    Name = "Le Belvedere Restaurant",
                    Price = 100.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Restaurant",
                    Style = "",
                    MaxCapacity = 60,
                    Images = new List<string>
                    {
                        "/ImagesServices/140.jpeg",
                    },
                    ExternalLinks = "https://www.belvedere-annecy.com"
                },

                // Visites guidées
                new Service
                {
                    Id = 141,
                    Name = "Annecy Segway Tours",
                    Price = 40.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Guided Tour",
                    Style = "",
                    MaxCapacity = 15,
                    Images = new List<string>
                    {
                        "/ImagesServices/141.jpeg",
                    },
                    ExternalLinks = "https://www.annecy-segway-tours.com"
                },

                new Service
                {
                    Id = 142,
                    Name = "Annecy City Sightseeing Tour",
                    Price = 30.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Guided Tour",
                    Style = "",
                    MaxCapacity = 20,
                    Images = new List<string>
                    {
                        "/ImagesServices/142.jpeg",
                    },
                    ExternalLinks = "https://www.annecy-city-sightseeing-tour.com"
                },

                // Activités culturelles
                new Service
                {
                    Id = 143,
                    Name = "Château d'Annecy",
                    Price = 10.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Cultural Activity",
                    Style = "",
                    MaxCapacity = 200,
                    Images = new List<string>
                    {
                        "/ImagesServices/143.jpeg",
                    },
                    ExternalLinks = "https://en.chateau-annecy.com"
                },

                new Service
                {
                    Id = 144,
                    Name = "Palais de l'Isle",
                    Price = 8.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Cultural Activity",
                    Style = "",
                    MaxCapacity = 150,
                    Images = new List<string>
                    {
                        "/ImagesServices/144.jpeg",
                    },
                    ExternalLinks = "https://www.lac-annecy.com"
                },

                new Service
                {
                    Id = 145,
                    Name = "Musée-Château d'Annecy",
                    Price = 12.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Cultural Activity",
                    Style = "",
                    MaxCapacity = 100,
                    Images = new List<string>
                    {
                        "/ImagesServices/145.jpeg",
                    },
                    ExternalLinks = "https://en.chateau-annecy.com"
                },

                // Activités sportives
                new Service
                {
                    Id = 146,
                    Name = "Lake Annecy Watersports",
                    Price = 30.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Sport Activity",
                    Style = "",
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/146.jpeg",
                    },
                    ExternalLinks = "https://www.annecy-watersports.com"
                },

                new Service
                {
                    Id = 147,
                    Name = "Annecy Paragliding",
                    Price = 25.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Annecy",
                    Type = "Sport Activity",
                    Style = "",
                    MaxCapacity = 40,
                    Images = new List<string>
                    {
                        "/ImagesServices/147.jpeg",
                    },
                    ExternalLinks = "https://www.annecy-paragliding.com"
                }

                );


>>>>>>> 29435c6c2af7e8eed44b585d8cc77c1a74648318
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
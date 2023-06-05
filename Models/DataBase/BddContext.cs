using System;
using System.Collections.Generic;
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
using System.IO.Pipelines;
using System.Xml.Linq;

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
            }
            );

            // Création des services
            this.Services.AddRange(

            // Paris:
            new Service
            {
                Id = 1,
                Name = "Hôtel Prince Albert Montmartre",
                Price = 120.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Hébergement",
                Style = null,
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris1Hebergement.png",
                },
                ExternalLinks = "https://www.hotelprincealbert.com/hotel-prince-albert-montmartre",
                DestinationId = 1
            },

            new Service
            {
                Id = 2,
                Name = "Le Village Hostel",
                Price = 35.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Hébergement",
                Style = null,
                MaxCapacity = 1,
                Images = new List<string>
                {
                    "/ImagesTest/Paris2Hebergement.png",
                },
                ExternalLinks = "https://www.villagehostel.fr/",
                DestinationId = 1
            },

            new Service
            {
                Id = 3,
                Name = "La Villa Paris",
                Price = 180.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Hébergement",
                Style = null,
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris3Hebergement.jpg",
                },
                ExternalLinks = "https://www.la-villa-paris.com",
                DestinationId = 1
            },

            new Service
            {
                Id = 4,
                Name = "Hôtel de la Herse d'Or",
                Price = 60.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Hébergement",
                Style = null,
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris4Hebergement.jpg",
                },
                ExternalLinks = "https://www.hotel-herse-dor.com/",
                DestinationId = 1
            },

            new Service
            {
                Id = 5,
                Name = "Le Bristol Paris",
                Price = 200.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Hébergement",
                Style = null,
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris5Hebergement.png",
                },
                ExternalLinks = "https://www.oetkercollection.com/fr/hotels/le-bristol-paris/",
                DestinationId = 1
            },

            new Service
            {
                Id = 6,
                Name = "Le Ptit Bistro",
                Price = 30.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Restauration",
                Style = null,
                MaxCapacity = 4,
                Images = new List<string>
                {
                    "/ImagesTest/Paris1Restauration.png",
                },
                ExternalLinks = "https://lepetitbistrot.eatbu.com/",
                DestinationId = 1
            },

            new Service
            {
                Id = 7,
                Name = "Le Grenier de Notre Dame(Veggie)",
                Price = 25.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Restauration",
                Style = null,
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris2Restauration.png",
                },
                ExternalLinks = "https://www.LeGrenierDeNotreDame.fr",
                DestinationId = 1
            },

            new Service
            {
                Id = 8,
                Name = "La Brasserie Parisienne",
                Price = 60.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Restauration",
                Style = null,
                MaxCapacity = 6,
                Images = new List<string>
                {
                    "/ImagesTest/Paris8Restauration.png",
                },
                ExternalLinks = "https://www.labrasserieparisienne.com",
                DestinationId = 1
            },

            new Service
            {
                Id = 9,
                Name = "Le Bistrot Gourmand",
                Price = 80.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Restauration",
                Style = null,
                MaxCapacity = 4,
                Images = new List<string>
                {
                    "/ImagesTest/Paris9Restauration.jpg",
                },
                ExternalLinks = "https://le-bistrot-gourmand.cover.page/fr",
                DestinationId = 1
            },

            new Service
            {
                Id = 10,
                Name = "Le Ciel de Paris",
                Price = 100.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Restauration",
                Style = null,
                MaxCapacity = 2,
                Images = new List<string>
                {
                    "/ImagesTest/Paris10Restauration.png",
                },
                ExternalLinks = "https://www.cieldeparis.com/",
                DestinationId = 1
            },

            new Service
            {
                Id = 11,
                Name = "Visite du Louvre",
                Price = 15.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Activité",
                Style = "Culture",
                MaxCapacity = 30,
                Images = new List<string>
                {
                    "/ImagesTest/Paris11Activites.png",
                },
                ExternalLinks = "https://www.louvre.fr",
                DestinationId = 1
            },

            new Service
            {
                Id = 12,
                Name = "Spectacle au Moulin Rouge",
                Price = 120.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Activité",
                Style = "Culture",
                MaxCapacity = 100,
                Images = new List<string>
                {
                    "/ImagesTest/Paris12Activites.jpg",
                },
                ExternalLinks = "https://www.moulinrouge.fr",
                DestinationId = 1
            },

            new Service
            {
                Id = 13,
                Name = "Balade en bateau sur la Seine",
                Price = 25.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Paris",
                Type = "Activité",
                Style = "Nature",
                MaxCapacity = 20,
                Images = new List<string>
                {
                    "/ImagesTest/Paris13Activites.jpg",
                },
                ExternalLinks = "https://www.bateaux-mouches.fr",
                DestinationId = 1
            },

            new Service
            {
                Id = 14,
                Name = "Randonnée Paris-Roller",
                Price = 10.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "33 Quai de la Tournelle, 75005 Paris",
                Type = "Activité",
                Style = "Sport",
                MaxCapacity = 50,
                Images = new List<string>
                {
                    "/ImagesTest/Paris14Activites.jpg",
                },
                ExternalLinks = "https://pari-roller.com/",
                DestinationId = 1
            },

            new Service
            {
                Id = 15,
                Name = "Escapade Segway",
                Price = 30.0,
                Schedule = DateTime.Now.AddDays(2),
                Location = "Port de la Gare, 75013 Paris",
                Type = "Activité",
                Style = "Sport",
                MaxCapacity = 6,
                Images = new List<string>
                {
                    "/ImagesTest/Paris15Activites.jpeg",
                },
                ExternalLinks = "https://www.funbooker.com/fr/annonce/segway-tour-a-la-decouverte-de-paris/voir",
                DestinationId = 1
            },

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
                ExternalLinks = "https://www.hostelrome.com",
                DestinationId = 2
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
                ExternalLinks = "https://www.the-yellow.com",
                DestinationId = 2
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
                ExternalLinks = "https://www.hotelderussie.it",
                DestinationId = 2
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
                ExternalLinks = "https://www.hotelartemide.it",
                DestinationId = 2
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
                ExternalLinks = "https://www.raphaelhotel.com",
                DestinationId = 2
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
                ExternalLinks = "https://www.trattoriadadanilo.it",
                DestinationId = 2
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
                ExternalLinks = "https://www.bonci.it",
                DestinationId = 2
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
                ExternalLinks = "https://www.romecavalieri.com/lapergola",
                DestinationId = 2
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
                ExternalLinks = "https://www.armandoalpantheon.it",
                DestinationId = 2
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
                ExternalLinks = "https://www.anticoarco.it",
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
                ExternalLinks = "https://www.hertz.com",
                DestinationId = 2
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
                ExternalLinks = "https://www.avis.com",
                DestinationId = 2
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
                ExternalLinks = "https://www.mydriver.com",
                DestinationId = 2
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
                ExternalLinks = "https://www.romeprivateguides.com",
                DestinationId = 2
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
                ExternalLinks = "https://www.walksofitaly.com",
                DestinationId = 2
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
                ExternalLinks = "https://www.hotelclement.cz",
                DestinationId = 3
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
                ExternalLinks = "https://www.hotelsalvator.cz",
                DestinationId = 3
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
                ExternalLinks = "https://www.hoteljulian.com",
                DestinationId = 3
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
                ExternalLinks = "https://www.mosaichouse.com",
                DestinationId = 3
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
                ExternalLinks = "https://www.hostelone.com",
                DestinationId = 3
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
                ExternalLinks = "https://www.ufleku.cz",
                DestinationId = 3
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
                ExternalLinks = "https://www.czechfolklore.com",
                DestinationId = 3
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
                ExternalLinks = "https://www.lokal-dlouha.ambi.cz",
                DestinationId = 3
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
                ExternalLinks = "https://www.praguelocalguides.com",
                DestinationId = 3
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
                ExternalLinks = "https://www.urbanadventures.com/destination/Prague-tours",
                DestinationId = 3
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
                ExternalLinks = "https://www.funinprague.eu/en/",
                DestinationId = 3
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
                ExternalLinks = "https://www.getpragueguide.com/",
                DestinationId = 3
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
                ExternalLinks = "https://www.premiant.cz/fra/",
                DestinationId = 3
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
                ExternalLinks = "https://mijntours.com/prague/",
                DestinationId = 3
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
                ExternalLinks = "https://www.padlujeme.cz/",
                DestinationId = 3
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
                ExternalLinks = "https://www.rialtohotel.com/",
                DestinationId = 4
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
                ExternalLinks = "https://www.hoteldanieli.com",
                DestinationId = 4
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
                ExternalLinks = "https://www.casagredohotel.com",
                DestinationId = 4
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
                ExternalLinks = "https://www.londrapalace.com",
                DestinationId = 4
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
                ExternalLinks = "https://www.hotelcanalgrande.it",
                DestinationId = 4
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
                ExternalLinks = "https://www.antichecarampane.com",
                DestinationId = 4
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
                ExternalLinks = "http://www.osterialletestiere.it/",
                DestinationId = 4
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
                ExternalLinks = "https://daromano.it/",
                DestinationId = 4
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
                ExternalLinks = "https://www.venicetours.it",
                DestinationId = 4
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
                ExternalLinks = "https://www.gondolaserenade.com",
                DestinationId = 4
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
                ExternalLinks = "https://www.basilicasanmarco.it",
                DestinationId = 4
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
                ExternalLinks = "https://www.gallerieaccademia.it",
                DestinationId = 4
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
                ExternalLinks = "https://thegardenspacevenice.it/",
                DestinationId = 4
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
                ExternalLinks = "https://www.noleggioscooterlido.it/",
                DestinationId = 4
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
                ExternalLinks = "https://www.venicekayak.com/",
                DestinationId = 4
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
                ExternalLinks = "https://www.bayerischerhof.de/en/index.html",
                DestinationId = 5
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
                ExternalLinks = "https://www.kempinski.com/en/hotel-vier-jahreszeiten",
                DestinationId = 5
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
                ExternalLinks = "https://www.hilton.com/en/hotels/mucchtw-hilton-munich-city/",
                DestinationId = 5
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
                ExternalLinks = "https://www.wombats-hostels.com/de/munich",
                DestinationId = 5
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
                ExternalLinks = "https://www.eurostarshotels.co.uk/eurostars-grand-central.html",
                DestinationId = 5
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
                ExternalLinks = "https://www.hofbraeuhaus.de/en/welcome.html",
                DestinationId = 5
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
                ExternalLinks = "https://tantris.de/en/",
                DestinationId = 5
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
                ExternalLinks = "https://www.brennergrill.de/en/",
                DestinationId = 5
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
                ExternalLinks = "https://www.munich.travel/en/",
                DestinationId = 5
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
                ExternalLinks = "https://www.pinakothek-der-moderne.de/en/",
                DestinationId = 5
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
                ExternalLinks = "https://www.mikesbiketours.com/munich/",
                DestinationId = 5
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
                ExternalLinks = "https://www.munichdaytrips.com/en",
                DestinationId = 5
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
                ExternalLinks = "https://www.inmunichtours.com/",
                DestinationId = 5
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
                ExternalLinks = "https://www.munich-wanderland.com/",
                DestinationId = 5
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
                ExternalLinks = "https://www.theritzlondon.com",
                DestinationId = 6
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
                ExternalLinks = "https://www.thesavoylondon.com",
                DestinationId = 6
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
                ExternalLinks = "https://www.claridges.co.uk",
                DestinationId = 6
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
                ExternalLinks = "https://www.dorchestercollection.com/en/london/the-dorchester",
                DestinationId = 6
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
                ExternalLinks = "https://www.shangri-la.com/london/shangrila",
                DestinationId = 6
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
                ExternalLinks = "https://www.theledbury.com",
                DestinationId = 6
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
                ExternalLinks = "https://www.sketch.london",
                DestinationId = 6
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
                ExternalLinks = "https://www.dishoom.com",
                DestinationId = 6
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
                ExternalLinks = "https://www.walks.com/london",
                DestinationId = 6
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
                ExternalLinks = "https://www.britishmuseum.org",
                DestinationId = 6
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
                ExternalLinks = "https://www.hrp.org.uk/tower-of-london",
                DestinationId = 6
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
                ExternalLinks = "https://www.thames-river-cruise.com/it/?msclkid=9dac8ae633d61bc42889ee9ed0ed5606",
                DestinationId = 6
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
                ExternalLinks = "https://www.royalparks.org.uk/parks/hyde-park",
                DestinationId = 6
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
                ExternalLinks = "https://www.daytourslondon.com/half-day-stonehenge-tour-from-london/",
                DestinationId = 6
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
                ExternalLinks = "https://thamesribexperience.com/",
                DestinationId = 6
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
                ExternalLinks = "https://www.nhm.ac.uk/visit.html",
                DestinationId = 6
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
                ExternalLinks = "https://backpackers.gr/",
                DestinationId = 7
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
                ExternalLinks = "https://www.citycircus.gr",
                DestinationId = 7
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
                ExternalLinks = "https://www.athensstudios.gr",
                DestinationId = 7
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
                ExternalLinks = "https://the-student-travellers-inn.athens-greecehotels.com/en/",
                DestinationId = 7
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
                ExternalLinks = "http://hostelsanremoathens.com/",
                DestinationId = 7
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
                ExternalLinks = "https://www.dionysoszonars.gr",
                DestinationId = 7
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
                ExternalLinks = "https://thes.katalogos.menu/#/el",
                DestinationId = 7
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
                ExternalLinks = "https://www.liondi.com/",
                DestinationId = 7
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
                ExternalLinks = "https://www.athenswalkingtours.gr",
                DestinationId = 7
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
                ExternalLinks = "https://www.theacropolismuseum.gr",
                DestinationId = 7
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
                ExternalLinks = "https://www.namuseum.gr",
                DestinationId = 7
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
                ExternalLinks = "https://alldaycruise.net/",
                DestinationId = 7
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
                ExternalLinks = "https://www.athens-walks.com/",
                DestinationId = 7
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
                ExternalLinks = "https://www.withlocals.com/fr/experiences/greece/athens/",
                DestinationId = 7
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
                ExternalLinks = "https://www.yeahhostels.com/barcelona",
                DestinationId = 8
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
                ExternalLinks = "https://www.safestay.com/barcelona-sea",
                DestinationId = 8
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
                ExternalLinks = "https://www.casagraciabcn.com",
                DestinationId = 8
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
                ExternalLinks = "https://www.hotelartsbarcelona.com",
                DestinationId = 8
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
                ExternalLinks = "https://www.marriott.com/hotels/travel/bcnwh-w-barcelona",
                DestinationId = 8
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
                ExternalLinks = "https://congraciarestaurant.com/",
                DestinationId = 8
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
                ExternalLinks = "https://www.cellercanroca.com",
                DestinationId = 8
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
                ExternalLinks = "https://www.disfrutarbarcelona.com",
                DestinationId = 8
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
                ExternalLinks = "https://www.contexttravel.com/cities/barcelona",
                DestinationId = 8
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
                ExternalLinks = "https://www.runnerbeantours.com/barcelona",
                DestinationId = 8
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
                ExternalLinks = "https://sagradafamilia.org",
                DestinationId = 8
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
                ExternalLinks = "https://parkguell.barcelona",
                DestinationId = 8
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
                ExternalLinks = "https://museupicassobcn.cat/",
                DestinationId = 8
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
                ExternalLinks = "https://www.boqueria.barcelona/",
                DestinationId = 8
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
                ExternalLinks = "https://www.buenavistatours.es/",
                DestinationId = 8
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
                ExternalLinks = "https://www.hostelmarmota.com",
                DestinationId = 9
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
                ExternalLinks = "https://www.youth-hostel-innsbruck.at/",
                DestinationId = 9
            },

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
                ExternalLinks = "https://www.hotelinnsbruck.com",
                DestinationId = 9
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
                ExternalLinks = "https://www.marriott.com/hotels/travel/innac-ac-hotel-innsbruck",
                DestinationId = 9
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
                ExternalLinks = "https://www.sitzwohl-innsbruck.at",
                DestinationId = 9
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
                ExternalLinks = "https://www.restaurant-lichtblick.at/",
                DestinationId = 9
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
                ExternalLinks = "https://www.tyroltravel.net/experiences/tour-of-dolomites%2C-alpine-lakes-including-braies",
                DestinationId = 9
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
                ExternalLinks = "https://www.alpenzoo.at",
                DestinationId = 9
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
                ExternalLinks = "https://www.tyroltravel.net/product-page/innsbrucktourdg",
                DestinationId = 9
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
                ExternalLinks = "https://www.innsbruck.gv.at/en/freizeit/kultur/museen-stadtarchiv/museum-goldenes-dachl",
                DestinationId = 9
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
                ExternalLinks = "https://www.burghauptmannschaft.at/Betriebe/Hofburg-Innsbruck.html",
                DestinationId = 9
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
                ExternalLinks = "https://www.bergisel.info",
                DestinationId = 9
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
                ExternalLinks = "https://www.nordkette.com/en",
                DestinationId = 9
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
                ExternalLinks = "https://www.olympiaworld.at",
                DestinationId = 9
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
                ExternalLinks = "https://www.upstreamsurfing.com/",
                DestinationId = 9
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
                Style = null,
                MaxCapacity = 80,
                Images = new List<string>
                {
                    "/ImagesServices/135AnnecyHotel.png",
                },
                ExternalLinks = "https://allobroges.com/hotel-a-annecy/",
                DestinationId = 10
            },

            new Service
            {
                Id = 136,
                Name = "Hotel des Alpes",
                Price = 40.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Accomodation",
                Style = null,
                MaxCapacity = 60,
                Images = new List<string>
                {
                    "/ImagesServices/136AnnecyHotel.png",
                },
                ExternalLinks = "https://www.hotelannecy.com/",
                DestinationId = 10
            },

            new Service
            {
                Id = 137,
                Name = "Rivage Hôtel & Spa",
                Price = 120.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Accomodation",
                Style = null,
                MaxCapacity = 100,
                Images = new List<string>
                {
                    "/ImagesServices/137AnnecyHotel.png",
                },
                ExternalLinks = "https://www.rivage-hotel.com/",
                DestinationId = 10
            },

            new Service
            {
                Id = 138,
                Name = "Hotel du Nord Annecy",
                Price = 60.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Accomodation",
                Style = null,
                MaxCapacity = 80,
                Images = new List<string>
                {
                    "/ImagesServices/138AnnecyHotel.png",
                },
                ExternalLinks = "https://www.annecy-hotel-du-nord.com/",
                DestinationId = 10
            },

            new Service
            {
                Id = 139,
                Name = "L'Esquisse Restaurant - Annecy",
                Price = 80.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Restaurant",
                Style = null,
                MaxCapacity = 50,
                Images = new List<string>
                {
                    "/ImagesServices/139AnnecyRestaurant.jpeg",
                },
                ExternalLinks = "https://esquisse-annecy.fr/fr/",
                DestinationId = 10
            },

            new Service
            {
                Id = 140,
                Name = "Cozna",
                Price = 100.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Restaurant",
                Style = null,
                MaxCapacity = 60,
                Images = new List<string>
                {
                    "/ImagesServices/140AnnecyRestaurant.webp",
                },
                ExternalLinks = "https://restaurantcozna.com/",
                DestinationId = 10
            },

            new Service
            {
                Id = 141,
                Name = "Balade en VTT électrique à Annecy",
                Price = 40.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Activité",
                Style = "Sport" + " " + "Nature",
                MaxCapacity = 15,
                Images = new List<string>
                {
                    "/ImagesServices/141AnnecyActivite.png",
                },
                ExternalLinks = "https://annecy.takamaka.fr/fr/",
                DestinationId = 10
            },

            new Service
            {
                Id = 142,
                Name = "Annecy City Sightseeing Tour",
                Price = 30.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Visite guidée",
                Style = "Culture",
                MaxCapacity = 20,
                Images = new List<string>
                {
                    "/ImagesServices/142AnnecyActivite.png",
                },
                ExternalLinks = "https://annecycitytour.fr/en/",
                DestinationId = 10
            },

            new Service
            {
                Id = 143,
                Name = "Museé-Château d'Annecy",
                Price = 10.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Activité",
                Style = "Culturel",
                MaxCapacity = 200,
                Images = new List<string>
                {
                    "/ImagesServices/143AnnecyActivite.jpeg",
                },
                ExternalLinks = "http://musees.annecy.fr/Ressources-en-ligne",
                DestinationId = 10
            },

            new Service
            {
                Id = 144,
                Name = "Parcours de tyroliennes sur les montagnes du lac d'Annecy",
                Price = 8.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Activité",
                Style = "Sport" + " " + "Nature",
                MaxCapacity = 150,
                Images = new List<string>
                {
                    "/ImagesServices/144AnnecyActivite.jpeg",
                },
                ExternalLinks = "https://www.chemin-des-tyroliennes.com/",
                DestinationId = 10
            },

            new Service
            {
                Id = 145,
                Name = "Société des Régates à Voile d'Annecy",
                Price = 25.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Activité",
                Style = "Sport" + " " + "Nature",
                MaxCapacity = 100,
                Images = new List<string>
                {
                    "/ImagesServices/145AnnecyActivite.png",
                },
                ExternalLinks = "http://www.srva.info/",
                DestinationId = 10
            },

            new Service
            {
                Id = 146,
                Name = "Vol en parapente",
                Price = 30.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Activité",
                Style = "Sport" + " " + "Nature",
                MaxCapacity = 50,
                Images = new List<string>
                {
                    "/ImagesServices/146AnnecyActivite.webp",
                },
                ExternalLinks = "https://www.lespassagersduvent.com/",
                DestinationId = 10
            },

            new Service
            {
                Id = 147,
                Name = "Wakeboard sur le lac",
                Price = 50.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Activité",
                Style = "Sport" + " " + "Nature",
                MaxCapacity = 40,
                Images = new List<string>
                {
                    "/ImagesServices/147AnnecyActivite.png",
                },
                ExternalLinks = "https://ski-wake.com/",
                DestinationId = 10
            },

            new Service
            {
                Id = 148,
                Name = "Cours d'apnée au lac d'Annecy",
                Price = 70.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Activité",
                Style = "Sport" + " " + "Nature",
                MaxCapacity = 40,
                Images = new List<string>
                {
                    "/ImagesServices/148AnnecyActivite.png",
                },
                ExternalLinks = "https://www.planeteapnee.fr/",
                DestinationId = 10
            },

            new Service
            {
                Id = 149,
                Name = "Canyoning à Annecy",
                Price = 80.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Annecy",
                Type = "Activité",
                Style = "Sport" + " " + "Nature",
                MaxCapacity = 40,
                Images = new List<string>
                {
                    "/ImagesServices/149AnnecyActivite.webp",
                },
                ExternalLinks = "https://odyssee-canyon.com/",
                DestinationId = 10
            },

            // Garmisch-Partenkirchen
            new Service
            {
                Id = 150,
                Name = "Hostel 2962 Garmisch",
                Price = 30.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Accommodation",
                Style = null,
                MaxCapacity = 80,
                Images = new List<string>
                {
                    "/ImagesServices/150GarmischHostel.png",
                },
                ExternalLinks = "http://www.hostel2962-garmisch.com/",
                DestinationId = 11
            },

            new Service
            {
                Id = 151,
                Name = "Youth Hostel Lübeck Altstadt",
                Price = 40.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Accommodation",
                Style = null,
                MaxCapacity = 60,
                Images = new List<string>
                {
                    "/ImagesServices/151GarmischHostel.webp",
                },
                ExternalLinks = "https://www.jugendherberge.de/en/youth-hostels/luebeck-altstadt/",
                DestinationId = 11
            },

            new Service
            {
                Id = 152,
                Name = "Mountain Hideaway & Health Care Das Graseck",
                Price = 100.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Accommodation",
                Style = null,
                MaxCapacity = 40,
                Images = new List<string>
                {
                    "/ImagesServices/152GarmischHotel.svg",
                },
                ExternalLinks = "https://www.das-graseck.de/en",
                DestinationId = 11
            },

            new Service
            {
                Id = 153,
                Name = "Hotel Zugspitze",
                Price = 120.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Accommodation",
                Style = null,
                MaxCapacity = 100,
                Images = new List<string>
                {
                    "/ImagesServices/153GarmischHotel.svg",
                },
                ExternalLinks = "https://www.hotel-zugspitze.de",
                DestinationId = 11
            },

            new Service
            {
                Id = 154,
                Name = "Hotel Edelweiss",
                Price = 150.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Accommodation",
                Style = null,
                MaxCapacity = 80,
                Images = new List<string>
                {
                    "/ImagesServices/154GarmischHotel.png",
                },
                ExternalLinks = "https://www.edelweisslodgeandresort.com/",
                DestinationId = 11
            },

            new Service
            {
                Id = 155,
                Name = "Restaurant Gasthof Fraundorfer",
                Price = 50.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Restaurant",
                Style = null,
                MaxCapacity = 50,
                Images = new List<string>
                {
                    "/ImagesServices/155GarmischRestaurant.png",
                },
                ExternalLinks = "https://www.gasthof-fraundorfer.de/Gastronomie",
                DestinationId = 11
            },

            new Service
            {
                Id = 156,
                Name = "Restaurant Husar",
                Price = 100.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Restaurant",
                Style = null,
                MaxCapacity = 60,
                Images = new List<string>
                {
                    "/ImagesServices/156GarmischRestaurant.png",
                },
                ExternalLinks = "http://www.restauranthusar.de/",
                DestinationId = 11
            },

            new Service
            {
                Id = 157,
                Name = "Neuschwanstein Castle Tour",
                Price = 30.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Visite",
                Style = "Culturel",
                MaxCapacity = 15,
                Images = new List<string>
                {
                    "/ImagesServices/157GarmischActivite.jpeg",
                },
                ExternalLinks = "https://www.neuschwanstein.de/englisch/tourist/index.htm",
                DestinationId = 11
            },

            new Service
            {
                Id = 158,
                Name = "Escalade à Garmisch-Partenkirchen",
                Price = 30.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Activité",
                Style = "Sport" + " " + "Nature",
                MaxCapacity = 20,
                Images = new List<string>
                {
                    "/ImagesServices/158GarmischActivite.png",
                },
                ExternalLinks = "https://www.gapa-tourismus.de/bergsteigen",
                DestinationId = 11
            },

            new Service
            {
                Id = 159,
                Name = "Richard Strauss Institute",
                Price = 30.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Visite",
                Style = "Culturel",
                MaxCapacity = 200,
                Images = new List<string>
                {
                    "/ImagesServices/159GarmischActivite.png",
                },
                ExternalLinks = "https://www.gapa.de/en/culture/museum",
                DestinationId = 11
            },

            new Service
            {
                Id = 160,
                Name = "Visite au Werdenfels Museum",
                Price = 4.5,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Visite",
                Style = "Culturel",
                MaxCapacity = 150,
                Images = new List<string>
                {
                    "/ImagesServices/160GarmischActivite.png",
                },
                ExternalLinks = "https://museum-werdenfels.de/",
                DestinationId = 11
            },

            new Service
            {
                Id = 161,
                Name = "Visite à pied de Garmisch-Partenkirchen",
                Price = 60.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Visite guidée",
                Style = "Culture",
                MaxCapacity = 100,
                Images = new List<string>
                {
                    "/ImagesServices/161GarmischActivite.png",
                },
                ExternalLinks = "https://www.allthingsgarmisch.com/",
                DestinationId = 11
            },

            new Service
            {
                Id = 162,
                Name = "Ski a Garmisch-Partenkirchen",
                Price = 80.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Activité",
                Style = "Sport" + " " + "Nature",
                MaxCapacity = 50,
                Images = new List<string>
                {
                    "/ImagesServices/162GarmischActivite.png",
                },
                ExternalLinks = "https://www.gapa-tourismus.de/de/Sport-und-Natur/Winter/Skifahren",
                DestinationId = 11
            },

            new Service
            {
                Id = 163,
                Name = "Hiking in the Bavarian Alps",
                Price = 25.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Activité",
                Style = "Sport" + " " + "Nature",
                MaxCapacity = 40,
                Images = new List<string>
                {
                    "/ImagesServices/163GarmischActivite.png",
                },
                ExternalLinks = "https://www.gapa-tourismus.de/wandern",
                DestinationId = 11
            },

            new Service
            {
                Id = 164,
                Name = "Trail Running Park in the Bavarian Alps",
                Price = 25.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Garmisch-Partenkirchen",
                Type = "Activité",
                Style = "Sport" + " " + "Nature",
                MaxCapacity = 40,
                Images = new List<string>
                {
                    "/ImagesServices/164GarmischActivite.png",
                },
                ExternalLinks = "https://www.gapa-tourismus.de/trailrunning",
                DestinationId = 11
            },
            // Lisbonne :
            new Service
            {
                Id = 165,
                Name = "Lisbon Destination Hostel",
                Price = 25.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Accommodation",
                Style = null,
                MaxCapacity = 100,
                Images = new List<string>
                {
                    "/ImagesServices/165.jpeg",
                },
                ExternalLinks = "https://www.destinationhostels.com/accomodation/lisbon-destination-hostel/",
                DestinationId = 12
            },

            new Service
            {
                Id = 166,
                Name = "Living Lounge Hostel",
                Price = 30.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Accommodation",
                Style = null,
                MaxCapacity = 80,
                Images = new List<string>
                {
                    "/ImagesServices/166LisonneHostel.png",
                },
                ExternalLinks = "https://www.livingloungehostel.com",
                DestinationId = 12
            },

            new Service
            {
                Id = 167,
                Name = "Sunset Destination Hostel",
                Price = 35.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Accommodation",
                Style = null,
                MaxCapacity = 60,
                Images = new List<string>
                {
                    "/ImagesServices/167LisbonneHostel.png",
                },
                ExternalLinks = "https://www.destinationhostels.com/accomodation/sunset-destination-hostel/",
                DestinationId = 12
            },

            new Service
            {
                Id = 168,
                Name = "Hotel Avenida Palace",
                Price = 150.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Accommodation",
                Style = null,
                MaxCapacity = 100,
                Images = new List<string>
                {
                    "/ImagesServices/168LisbonneHotel.png",
                },
                ExternalLinks = "https://www.hotelavenidapalace.pt",
                DestinationId = 12
            },

            new Service
            {
                Id = 169,
                Name = "Altis Avenida Hotel",
                Price = 180.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Accommodation",
                Style = null,
                MaxCapacity = 80,
                Images = new List<string>
                {
                    "/ImagesServices/169LisbonneHotel.png",
                },
                ExternalLinks = "https://www.altishotels.com",
                DestinationId = 12
            },

            new Service
            {
                Id = 170,
                Name = "Belcanto",
                Price = 200.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Restaurant",
                Style = null,
                MaxCapacity = 50,
                Images = new List<string>
                {
                    "/ImagesServices/170LisbonneRestaurant.png",
                },
                ExternalLinks = "https://www.belcanto.pt",
                DestinationId = 12
            },

            new Service
            {
                Id = 171,
                Name = "Time Out Market Lisbon",
                Price = 40.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Restaurant",
                Style = null,
                MaxCapacity = 60,
                Images = new List<string>
                {
                    "/ImagesServices/171LisbonneRestaurant.png",
                },
                ExternalLinks = "https://www.timeoutmarket.com/lisboa/en",
                DestinationId = 12
            },

            new Service
            {
                Id = 172,
                Name = "Belem Tower Guided Tour",
                Price = 9.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Visite",
                Style = "Culturel",
                MaxCapacity = 15,
                Images = new List<string>
                {
                    "/ImagesServices/172LisbonneActivite.png",
                },
                ExternalLinks = "https://torrebelem.com/pt/",
                DestinationId = 12
            },

            new Service
            {
                Id = 173,
                Name = "Visita au Mosteiro dos Jerónimos",
                Price = 25.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Visite",
                Style = "Culturel",
                MaxCapacity = 20,
                Images = new List<string>
                {
                    "/ImagesServices/173LisbonneActivite.jpeg",
                },
                ExternalLinks = "https://www.mosteirojeronimos.com/en/",
                DestinationId = 12
            },

            new Service
            {
                Id = 174,
                Name = "Acquarium de Lisbonne",
                Price = 15.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Cultural Activity",
                Style = "Nature",
                MaxCapacity = 200,
                Images = new List<string>
                {
                    "/ImagesServices/174LisbonneActivite.png",
                },
                ExternalLinks = "https://www.oceanario.pt/en/",
                DestinationId = 12
            },

            new Service
            {
                Id = 175,
                Name = "Musée Nacional des Azulejos",
                Price = 10.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Visite",
                Style = "Culture",
                MaxCapacity = 150,
                Images = new List<string>
                {
                    "/ImagesServices/175LisbonneActivite.png",
                },
                ExternalLinks = "http://www.museudoazulejo.pt/",
                DestinationId = 12
            },

            new Service
            {
                Id = 176,
                Name = "Musée et Fondaction Calouste Gulbenkian",
                Price = 12.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Visite",
                Style = "Culture",
                MaxCapacity = 100,
                Images = new List<string>
                {
                    "/ImagesServices/176LisbonneActivite.jpeg",
                },
                ExternalLinks = "https://gulbenkian.pt/museu/en/",
                DestinationId = 12
            },

            new Service
            {
                Id = 177,
                Name = "Cours de Surf",
                Price = 30.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Activite",
                Style = "Sport",
                MaxCapacity = 50,
                Images = new List<string>
                {
                    "/ImagesServices/177LisbonneActivite.png",
                },
                ExternalLinks = "https://www.lisbonsurfacademy.com",
                DestinationId = 12
            },

            new Service
            {
                Id = 178,
                Name = "Location de kayak",
                Price = 25.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Activite",
                Style = "Sport",
                MaxCapacity = 40,
                Images = new List<string>
                {
                    "/ImagesServices/178LisbonneActivite.webp",
                },
                ExternalLinks = "https://www.lisbonkayak.com/",
                DestinationId = 12
            },

            new Service
            {
                Id = 179,
                Name = "Visite gastronomique inconnue de Lisbonne",
                Price = 94.0,
                Schedule = DateTime.Now.AddDays(1),
                Location = "Lisbon",
                Type = "Activite",
                Style = "Culturel",
                MaxCapacity = 40,
                Images = new List<string>
                {
                    "/ImagesServices/179LisbonneActivite.webp",
                },
                ExternalLinks = "https://www.eatingeurope.com/lisbon/",
                DestinationId = 12
            }

                // Interlaken : Vérifier les chiffres (14 références pour Lisbonne et Interlaken)



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
                Password = UserDAL.EncodeMD5("password"),
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
            },
            new User
            {
                Id = 7,
                LastName = "Rousseau",
                FirstName = "Alexandre",
                Email = "alexandre.rousseau@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
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
                Password = UserDAL.EncodeMD5("password"),
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
                Password = UserDAL.EncodeMD5("password"),
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
                Password = UserDAL.EncodeMD5("password"),
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
                Password = UserDAL.EncodeMD5("password"),
                Address = "12 Rue de la Paix, Quartier Latin, Paris",
                PhoneNumber = "0145678901",
                BirthDate = new DateTime(1985, 8, 20),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 12,
                LastName = "Leroy",
                FirstName = "Sophie",
                Email = "sophie.leroy@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "27 Rue du Palais Grillet, Presqu'île, Lyon",
                PhoneNumber = "046543210",
                BirthDate = new DateTime(1992, 3, 10),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 13,
                LastName = "Martin",
                FirstName = "Pierre",
                Email = "pierre.martin@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "45 Rue des Pénitents Bleus, Vieux Lyon, Lyon",
                PhoneNumber = "0141234564",
                BirthDate = new DateTime(1991, 11, 25),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 14,
                LastName = "Dubois",
                FirstName = "Marie",
                Email = "marie.dubois@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "8 Rue de la République, Part-Dieu, Lyon",
                PhoneNumber = "0149876543",
                BirthDate = new DateTime(1988, 6, 15),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 15,
                LastName = "Dupont",
                FirstName = "Paul",
                Email = "paul.dupont@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "15 Rue de l'Ancienne Porte Neuve, Centre Ville, Perpignan",
                PhoneNumber = "0145678901",
                BirthDate = new DateTime(1995, 2, 18),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 16,
                LastName = "Lefèvre",
                FirstName = "Sophie",
                Email = "sophie.lefevre@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "22 Avenue de Grande Bretagne, Les Coves, Perpignan",
                PhoneNumber = "0410987654",
                BirthDate = new DateTime(1987, 7, 5),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 17,
                LastName = "Moreau",
                FirstName = "Luc",
                Email = "luc.moreau@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "32 Rue Saint-Ferréol, Centre Ville, Marseille",
                PhoneNumber = "0623415678",
                BirthDate = new DateTime(1993, 9, 12),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 18,
                LastName = "Girard",
                FirstName = "Emma",
                Email = "emma.girard@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "9 Quai du Port, Vieux Port, Marseille",
                PhoneNumber = "0168765432",
                BirthDate = new DateTime(1989, 4, 27),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 19,
                LastName = "Dupuis",
                FirstName = "Marc",
                Email = "marc.dupuis@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "23 Rue Esquermoise, Vieux Lille, Lille",
                PhoneNumber = "0131234567",
                BirthDate = new DateTime(1990, 12, 5),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 20,
                LastName = "Lefebvre",
                FirstName = "Julie",
                Email = "julie.lefebvre@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "8 Rue de la Clef, Centre Ville, Lille",
                PhoneNumber = "0139876543",
                BirthDate = new DateTime(1986, 2, 28),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 21,
                LastName = "Roy",
                FirstName = "Sophie",
                Email = "sophie.roy@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "17 Rue Sainte-Catherine, Saint-Michel, Bordeaux",
                PhoneNumber = "0123456789",
                BirthDate = new DateTime(1992, 10, 15),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 22,
                LastName = "Gagnon",
                FirstName = "Alexandre",
                Email = "alexandre.gagnon@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "12 Avenue de la Marne, La Négresse, Biarritz",
                PhoneNumber = "0456789123",
                BirthDate = new DateTime(1988, 5, 2),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 23,
                LastName = "Tremblay",
                FirstName = "Emma",
                Email = "emma.tremblay@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "9 Rue des Francs-Bourgeois, Centre Ville, Strasbourg",
                PhoneNumber = "0498765432",
                BirthDate = new DateTime(1995, 12, 28),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 24,
                LastName = "Lavoie",
                FirstName = "Pierre",
                Email = "pierre.lavoie@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "6 Boulevard des Anglais, Milady, Biarritz",
                PhoneNumber = "0632147859",
                BirthDate = new DateTime(1991, 7, 10),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 25,
                LastName = "Bélanger",
                FirstName = "Marie",
                Email = "marie.belanger@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "15 Rue de la Douane, Centre Ville, Strasbourg",
                PhoneNumber = "0356897412",
                BirthDate = new DateTime(1987, 3, 22),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 26,
                LastName = "Fortin",
                FirstName = "Marc",
                Email = "marc.fortin@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "32 Avenue Jean Médecin, Carré d'Or, Nice",
                PhoneNumber = "0123456789",
                BirthDate = new DateTime(1993, 9, 5),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 27,
                LastName = "Gauthier",
                FirstName = "Isabelle",
                Email = "isabelle.gauthier@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "8 Rue Masséna, Vieux Nice, Nice",
                PhoneNumber = "0456789123",
                BirthDate = new DateTime(1990, 4, 12),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 28,
                LastName = "Morin",
                FirstName = "David",
                Email = "david.morin@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "17 Rue du Maréchal Foch, Petite France, Strasbourg",
                PhoneNumber = "0498765432",
                BirthDate = new DateTime(1986, 11, 25),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 29,
                LastName = "Lévesque",
                FirstName = "Sarah",
                Email = "sarah.levesque@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "6 Avenue Thiers, Jean Médecin, Nice",
                PhoneNumber = "0632147859",
                BirthDate = new DateTime(1994, 2, 18),
                ProfilePicture = "/path/to/profile_picture.jpg"
            },
            new User
            {
                Id = 30,
                LastName = "Caron",
                FirstName = "Thomas",
                Email = "thomas.caron@gmail.com",
                Password = UserDAL.EncodeMD5("password"),
                Address = "15 Rue de la Juiverie, Bouffay, Nantes",
                PhoneNumber = "0132546897",
                BirthDate = new DateTime(1991, 10, 20),
                ProfilePicture = "/path/to/profile_picture.jpg"
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
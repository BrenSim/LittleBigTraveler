﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                    Name = "Spa et bien-être à Munich",
                    Price = 70.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Détente",
                    Style = "",
                    MaxCapacity = 20,
                    Images = new List<string>
                    {
                        "/ImagesServices/71.jpg",
                    },
                    ExternalLinks = "https://www.spa-munich.com"
                },
                new Service
                {
                    Id = 72,
                    Name = "Location de vélos à Munich",
                    Price = 25.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Sport",
                    Style = "",
                    MaxCapacity = 50,
                    Images = new List<string>
                    {
                        "/ImagesServices/72.jpg",
                    },
                    ExternalLinks = "https://www.locationvelosmunich.com"
                },
                new Service
                {
                    Id = 73,
                    Name = "Parc olympique de Munich",
                    Price = 40.0,
                    Schedule = DateTime.Now.AddDays(1),
                    Location = "Munich",
                    Type = "Sport",
                    Style = "",
                    MaxCapacity = 10,
                    Images = new List<string>
                    {
                        "/ImagesServices/73.jpg",
                    },
                    ExternalLinks = "https://www.olympiapark.de"
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
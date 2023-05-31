//using System;
//using System.Linq;
//using System.Collections.Generic;
//using LittleBigTraveler.Models.DataBase;
//using LittleBigTraveler.Models.TravelClasses;
//using LittleBigTraveler.Models.UserClasses;
//using Microsoft.EntityFrameworkCore;

//namespace LittleBigTraveler.Models.DataBase
//{
//    public class TravelDAL : ITravelDAL
//    {
//        private BddContext _bddContext;

//        public TravelDAL()
//        {
//            _bddContext = new BddContext();
//        }

//        public void DeleteCreateDatabase()
//        {
//            _bddContext.Database.EnsureDeleted();
//            _bddContext.Database.EnsureCreated();
//        }

//        public void Dispose()
//        {
//            _bddContext.Dispose();
//        }

//        public int CreateTravel(int customerId, int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, double price, int numParticipants)
//        {
//            var customer = _bddContext.Customers.Include(c => c.User).FirstOrDefault(c => c.Id == customerId);
//            var destination = _bddContext.Destinations.FirstOrDefault(d => d.Id == destinationId);
//            if (customer != null && destination != null)
//            {
//                Travel travel = new Travel()
//                {
//                    Customer = customer,
//                    Destination = destination,
//                    DepartureLocation = departureLocation,
//                    DepartureDate = departureDate,
//                    ReturnDate = returnDate,
//                    Price = price,
//                    NumParticipants = numParticipants
//                };

//                _bddContext.Travels.Add(travel);
//                _bddContext.SaveChanges();

//                return travel.Id;
//            }
//            else
//            {
//                throw new Exception("Invalid customer or destination id.");
//            }
//        }

//        public void DeleteTravel(int id)
//        {
//            var travel = _bddContext.Travels.FirstOrDefault(t => t.Id == id);
//            if (travel != null)
//            {
//                _bddContext.Travels.Remove(travel);
//                _bddContext.SaveChanges();
//            }
//        }

//        public void ModifyTravel(int id, int customerId, int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, double price, int numParticipants)
//        {
//            var travel = _bddContext.Travels.FirstOrDefault(t => t.Id == id);
//            var customer = _bddContext.Customers.Include(c => c.User).FirstOrDefault(c => c.Id == customerId);
//            var destination = _bddContext.Destinations.FirstOrDefault(d => d.Id == destinationId);

//            if (travel != null && customer != null && destination != null)
//            {
//                travel.Customer = customer;
//                travel.Destination = destination;
//                travel.DepartureLocation = departureLocation;
//                travel.DepartureDate = departureDate;
//                travel.ReturnDate = returnDate;
//                travel.Price = price;
//                travel.NumParticipants = numParticipants;

//                _bddContext.SaveChanges();
//            }
//            else
//            {
//                throw new Exception("Invalid travel, customer or destination id.");
//            }
//        }

//        public List<Travel> GetAllTravels()
//        {
//            return _bddContext.Travels
//                .Include(t => t.Customer)
//                    .ThenInclude(c => c.User)
//                .Include(t => t.Destination)
//                .ToList();
//        }

//        public Travel GetTravelWithId(int id)
//        {
//            return _bddContext.Travels
//                .Include(t => t.Customer)
//                    .ThenInclude(c => c.User)
//                .Include(t => t.Destination)
//                .FirstOrDefault(t => t.Id == id);
//        }

//    }
//}
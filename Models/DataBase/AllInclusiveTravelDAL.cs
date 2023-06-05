//using System;
//using System.Linq;
//using System.Collections.Generic;
//using LittleBigTraveler.Models.DataBase;
//using LittleBigTraveler.Models.TravelClasses;
//using LittleBigTraveler.Models.UserClasses;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Http;

//namespace LittleBigTraveler.Models.DataBase
//{
//    public class AllInclusiveTravelDAL : IAllInclusiveTravelDAL
//    {
//        private readonly BddContext _bddContext;
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public AllInclusiveTravelDAL(BddContext bddContext, IHttpContextAccessor httpContextAccessor)
//        {
//            _bddContext = bddContext;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public void Dispose()
//        {
//            _bddContext.Dispose();
//        }

//        // Redéfinition de GetTravelById dans ce DAL pour faciliter la récupération des "Travel" dans le controller
//        public Travel GetTravelById(int travelId)
//        {
//            return _bddContext.Travels
//                .Include(t => t.Customer)
//                    .ThenInclude(c => c.User)
//                .Include(t => t.Destination)
//                .FirstOrDefault(t => t.Id == travelId);
//        }





//        // Récupération de toutes les données "AllInclusiveTravel"
//        public List<AllInclusiveTravel> GetAllInclusiveTravel()
//        {
//            return _bddContext.AllInclusiveTravels.ToList();
//        }

//        // Récupération d'un AllInclusiveTravel par ID
//        public AllInclusiveTravel GetAllInclusiveTravelById(int id)
//        {
//            return _bddContext.AllInclusiveTravels
//                .Include(a => a.Customer)
//                    .ThenInclude(c => c.User)
//                .Include(a => a.Travel)
//                .Include(a => a.ServiceForPackage) 
//                .FirstOrDefault(a => a.Id == id);
//        }

//        // Récupération des AllInclusiveTravel d'un client par son ID
//        public List<AllInclusiveTravel> GetCustomerAllInclusiveTravels(int customerId)
//        {

//            // Récupération de l'ID du client connecté à partir du contexte HTTP
//            customerId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
//            return _bddContext.AllInclusiveTravels
//                .Include(a => a.Customer)
//                    .ThenInclude(c => c.User)
//                .Include(a => a.Travel)
//                .Where(a => a.Customer.Id == customerId)
//                .ToList();
//        }

        

//    }
//}





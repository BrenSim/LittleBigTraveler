//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using LittleBigTraveler.Models.DataBase;
//using LittleBigTraveler.Models.TravelClasses;
//using LittleBigTraveler.ViewModels;
//using Microsoft.AspNetCore.Identity;
//using System.Threading.Tasks;

//namespace LittleBigTraveler.Controllers
//{
//    public class TravelController : Controller
//    {
//        public IActionResult List()
//        {
//            using (var travelDAL = new TravelDAL())
//            {
//                var travels = travelDAL.GetAllTravels();
//                var travelViewModels = MapTravelsToViewModels(travels);
//                return View(travelViewModels);
//            }
//        }

//        // Action pour l'ajout d'un voyage
//        public IActionResult AddTravel()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddTravels(TravelViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                using (var travelDAL = new TravelDAL())
//                {
//                    // Récupérer l'utilisateur actuellement connecté
//                    var user = await _userManager.GetUserAsync(User);
//                    if (user == null)
//                    {
//                        ModelState.AddModelError("", "No user is currently logged in.");
//                        return View("AddTravel", model);
//                    }

//                    // Récupérer le client correspondant à l'utilisateur connecté
//                    var customer = _bddContext.Customers.FirstOrDefault(c => c.UserId == user.Id);
//                    if (customer == null)
//                    {
//                        ModelState.AddModelError("", "The logged-in user is not a customer.");
//                        return View("AddTravel", model);
//                    }

//                    // Récupérer la destination sélectionnée
//                    Destination destination _bddContext.Destinations.Find(model.DestinationId);
//                    if (destination == null)
//                    {
//                        ModelState.AddModelError("", "Invalid destination.");
//                        return View("AddTravel", model);
//                    }

//                    int travelId = travelDAL.CreateTravel(model.DepartureDate, model.ReturnDate, customer, destination, model.Price, model.NumParticipants);
//                    return RedirectToAction("Index", "Home");
//                }
//            }

//            return View("AddTravel", model);
//        }


//        // Action pour la suppression d'un voyage
//        public IActionResult DeleteTravel(int id)
//        {
//            using (var travelDAL = new TravelDAL())
//            {
//                travelDAL.DeleteTravel(id);
//            }

//            return RedirectToAction("List");
//        }

//        // Action pour la modification d'un voyage
//        public IActionResult ChangeTravel(int id)
//        {
//            using (var travelDAL = new TravelDAL())
//            {
//                var travel = travelDAL.GetTravelWithId(id);
//                if (travel == null)
//                {
//                    return NotFound();
//                }

//                var model = MapTravelToViewModel(travel);
//                return View(model);
//            }
//        }

//        [HttpPost]
//        public IActionResult ChangeTravel(int id, TravelViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                using (var travelDAL = new TravelDAL())
//                {
//                    travelDAL.ModifyTravel(
//                        id,
//                        model.Customer,
//                        model.Destination,
//                        model.DepartureLocation,
//                        model.DepartureDate,
//                        model.ReturnDate,
//                        model.Price,
//                        model.NumParticipants
//                    );
//                }
//                return RedirectToAction("List");
//            }

//            return View(model);
//        }



//        public TravelViewModel MapTravelToViewModel(Travel travel)
//        {
//            var model = new TravelViewModel
//            {
//                Id = travel.Id,
//                Customer = travel.Customer,
//                Destination = travel.Destination,
//                DepartureLocation = travel.DepartureLocation,
//                DepartureDate = travel.DepartureDate,
//                ReturnDate = travel.ReturnDate,
//                Price = travel.Price,
//                NumParticipants = travel.NumParticipants
//            };

//            return model;
//        }

//        public List<TravelViewModel> MapTravelsToViewModels(List<Travel> travels)
//        {
//            return travels.Select(MapTravelToViewModel).ToList();
//        }
//    }
//}


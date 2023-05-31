using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.ViewModels;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Controllers
{
    public class TravelController : Controller
    {
        public IActionResult List()
        {
            using (var travelDAL = new TravelDAL())
            {
                var travels = travelDAL.GetAllTravels();
                return View(travels);
            }
        }

        public IActionResult AddTravel()
        {
            using (var destinationDAL = new DestinationDAL())
            {
                var destinations = destinationDAL.GetAllDestinations();
                var customers = new List<Customer>(); // Mettez ici votre logique pour récupérer les clients

                var model = new TravelViewModel
                {
                    Destinations = destinations,
                    Customers = customers
                };

                return View(model);
            }
        }

        [HttpPost]
        public IActionResult AddTravel(TravelViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var travelDAL = new TravelDAL())
                {
                    int travelId = travelDAL.CreateTravel(model.CustomerId, model.DestinationId, model.DepartureLocation, model.DepartureDate, model.ReturnDate, model.Price, model.NumParticipants);
                    return RedirectToAction("Index", "Home");
                }
            }

            using (var destinationDAL = new DestinationDAL())
            {
                var destinations = destinationDAL.GetAllDestinations();
                var customers = new List<Customer>(); // Mettez ici votre logique pour récupérer les clients

                model.Destinations = destinations;
                model.Customers = customers;

                return View("AddTravel", model);
            }
        }

        public IActionResult DeleteTravel(int id)
        {
            using (var travelDAL = new TravelDAL())
            {
                travelDAL.DeleteTravel(id);
            }

            return RedirectToAction("List");
        }

        public IActionResult EditTravel(int id)
        {
            using (var travelDAL = new TravelDAL())
            {
                var travel = travelDAL.GetTravelWithId(id);
                if (travel == null)
                {
                    return NotFound();
                }

                using (var destinationDAL = new DestinationDAL())
                {
                    var destinations = destinationDAL.GetAllDestinations();
                    var customers = new List<Customer>(); // Mettez ici votre logique pour récupérer les clients

                    var model = new TravelViewModel
                    {
                        Id = travel.Id,
                        CustomerId = travel.CustomerId,
                        DestinationId = travel.DestinationId,
                        DepartureLocation = travel.DepartureLocation,
                        DepartureDate = travel.DepartureDate,
                        ReturnDate = travel.ReturnDate,
                        Price = travel.Price,
                        NumParticipants = travel.NumParticipants,
                        Destinations = destinations,
                        Customers = customers
                    };

                    return View(model);
                }
            }
        }

        [HttpPost]
        public IActionResult EditTravel(int id, TravelViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var travelDAL = new TravelDAL())
                {
                    var travel = new Travel
                    {
                        Id = id,
                        CustomerId = model.CustomerId,
                        DestinationId = model.DestinationId,
                        DepartureLocation = model.DepartureLocation,
                        DepartureDate = model.DepartureDate,
                        ReturnDate = model.ReturnDate,
                        Price = model.Price,
                        NumParticipants = model.NumParticipants
                    };

                    travelDAL.ModifyTravel(travel);
                }

                return RedirectToAction("List");
            }

            using (var destinationDAL = new DestinationDAL())
            {
                var destinations = destinationDAL.GetAllDestinations();
                var customers = new List<Customer>(); // Mettez ici votre logique pour récupérer les clients
                model.Destinations = destinations;
                model.Customers = customers;

                return View("EditTravel", model);
            }
        }

        // Autres actions du contrôleur...

    }
}
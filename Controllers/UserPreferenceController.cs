using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.Models;
using LittleBigTraveler.Models.DataBase;
using System.Security.Claims;

namespace LittleBigTraveler.Controllers
{
    [Authorize]
    public class UserPreferenceController : Controller
    {
        private IUserPreferenceDAL _userPreferenceDAL;
        private IDestinationDAL _destinationDAL;
        private IServiceDAL _serviceDAL;

        public UserPreferenceController(IUserPreferenceDAL userPreferenceDAL, IDestinationDAL destinationDAL, IServiceDAL serviceDAL)
        {
            _userPreferenceDAL = userPreferenceDAL;
            _destinationDAL = destinationDAL;
            _serviceDAL = serviceDAL;
        }

        // Récupération de l'ID de l'utilisateur connecté
        private int GetUserId()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var userIdString = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdString, out int userId))
                {
                    return userId;
                }
            }

            throw new Exception("L'utilisateur n'est pas authentifié ou l'ID de l'utilisateur n'est pas valide.");
        }

        // Ajout d'une destination préférée
        [HttpPost]
        public IActionResult AddPreferredDestination(int destinationId)
        {
            try
            {
                int userId = GetUserId();
                _userPreferenceDAL.AddPreferredDestination(userId, destinationId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erreur lors de l'ajout de la destination préférée : " + ex.Message);
            }
        }

        // Ajout d'un service préféré
        [HttpPost]
        public IActionResult AddPreferredService(int serviceId)
        {
            try
            {
                int userId = GetUserId();
                _userPreferenceDAL.AddPreferredService(userId, serviceId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erreur lors de l'ajout du service préféré : " + ex.Message);
            }
        }

        // Suppression d'une destination préférée
        [HttpDelete]
        public IActionResult RemovePreferredDestination(int destinationId)
        {
            try
            {
                int userId = GetUserId();
                _userPreferenceDAL.RemovePreferredDestination(userId, destinationId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erreur lors de la suppression de la destination préférée : " + ex.Message);
            }
        }

        // Suppression d'un service préféré
        [HttpDelete]
        public IActionResult RemovePreferredService(int serviceId)
        {
            try
            {
                int userId = GetUserId();
                _userPreferenceDAL.RemovePreferredService(userId, serviceId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Erreur lors de la suppression du service préféré : " + ex.Message);
            }
        }

        // Récupération des préférences de l'utilisateur
        [HttpGet]
        public IActionResult GetUserPreferences()
        {
            try
            {
                int userId = GetUserId();
                var userPreferences = _userPreferenceDAL.GetUserPreferences(userId);
                return Ok(userPreferences);
            }
            catch (Exception ex)
            {
                return BadRequest("Erreur lors de la récupération des préférences de l'utilisateur : " + ex.Message);
            }
        }
    }
}

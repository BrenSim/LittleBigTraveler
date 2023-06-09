using System;
using System.Collections.Generic;
using System.Linq;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;

using Microsoft.EntityFrameworkCore;

using static System.Net.Mime.MediaTypeNames;

namespace LittleBigTraveler.Models.DataBase
{

// Data access layer général (initialisation de la database)

	public class Dal : IDal
	{

        private BddContext _bddContext;

        public Dal()
        {
            _bddContext = new BddContext();
        }

        // Supression/Création de la database (méthode appelé dans BddContext)
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}


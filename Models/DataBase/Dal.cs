using System;
using System.Collections.Generic;
using System.Linq;
namespace LittleBigTraveler.Models.DataBase
{
	public class Dal : IDal
	{
        private BddContext _bddContext;

        public Dal()
        {
            _bddContext = new BddContext();
        }

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


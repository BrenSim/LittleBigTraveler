using System;
using LittleBigTraveler.Models.TravelClasses;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.DataBase
{
	public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();
        //List<Destination> ObtientToutesDestination();
        //int CreerDestination(string city, string country, string description, string style, string image, string link);
        //void SupprimerDestination(int Id);
        //void ModifierDestination(int Id, string city, string country, string description, string style, string image, string link);
        //Destination ObtientDestinationParId(int Id);
    }
}


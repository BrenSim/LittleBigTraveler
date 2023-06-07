using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using static System.Net.Mime.MediaTypeNames;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();

    }
}


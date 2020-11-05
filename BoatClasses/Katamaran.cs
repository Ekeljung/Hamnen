using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    class Katamaran : Boat
    {
        public Katamaran()
        {
            //int beds = Rnd(0, 501);
            string randCargoId = RndString(3);
            Type = "Katamaran";
            Id = $"K-{randCargoId}";
            Weight = Rnd(1200, 8000);
            MaxSpeed = Rnd(1, 12);
            DaysToStay = 3;
            ParkingSpaceNeeded = 4;
            Unique = Rnd(0, 501);       //Beds
            Berth = 0;
            FullName = $"Katamaran";
        }

        public override string BoatInfo()
        {
            return $"{Berth}-{Berth + 3}\t\t{Type,-5}\t{Id,6}\t\t{Utilities.KnotsToKmh(MaxSpeed),4} Km/h\t\t{Weight,5} kg\t\t{Unique} {(Unique > 1 ? "Beds" : "Bed")}";
        }
    }
}

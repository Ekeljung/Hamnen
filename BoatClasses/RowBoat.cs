using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    class RowBoat : Boat
    {
        public RowBoat()
        {
            //int passenger = Rnd(1, 7);
            string randRowId = RndString(3);
            Type = "Row";
            Id = $"R-{randRowId}";
            Weight = Rnd(100, 301);
            MaxSpeed = Rnd(1, 4);
            DaysToStay = 1;
            ParkingSpaceNeeded = 1;
            Unique = Rnd(1, 7);         //Passengers
            Berth = 0;
            FullName = $"Rowing boat";
        }


        public override string BoatInfo()
        {
            return $"{Berth}\t\t{Type,-5}\t\t{Id,6}\t\t{Utilities.KnotsToKmh(MaxSpeed),4} Km/h\t\t{Weight,5} kg\t\t{Unique} {(Unique > 1 ? "passengers" : "passenger")}";
        }
    }
}

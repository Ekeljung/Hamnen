using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    class SailBoat : Boat
    {

        public SailBoat()
        {
            //int length = Rnd(10, 61);
            string randSailId = RndString(3);
            Type = "Sail";
            Id = $"S-{randSailId}";
            Weight = Rnd(800, 6001);
            MaxSpeed = Rnd(1, 13);
            DaysToStay = 4;
            ParkingSpaceNeeded = 2;
            Unique = Rnd(10, 61);       //Lenght
            Berth = 0;
            FullName = $"Sailing ship";
        }

        public override string BoatInfo()
        {
            return $"{Berth}-{Berth + 1}\t\t{Type,-5}\t\t{Id,6}\t\t{Utilities.KnotsToKmh(MaxSpeed),4} Km/h\t\t{Weight,5} kg\t\t{Unique} Meters long";
        }
    }
}

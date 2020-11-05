using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    class CargoShip : Boat
    {

        public CargoShip()
        {
            //int containers = Rnd(0, 501);
            string randCargoId = RndString(3);
            Type = "Cargo";
            Id = $"C-{randCargoId}";
            Weight = Rnd(3000, 20001);
            MaxSpeed = Rnd(1, 21);
            DaysToStay = 6;
            ParkingSpaceNeeded = 4;
            Unique = Rnd(0, 501);       //Containers
            Berth = 0;
            FullName = $"Cargo ship";
        }

        public override string BoatInfo()
        {
            if (Weight > 10000)
                return $"{Berth}-{Berth + 3}\t\t{Type,-5}\t\t{Id,6}\t\t{Utilities.KnotsToKmh(MaxSpeed),4} Km/h\t\t{Weight,3} kg\t\t{Unique} Containers";

            else
                return $"{Berth}-{Berth + 3}\t\t{Type,-5}\t\t{Id,6}\t\t{Utilities.KnotsToKmh(MaxSpeed),4} Km/h\t\t{Weight,5} kg\t\t{Unique} Containers";

        }
    }
}

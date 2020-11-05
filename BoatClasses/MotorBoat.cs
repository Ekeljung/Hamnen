using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    class MotorBoat : Boat
    {

        public MotorBoat()
        {
            //int horsepower = Rnd(10, 1001);
            string randMotorId = RndString(3);
            Type = "Motor";
            Id = $"M-{randMotorId}";
            Weight = Rnd(200, 3001);
            MaxSpeed = Rnd(1, 61);
            DaysToStay = 3;
            ParkingSpaceNeeded = 1;
            Unique = Rnd(10, 1001);     //Horsepower
            Berth = 0;
            FullName = $"Motor boat";
        }

        public override string BoatInfo()
        {
            if (MaxSpeed > 100 && Weight > 1000)
                return $"{Berth}\t\t{Type,-5}\t\t{Id,6}\t\t{Utilities.KnotsToKmh(MaxSpeed),4} Km/h\t{Weight,5} kg\t{Unique} Hp";
            else
                return $"{Berth}\t\t{Type,-5}\t\t{Id,6}\t\t{Utilities.KnotsToKmh(MaxSpeed),4} Km/h\t\t{Weight,5} kg\t\t{Unique} Hp";
        }
    }
}

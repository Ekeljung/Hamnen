using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    abstract class Boat     //Abstract. Base class
    {
        public string Id { get; set; }
        public int Berth { get; set; }
        public string Type { get; set; }
        public int Weight { get; set; }
        public int DaysToStay { get; set; }
        public int MaxSpeed { get; set; }
        public int Unique { get; set; }

        public int ParkingSpaceNeeded { get; set; }
        public string FullName { get; set; }
        public bool ParkingAvailable { get; set; }


        public virtual string BoatInfo()
        {
            return "THIS WILL NEVER BE PRINTED!";
        }


        static Random rnd = new Random();


        /// <summary>
        /// Enter a min and a maxvalue. Returns a random between min and -1 from max.
        /// </summary>
        /// <param name="minValue">Lowest value</param>
        /// <param name="maxValue">Highest value, add 1 to your prefered max</param>
        /// <returns></returns>
        public int Rnd(int minValue, int maxValue)
        {
            int value = rnd.Next(minValue, maxValue);
            return value;
        }


        /// <summary>
        /// Get random letters from alphabet
        /// </summary>
        /// <param name="quantity">Number of letter that you want</param>
        /// <returns></returns>
        public string RndString(int quantity)
        {
            string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string value = "";
            for (int i = 0; i < quantity; i++)
            {
                int index = rnd.Next(0, 25);
                value += abc[index];
            }
            return value;
        }
    }
}

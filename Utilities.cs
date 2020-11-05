using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hamnen
{
    class Utilities
    {
        const string fileName = "Harbour.txt";


        /// <summary>
        /// Kinda self explanatory. Convert knots (air/sea travel speed measurement) to metric (kilometers per hour)
        /// </summary>
        /// <param name="speed">Variabel to convert</param>
        /// <returns></returns>
        internal static double KnotsToKmh(double speed)
        {
            return Math.Round(speed * 1.852);
        }


        /// <summary>
        /// Convert foot (imperial) to meters (metric)
        /// </summary>
        /// <param name="foot">Variabel to convert</param>
        /// <returns></returns>
        internal static int FootToMeter(int foot)
        {
            return (int)(foot * 0.3048);    //Ändra till Math.Round?
        }


        /// <summary>
        /// Save our boats in dock to file
        /// </summary>
        /// <param name="harbour2">Our array</param>
        internal static void SaveData(DockPlace[] harbour2)
        {
            StreamWriter sw = new StreamWriter(fileName, false);    //true to append data to the file; false to overwrite the file
            foreach (var boat3 in harbour2)
            {
                if (boat3.dockPlace[0] != null)
                {
                    sw.WriteLine(
                    $"{boat3.dockPlace[0].Id};" +
                    $"{boat3.dockPlace[0].Berth};" +
                    $"{boat3.dockPlace[0].Type};" +
                    $"{boat3.dockPlace[0].Weight};" +
                    $"{boat3.dockPlace[0].DaysToStay};" +
                    $"{boat3.dockPlace[0].MaxSpeed};" +
                    $"{boat3.dockPlace[0].Unique}");
                }
                if (boat3.dockPlace[1] != null)
                {
                    sw.WriteLine(
                    $"{boat3.dockPlace[1].Id};" +
                    $"{boat3.dockPlace[1].Berth};" +
                    $"{boat3.dockPlace[1].Type};" +
                    $"{boat3.dockPlace[1].Weight};" +
                    $"{boat3.dockPlace[1].DaysToStay};" +
                    $"{boat3.dockPlace[1].MaxSpeed};" +
                    $"{boat3.dockPlace[1].Unique}");
                }
            }
            sw.Close();
        }


        /// <summary>
        /// Check our file and load the data
        /// </summary>
        /// <param name="harbour3">Our array</param>
        internal static void LoadFile(DockPlace[] harbour3)
        {
            foreach (var boat4 in File.ReadLines(fileName, System.Text.Encoding.UTF8))
            {
                string[] boatdata = boat4.Split(';');

                for (int i = 0; i < boatdata.Length; i++)
                {
                    switch (boatdata[0].First())
                    {
                        case 'R':
                            RowBoat r = new RowBoat
                            {
                                Id = boatdata[0],
                                Berth = int.Parse(boatdata[1]),
                                Type = boatdata[2],
                                Weight = int.Parse(boatdata[3]),
                                DaysToStay = int.Parse(boatdata[4]),
                                MaxSpeed = int.Parse(boatdata[5]),
                                Unique = int.Parse(boatdata[6])     //Passenger
                            };
                            LoadBoat(r, harbour3);
                            break;
                        case 'M':
                            MotorBoat m = new MotorBoat
                            {
                                Id = boatdata[0],
                                Berth = int.Parse(boatdata[1]),
                                Type = boatdata[2],
                                Weight = int.Parse(boatdata[3]),
                                DaysToStay = int.Parse(boatdata[4]),
                                MaxSpeed = int.Parse(boatdata[5]),
                                Unique = int.Parse(boatdata[6])     //Horsepower
                            };
                            LoadBoat(m, harbour3);
                            break;
                        case 'S':
                            SailBoat s = new SailBoat
                            {
                                Id = boatdata[0],
                                Berth = int.Parse(boatdata[1]),
                                Type = boatdata[2],
                                Weight = int.Parse(boatdata[3]),
                                DaysToStay = int.Parse(boatdata[4]),
                                MaxSpeed = int.Parse(boatdata[5]),
                                Unique = int.Parse(boatdata[6])     //Lenght
                            };
                            LoadBoat(s, harbour3);
                            break;
                        case 'C':
                            CargoShip c = new CargoShip
                            {
                                Id = boatdata[0],
                                Berth = int.Parse(boatdata[1]),
                                Type = boatdata[2],
                                Weight = int.Parse(boatdata[3]),
                                DaysToStay = int.Parse(boatdata[4]),
                                MaxSpeed = int.Parse(boatdata[5]),
                                Unique = int.Parse(boatdata[6])     //Containers
                            };
                            LoadBoat(c, harbour3);
                            break;
                        case 'K':
                            Katamaran k = new Katamaran
                            {
                                Id = boatdata[0],
                                Berth = int.Parse(boatdata[1]),
                                Type = boatdata[2],
                                Weight = int.Parse(boatdata[3]),
                                DaysToStay = int.Parse(boatdata[4]),
                                MaxSpeed = int.Parse(boatdata[5]),
                                Unique = int.Parse(boatdata[6])     //Beds
                            };
                            LoadBoat(k, harbour3);
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Check our file, load the data and put them into our dock
        /// </summary>
        /// <param name="b"></param>
        /// <param name="harbour4"></param>
        internal static void LoadBoat(Boat b, DockPlace[] harbour4)
        {
            for (int i = 0; i < harbour4.Length; i++)
            {
                if (b is RowBoat)
                {
                    if (b.Berth == harbour4[i].InnerBertNumber && harbour4[i].Available == true)
                    {
                        harbour4[i].dockPlace[0] = b;
                        harbour4[i].Available = false;
                        break;
                    }
                    if (harbour4[i].dockPlace[0] is RowBoat && harbour4[i].dockPlace[1] is null && harbour4[i].dockPlace[0].Id != b.Id)
                    {
                        harbour4[i].dockPlace[1] = b;
                        break;
                    }
                }

                if (b is MotorBoat)
                {
                    if (b.Berth == harbour4[i].InnerBertNumber && harbour4[i].Available == true)
                    {
                        harbour4[i].dockPlace[0] = b;
                        harbour4[i].Available = false;
                        break;
                    }
                }

                if (b is SailBoat)
                {
                    if (b.Berth == harbour4[i].InnerBertNumber)
                    {
                        harbour4[i].dockPlace[0] = b;
                        harbour4[i].Available = false;
                        harbour4[i + 1].Available = false;
                        break;
                    }
                }

                if (b is CargoShip)
                {
                    if (b.Berth == harbour4[i].InnerBertNumber)
                    {
                        harbour4[i].dockPlace[0] = b;
                        harbour4[i].Available = false;
                        harbour4[i + 1].Available = false;
                        harbour4[i + 2].Available = false;
                        harbour4[i + 3].Available = false;
                        break;
                    }
                }

                if (b is Katamaran)
                {
                    if (b.Berth == harbour4[i].InnerBertNumber)
                    {
                        harbour4[i].dockPlace[0] = b;
                        harbour4[i].Available = false;
                        harbour4[i + 1].Available = false;
                        harbour4[i + 2].Available = false;
                        break;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Hamnen
{
    class Program
    {
        static bool run = true;
        static int day = -1;
        static Random rnd = new Random();

        public static DockPlace[] harbour = new DockPlace[64];  //Our harbor. Each element get a array of 'Boat[] dockPlace' with the size of 2.
        static List<Boat> newBoats = new List<Boat>();          //List of boats thats being created each day
        static List<Boat> rejectedBoats = new List<Boat>();     //Collection of rejected boats

        //Get the path to "\bin\Debug\netcoreapp3.1" on the computer. This is where we save/load our file
        static string filePath = Directory.GetParent(typeof(Program).Assembly.Location).FullName;
        static string filePathFull = filePath + @"\Harbour.txt";



        static void Main(string[] args)
        {
            Thread.Sleep(2000);
            CreateDock();

            Utilities.LoadFile(harbour);    //Load our file, if there is one

            while (run)
            {
                day++;
                IncomingBoats(5);
                DockBoats(newBoats);

                PrintBoats();
                MenuChoice();
                Thread.Sleep(300);
            }
        }


        /// <summary>
        /// Create dock with some standard values
        /// </summary>
        private static void CreateDock()
        {
            for (int i = 0; i < harbour.Length; i++)
            {
                if (harbour[i] == null)
                {
                    harbour[i] = new DockPlace();
                    harbour[i].Available = true;
                    harbour[i].InnerBertNumber = (i + 1);
                }
            }

            if (File.Exists(filePathFull))
            {
                BootScreen();

                Thread.Sleep(400);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nFile exists...");
                Console.BackgroundColor = ConsoleColor.Black;
                Thread.Sleep(400);
                Console.WriteLine("Reading...");
                Thread.Sleep(400);
                Console.WriteLine();

                BootScreen2();
            }
            else
            {
                BootScreen();

                Thread.Sleep(400);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nFile does not exist in the current directory!");
                Console.BackgroundColor = ConsoleColor.Black;
                Thread.Sleep(rnd.Next(400, 700));
                Console.WriteLine("Creating file...");
                Thread.Sleep(rnd.Next(600, 1200));
                using (var stream = File.Create(filePathFull)) { }
                Console.WriteLine("SUCCESSFUL.");
                Console.WriteLine();

                BootScreen2();
            }
        }


        /// <summary>
        /// Controller to reset the 'rejected boats' counter or end the program
        /// </summary>
        public static void MenuChoice()
        {
            //Any button except 'Q' and 'R' continous the loop
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.R:          //Clear our 'rejected boat' list to start from zero
                    rejectedBoats.Clear();
                    break;
                case ConsoleKey.Q:
                    ExitScreen();
                    run = false;
                    break;
                case ConsoleKey.Enter:
                    break;
                default:
                    Console.WriteLine("");
                    Console.Clear();
                    break;
            }
        }


        /// <summary>
        /// Some UI that shows when program is starting
        /// </summary>
        public static void BootScreen()
        {
            string dots = "...";

            Console.WriteLine("PhoenixBIOS 4.0 Release 6.0");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine("Copyright 1985-2000 Phoenix Technologies Ltd.");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine("All Rights Reserved");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine("\nBIOS version 29.02");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine("Gateway Solo 9550");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine("System ID = 00250850");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine($"\nBuild Time; {DateTime.Now}");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine($"CPU = Intel(R) Mobile Pentium(R) III Processor-M 1066 MHz\n");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine($"639 KB System RAM Passed");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine($"254M Extended RAM Passed");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine($"512K Cache SRAM Passed");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine($"System BIOS shadowed");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine($"Video BIOS  shadows");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine($"UMB upper limit segment address: E446\n");
            Thread.Sleep(1000);

            Console.WriteLine("Locating directory...");
            Thread.Sleep(350);
            Console.WriteLine("Looking for file...");
            Thread.Sleep(200);
            Console.WriteLine(filePathFull);
            Thread.Sleep(500);
            
            foreach (var dot in dots)
            {
                Console.Write(dot);
                Thread.Sleep(500);
            }

            Console.WriteLine();
        }


        /// <summary>
        /// Some UI that shows when program is starting, LOGO.
        /// </summary>
        public static void BootScreen2()
        {
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine("Starting program:");
            Console.WriteLine(@"  _   _    _    ____  ____   ___  _   _ ____  ");
            Console.WriteLine(@" | | | |  / \  | _  \| __ ) / _ \| | | |  _ \ ");
            Console.WriteLine(@" | |_| | / _ \ | |_) |  _ \| | | | | | | |_) | ");
            Console.WriteLine(@" |  _  |/ ___ \|  _ <| |_) | |_| | |_| |  _ < ");
            Console.WriteLine(@" |_| |_/_/   \_\_| \_\____/ \___/ \___/|_| \_\");
            Thread.Sleep(1100);
        }


        /// <summary>
        /// Some UI that shows when program is ending
        /// </summary>
        public static void ExitScreen()
        {
            Console.Clear();
            Console.WriteLine("Program is shutting down");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine("\nApplicationname: Harbour");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine("Kill process");
            Thread.Sleep(rnd.Next(700, 1200));
            Console.WriteLine("Version 29.02");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine("System ID = 00250850");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine($"\nBuild Time; {DateTime.Now}");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine("Beginning dump of physical memory");
            Thread.Sleep(rnd.Next(200, 700));
            Console.WriteLine("Physical memory dump complete");
            Thread.Sleep(rnd.Next(700, 1200));

            Console.WriteLine("\nAll files saved successfully");
        }


        /// <summary>
        /// Randomly make new boats
        /// </summary>
        /// <param name="NumbOfNewBoats">Change this to control number of new boats</param>
        private static void IncomingBoats(int NumbOfNewBoats)
        {
            for (int i = 0; i < NumbOfNewBoats; i++)
            {
                Thread.Sleep(6);
                int newBoat = rnd.Next(1, 6);
                switch (newBoat)
                {
                    case 1:
                        newBoats.Add(new RowBoat()); 
                        //Boat rowboat = new RowBoat();
                        //newBoats.Add(rowboat);
                        break;
                    case 2:
                        newBoats.Add(new MotorBoat());
                        break;
                    case 3:
                        newBoats.Add(new SailBoat());
                        break;
                    case 4:
                        newBoats.Add(new CargoShip());
                        break;
                    case 5:
                        newBoats.Add(new Katamaran());
                        break;
                }
            }
        }


        /// <summary>
        /// The visual to show the boats and other information on screen
        /// </summary>
        public static void PrintBoats()
        {
            List<Boat> listOfBoats = new List<Boat>();
            Console.Clear();

            for (int i = 0; i < harbour.Length; i++)
            {
                if (harbour[i].dockPlace[0] != null)
                {
                    listOfBoats.Add(harbour[i].dockPlace[0]);
                }
                if (harbour[i].dockPlace[1] != null)
                {
                    listOfBoats.Add(harbour[i].dockPlace[1]);
                }
            }

            var qWeight = listOfBoats
                .Where(b => b != null)
                .Select(b => b.Weight)
                .Sum();

            var qAvgSpd = listOfBoats
                .Where(b => b != null)
                .Select(b => b.MaxSpeed)
                .Average();

            var q = listOfBoats
                .Select(b => b.MaxSpeed)
                .Average();

            var q3 = listOfBoats
                .Where(b => b != null)
                .GroupBy(b => b.FullName);

            var qFreeSpace = harbour
                .Where(b => b.Available)
                .Count();

            //Some code for UI
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("The Harbour");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"  Available: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(qFreeSpace);

            if (qFreeSpace > 9)
                Console.Write(" ");
            else
                Console.Write("  ");

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  Rejected: ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(rejectedBoats.Count());

            if (rejectedBoats.Count() > 10)
                Console.Write("  ");
            else if (rejectedBoats.Count >= 10)
                Console.Write("  ");
            else
                Console.Write("   ");

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("  Avg Speed: ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"{Utilities.KnotsToKmh(qAvgSpd)} Km/h");
            Console.ForegroundColor = ConsoleColor.White;

            if (qAvgSpd == 0)
                Console.Write("  ");
            else if (qAvgSpd < 10)
                Console.Write("   ");
            else if (qAvgSpd == 4)
                Console.Write("  ");
            else if (qAvgSpd == 10)
                Console.Write("   ");
            else if (qAvgSpd == 18)
                Console.Write("  ");
            else if (qAvgSpd > 33)
                Console.Write("  ");
            else
                Console.Write("  ");

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;

            Console.Write("  Total Weight: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{qWeight} Kg");

            if (qWeight < 10000)
            {
                Console.Write("   ");
            }
            else if (qWeight > 10000)
            {
                Console.Write("  ");
            }
            else if (qWeight > 100000)
            {
                Console.Write(" ");
            }

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("  Days since restart: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(day);

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("~~~");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Press 'Q' to quit or 'R' to reset.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");


            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Space\t\tType\t\tIdentity\t Max speed\t\t  Weight\t\tOther");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;


            //Print boats. Both empty and occupied
            for (int i = 0; i < harbour.Length; i++)
            {
                if (harbour[i].Available == true)
                    if (harbour[i].InnerBertNumber < 10)
                        Console.WriteLine($"{harbour[i].InnerBertNumber}      TOMT     -\t\t -\t\t\t-\t\t       -\t\t-");
                    else
                        Console.WriteLine($"{harbour[i].InnerBertNumber}     TOMT     -\t\t -\t\t\t-\t\t       -\t\t-");

                if (harbour[i].Available == false)
                {
                    if (harbour[i].dockPlace[0] != null)
                    {

                        Console.WriteLine($"{harbour[i].dockPlace[0].BoatInfo()}");
                    }
                    if (harbour[i].dockPlace[1] != null)
                        Console.WriteLine($"{harbour[i].dockPlace[1].BoatInfo()}");
                }
            }


            //Info about boats in harbour
            int qOccupiedSpace = 0;
            foreach (var item in q3)
            {
                qOccupiedSpace += item.Count();
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"Boats in harbour {qOccupiedSpace} ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var item in q3)
            {
                Console.Write($"\n{item.Key}: {item.Count()} ");
            }
        }


        /// <summary>
        /// Loop through current boats in harbour to see when they're leaving
        /// </summary>
        private static void LeavingBoats()
        {
            for (int i = 0; i < harbour.Length; i++)
            {
                if (harbour[i].dockPlace[0] is Katamaran)
                {

                    harbour[i].dockPlace[0].DaysToStay--;
                    if (harbour[i].dockPlace[0].DaysToStay == 0)
                    {
                        harbour[i].dockPlace[0] = null;
                        harbour[i].Available = true;
                        harbour[i + 1].Available = true;
                        harbour[i + 2].Available = true;
                    }
                }
                if (harbour[i].dockPlace[0] is CargoShip)
                {

                    harbour[i].dockPlace[0].DaysToStay--;
                    if (harbour[i].dockPlace[0].DaysToStay == 0)
                    {
                        harbour[i].dockPlace[0] = null;
                        harbour[i].Available = true;
                        harbour[i + 1].Available = true;
                        harbour[i + 2].Available = true;
                        harbour[i + 3].Available = true;
                    }
                }
                if (harbour[i].dockPlace[0] is SailBoat)
                {
                    harbour[i].dockPlace[0].DaysToStay--;
                    if (harbour[i].dockPlace[0].DaysToStay == 0)
                    {
                        harbour[i].dockPlace[0] = null;
                        harbour[i].Available = true;
                        harbour[i + 1].Available = true;
                    }
                }
                if (harbour[i].dockPlace[0] is RowBoat || harbour[i].dockPlace[0] is MotorBoat)
                {
                    harbour[i].dockPlace[0].DaysToStay--;
                    if (harbour[i].dockPlace[0].DaysToStay == 0)
                    {
                        harbour[i].dockPlace[0] = null;
                        harbour[i].Available = true;
                    }
                }
                if (harbour[i].dockPlace[1] is RowBoat)
                {
                    harbour[i].dockPlace[1].DaysToStay--;
                    if (harbour[i].dockPlace[1].DaysToStay == 0)
                    {
                        harbour[i].dockPlace[1] = null;
                        harbour[i].Available = true;
                    }
                }
            }
        }


        /// <summary>
        /// See if any new boats that are arriving can dock to harbour, but first we need to see which ones who are leaving. Then we save them to our file
        /// </summary>
        /// <param name="newBoats2"></param>
        private static void DockBoats(List<Boat> newBoats2)
        {
            LeavingBoats();

            foreach (var boat in newBoats2)
            {
                DockBoats2(boat);
            }

            newBoats2.Clear();
            Utilities.SaveData(harbour);
        }


        /// <summary>
        /// Add boats to harbour
        /// </summary>
        /// <param name="boat2">Each new boat created</param>
        private static void DockBoats2(Boat boat2)
        {
            bool rejected = true;
            for (int i = 0; i < harbour.Length; i++)
            {
                if (boat2 is RowBoat)
                {
                    if (harbour[i].Available == true && harbour[i].dockPlace[0] == null)
                    {
                        harbour[i].dockPlace[0] = boat2;
                        boat2.Berth = harbour[i].InnerBertNumber;
                        harbour[i].Available = false;
                        rejected = false;
                        break;
                    }
                    if (harbour[i].dockPlace[0] is RowBoat && harbour[i].dockPlace[1] == null)
                    {
                        harbour[i].dockPlace[1] = boat2;
                        boat2.Berth = harbour[i].InnerBertNumber;
                        rejected = false;
                        break;
                    }
                }

                if (boat2 is MotorBoat)
                {
                    if (harbour[i].Available == true && harbour[i].dockPlace[0] == null)
                    {
                        harbour[i].dockPlace[0] = boat2;
                        boat2.Berth = harbour[i].InnerBertNumber;
                        harbour[i].Available = false;
                        rejected = false;
                        break;
                    }
                }

                if (boat2 is SailBoat)
                {
                    if (i < harbour.Length - 1)
                    {
                        if (harbour[i].Available == true && harbour[i].dockPlace[0] == null && harbour[i + 1].dockPlace[0] == null)
                        {
                            harbour[i].dockPlace[0] = boat2;
                            boat2.Berth = harbour[i].InnerBertNumber;
                            harbour[i].Available = false;
                            harbour[i + 1].Available = false;
                            rejected = false;
                            break;
                        }
                    }
                }

                if (boat2 is CargoShip)
                {
                    if (i < harbour.Length - 3)
                    {
                        if (harbour[i].Available == true && harbour[i].dockPlace[0] == null && harbour[i + 1].dockPlace[0] == null && harbour[i + 2].dockPlace[0] == null && harbour[i + 3].dockPlace[0] == null && i < 61)
                        {
                            harbour[i].dockPlace[0] = boat2;
                            boat2.Berth = harbour[i].InnerBertNumber;
                            harbour[i].Available = false;
                            harbour[i + 1].Available = false;
                            harbour[i + 2].Available = false;
                            harbour[i + 3].Available = false;
                            rejected = false;
                            break;
                        }
                    }
                }
                if (boat2 is Katamaran)
                {
                    if (i < harbour.Length - 2)
                    {
                        if (harbour[i].Available == true && harbour[i].dockPlace[0] == null && harbour[i + 1].dockPlace[0] == null && harbour[i + 2].dockPlace[0] == null && i < 62)
                        {
                            harbour[i].dockPlace[0] = boat2;
                            boat2.Berth = harbour[i].InnerBertNumber;
                            harbour[i].Available = false;
                            harbour[i + 1].Available = false;
                            harbour[i + 2].Available = false;
                            rejected = false;
                            break;
                        }
                    }
                }
            }

            if (rejected)   //If we can't fit a boat, we add them to a list.
            {
                rejectedBoats.Add(boat2);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Manager
    {
        public static void choiceMenu(bool Open)
        {
             while (Open) { 
                int managerChoice = 0;

                Console.WriteLine(" ----------------------------------------------------");
                Console.WriteLine("| Selecteer een optie met het bijbehoorende nummer:  |");
                Console.WriteLine("| 1) Nieuwe film toevoegen                           |");
                Console.WriteLine("| 2) Terug naar het hoofdmenu                        |");
                Console.WriteLine("| 3) Afsluiten                                       |");
                Console.WriteLine(" ----------------------------------------------------\n");


                while (managerChoice < 1 || managerChoice > 3)
                {
                    try
                    {
                        managerChoice = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Maak a.u.b. een keuze uit één van de opties:");
                    }

                }
                //deze switch statement controleert of de gebruiker optie 1 of 2 kiest
                switch (managerChoice)
                {
                    case 1:
                        Console.WriteLine("Coming soon");
                        break;
                    case 2:
                        Open = false;
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}

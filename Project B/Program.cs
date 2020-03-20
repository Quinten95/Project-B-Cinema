using System;

namespace Project_B
{
    class Program
    {
        static void Main(string[] args)
        {
            string userChoice;

            Console.WriteLine("Selecteer een optie met de bijbehoorende nummer.");
            Console.WriteLine("------------------------");
            Console.WriteLine("1) Bekijk ons filmaanbod");
            Console.WriteLine("2) Maak een reservatie");
            Console.WriteLine("3) Log in / Registreer");
            userChoice = Console.ReadLine();

            if (userChoice == "1")
            {
                Console.WriteLine("Ons filmaanbod");
                Console.WriteLine("------------------------");
                Console.WriteLine();
            }
            else if (userChoice == "2")
            {
                Console.WriteLine("Voor welke film wilt u tickets kopen?");
            }
            else
            {
                Console.WriteLine("Voer alstublieft een nummer in.");
           
            }
        }
        
    }
}

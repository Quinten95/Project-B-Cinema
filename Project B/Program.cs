using System;

namespace Project_B
{
    class Program
    {
        public static void Main(string[] args)
        {
            Movies.InitMovies();
            displayWelcomeMsg();
            choiceMenu();
        }

        static void displayWelcomeMsg()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------");
            Console.WriteLine("|    ========   ===   ====     ===   ===========   ====        ====         =====         ===     === |");
            Console.WriteLine("|  ======       ===   =====    ===   ===========   =====      =====        === ===         ===   ===  |");
            Console.WriteLine("| ====          ===   === ==   ===   ===           === ==    == ===       ===   ===         === ===   |");
            Console.WriteLine("| ===           ===   ===  ==  ===   ==========    ===  ==  ==  ===      ===========         =====    |");
            Console.WriteLine("| ====          ===   ===   == ===   ===           ===   ====   ===     ====     ====       === ===   |");
            Console.WriteLine("|  ======       ===   ===    =====   ===========   ===    ==    ===    ===         ===     ===   ===  |");
            Console.WriteLine("|    ========   ===   ===     ====   ===========   ===          ===   ===           ===   ===     === |");
            Console.WriteLine("------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine(" ----------------------------------------------------");
            Console.WriteLine("|      Welkom op de site van CinemaX!                |");
            Console.WriteLine("| Hier kunt u zien welke films er draaien.           |");
            Console.WriteLine("|      Ook kunt u tickets bestellen!                 |");
        }
        
        static void choiceMenu()
        {
            string userChoice;

            Console.WriteLine(" ----------------------------------------------------");
            Console.WriteLine("| Selecteer een optie met het bijbehoorende nummer:  |");
            Console.WriteLine("| 1) Bekijk ons filmaanbod                           |");
            Console.WriteLine("| 2) Maak een reservatie                             |");
            Console.WriteLine(" ----------------------------------------------------");
            userChoice = Console.ReadLine();

            if (userChoice == "1")
            {
                Movies.DisplayMovies();
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

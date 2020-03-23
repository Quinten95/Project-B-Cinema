using System;

namespace Project_B
{
    class Program
    {
        static void Main(string[] args)
        {
            displayWelcomeMsg();
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
            Console.WriteLine("                              -----------------------------------------");
            Console.WriteLine("                             |      Welkom op de site van CinemaX!     |");
            Console.WriteLine("                             | Hier kunt u zien welke films er draaien.|");
            Console.WriteLine("                             |      Ook kunt u tickets bestellen!      |");
            Console.WriteLine("                              -----------------------------------------");
        }
    }
}

using System;

namespace Project_B
{
    class Program
    {
        public static void Main(string[] args)
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
                Movies.movieCatalog(Movies.movieOne);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Movies.movieCatalog(Movies.movieTwo);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Movies.movieCatalog(Movies.movieThree);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Movies.movieCatalog(Movies.movieFour);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Movies.movieCatalog(Movies.movieFive);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Movies.movieCatalog(Movies.movieSix);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Movies.movieCatalog(Movies.movieSeven);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Movies.movieCatalog(Movies.movieEight);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
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

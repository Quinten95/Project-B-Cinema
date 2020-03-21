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
                Console.WriteLine("------------------------");
                Console.WriteLine(Movies.movieOne.movieName);
                Console.WriteLine("Zaal " + Movies.movieOne.WhichScreen);
                Console.WriteLine("Starttijden = 09:00, 13:00, 17:00");
                Console.WriteLine("Filmlengte: " + Movies.movieOne.runTime + "minuten");
                Console.WriteLine("Genre: " + Movies.movieOne.genre);
                Console.WriteLine("Regisseur: " + Movies.movieOne.director);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Console.WriteLine("------------------------");
                Console.WriteLine(Movies.movieTwo.movieName);
                Console.WriteLine("Zaal " + Movies.movieTwo.WhichScreen);
                Console.WriteLine("Starttijden = 11:00, 15:00, 19:00");
                Console.WriteLine("Filmlengte: " + Movies.movieTwo.runTime + "minuten");
                Console.WriteLine("Genre: " + Movies.movieTwo.genre);
                Console.WriteLine("Regisseur: " + Movies.movieTwo.director);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Console.WriteLine("------------------------");
                Console.WriteLine(Movies.movieThree.movieName);
                Console.WriteLine("Zaal " + Movies.movieThree.WhichScreen);
                Console.WriteLine("Starttijden = 09:00, 13:00, 17:00");
                Console.WriteLine("Filmlengte: " + Movies.movieThree.runTime + "minuten");
                Console.WriteLine("Genre: " + Movies.movieThree.genre);
                Console.WriteLine("Regisseur: " + Movies.movieThree.director);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Console.WriteLine("------------------------");
                Console.WriteLine(Movies.movieFour.movieName);
                Console.WriteLine("Zaal " + Movies.movieFour.WhichScreen);
                Console.WriteLine("Starttijden = 11:00, 15:00, 19:00");
                Console.WriteLine("Filmlengte: " + Movies.movieFour.runTime + "minuten");
                Console.WriteLine("Genre: " + Movies.movieFour.genre);
                Console.WriteLine("Regisseur: " + Movies.movieFour.director);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Console.WriteLine("------------------------");
                Console.WriteLine(Movies.movieFive.movieName);
                Console.WriteLine("Zaal " + Movies.movieFive.WhichScreen);
                Console.WriteLine("Starttijden = 09:00, 13:00, 17:00");
                Console.WriteLine("Filmlengte: " + Movies.movieFive.runTime + "minuten");
                Console.WriteLine("Genre: " + Movies.movieFive.genre);
                Console.WriteLine("Regisseur: " + Movies.movieFive.director);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Console.WriteLine("------------------------");
                Console.WriteLine(Movies.movieSix.movieName);
                Console.WriteLine("Zaal " + Movies.movieSix.WhichScreen);
                Console.WriteLine("Starttijden = 11:00, 15:00, 19:00");
                Console.WriteLine("Filmlengte: " + Movies.movieSix.runTime + "minuten");
                Console.WriteLine("Genre: " + Movies.movieSix.genre);
                Console.WriteLine("Regisseur: " + Movies.movieSix.director);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Console.WriteLine("------------------------");
                Console.WriteLine(Movies.movieSeven.movieName);
                Console.WriteLine("Zaal " + Movies.movieSeven.WhichScreen);
                Console.WriteLine("Starttijden = 09:00, 13:00, 17:00");
                Console.WriteLine("Filmlengte: " + Movies.movieSeven.runTime + "minuten");
                Console.WriteLine("Genre: " + Movies.movieSeven.genre);
                Console.WriteLine("Regisseur: " + Movies.movieSeven.director);
                Console.WriteLine(""); //in deze regel komt later de korte uitschrijving over de film
                Console.WriteLine("------------------------");
                Console.WriteLine(Movies.movieEight.movieName);
                Console.WriteLine("Zaal " + Movies.movieEight.WhichScreen);
                Console.WriteLine("Starttijden = 11:00, 15:00, 19:00");
                Console.WriteLine("Filmlengte: " + Movies.movieEight.runTime + "minuten");
                Console.WriteLine("Genre: " + Movies.movieEight.genre);
                Console.WriteLine("Regisseur: " + Movies.movieEight.director);
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

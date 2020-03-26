using System;
using System.Globalization;

namespace Project_B
{
    class Program
    {
        public static void Main(string[] args)
        {
            Screen.InitScreens();
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
            int userChoice = 0;

            Console.WriteLine(" ----------------------------------------------------");
            Console.WriteLine("| Selecteer een optie met het bijbehoorende nummer:  |");
            Console.WriteLine("| 1) Bekijk ons filmaanbod                           |");
            Console.WriteLine("| 2) Maak een reservatie                             |");
            Console.WriteLine(" ----------------------------------------------------\n");

            while (userChoice != 1 && userChoice != 2)
            {
                try
                {
                    userChoice = int.Parse(Console.ReadLine());                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Maak a.u.b. een keuze uit één van de opties:");
                }
                
            }
            switch (userChoice)
            {
                case 1:
                    Movies.DisplayMovies();
                    Console.WriteLine("\n");
                    choiceMenu();
                    break;
                case 2:
                    Console.WriteLine("Voor welke film wilt u tickets kopen:");
                    userChoice = -1;
                    while (userChoice < 1 || userChoice > Movies.movieList.Count)
                    {
                        try
                        {
                            userChoice = int.Parse(Console.ReadLine());
                            Movies movieToReserve = (Movies)Movies.movieList[userChoice - 1];
                            reserveTicket(movieToReserve);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Voert u a.u.b. een film nummer in:");
                        }
                    }
                    break;
            }
        }

        static void reserveTicket(Movies movie)
        {

            Console.WriteLine("U heeft gekozen voor: " + movie.movieName);
            Console.WriteLine("Type \'y\' om uw keuze te bevestigen en \'n\' om uw keuze te wijzigen: ");
            
            string userConfirmation = "";
            while (userConfirmation != "y" && userConfirmation != "Y")
            {
                userConfirmation = Console.ReadLine();
                switch (userConfirmation)
                {
                    case "y":
                    case "Y":
                        {
                            Ticket ticket = new Ticket(movie);

                            Console.WriteLine("Voert u alstublieft uw naam in:");
                            string customerName = Console.ReadLine();

                            Console.WriteLine("Voert u alstublieft uw e-mail adres in:");
                            string customerEmail = Console.ReadLine();

                            Console.WriteLine("Voert u alsublieft uw geboortedatum in (dd/mm/yyyy):");
                            DateTime? customerBirthDay = null;
                            while (customerBirthDay == null)
                            {
                                try
                                {
                                    string customerBDayStr = Console.ReadLine();
                                    CultureInfo dutchCI = new CultureInfo("nl-NL", false);
                                    customerBirthDay = DateTime.Parse(customerBDayStr, dutchCI);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Uw geboortedatum moet als dd/mm/yyyy ingevoerd worden, bijv: 01/01/2020:");
                                }
                            }
                            Customer customer = new Customer(customerName, customerBirthDay, customerEmail);
                            break;
                        }

                    case "n":
                    case "N":
                        {
                            Console.WriteLine("Voor welke film wilt u tickets kopen:");
                            int userChoice = -1;
                            while (userChoice < 1 || userChoice > Movies.movieList.Count)
                            {
                                try
                                {
                                    userChoice = int.Parse(Console.ReadLine());
                                    Movies movieToReserve = (Movies)Movies.movieList[userChoice - 1];
                                    reserveTicket(movieToReserve);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Voert u a.u.b. een film nummer in:");
                                }
                            }

                            break;
                        }

                    default:
                        Console.WriteLine("Kiest u a.u.b. voor één van de opties");
                        break;
                }
            }

            
        }
    }
}

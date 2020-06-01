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
            Console.WriteLine("| 3) Bekijk onze prijzen                             |");
            Console.WriteLine(" ----------------------------------------------------\n");

            while (userChoice != 1 && userChoice != 2 && userChoice != 3)
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
            //deze switch statement controleert of de gebruiker optie 1 of 2 kiest
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
                        //hier wordt gecheckt of de gebruiker wel een bestaand filmnummer kiest
                        //zo ja wordt de reserveTicket method aangeroepen en de geselecteerde film als parameter meegegeven
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
                case 3:
                    MoviePrice.PriceList();
                    choiceMenu();
                    break;
            }
        }

        /** in deze method wordt met de meegegeven movie een ticket aangemaakt
         * wanneer de gebruiker aangeeft dat de geselecteerde film de juiste is
         */
        static void reserveTicket(Movies movie)
        {

            Console.WriteLine("U heeft gekozen voor: " + movie.movieName);
            Console.WriteLine("Type \'y\' om uw keuze te bevestigen en \'n\' om uw keuze te wijzigen: ");
            
            //deze while loop controleert of de gebruiker de juiste keuze heeft gemaakt
            string userConfirmation = "";
            while (userConfirmation != "y" && userConfirmation != "Y")
            {
                userConfirmation = Console.ReadLine();
                switch (userConfirmation)
                {
                    //als de gebruiker bevestigd met y of Y wordt er een ticket aangemaakt waar de geselecteerde film aan wordt meegegeven
                    //vervolgens wordt om de gebruiker zijn gegevens gevraagd en wordt hier een nieuwe customer instance van gemaakt
                    case "y":
                    case "Y":
                        {
                            Ticket ticketCaller = null;
                            Console.WriteLine("Wilt u een VIP ticket kopen? (ja/nee)");
                            string vipChoice = Console.ReadLine().ToLower();
                            bool isVip = false;
                            if (vipChoice == "ja")
                            {
                                isVip = true;
                            }
                            Console.WriteLine("Hoeveel tickets wilt u bestellen? (Min 1, Max 10)");
                            int numberOfPeople = -1;
                            while (numberOfPeople < 1 || numberOfPeople > 10)
                            {                                
                                try
                                {
                                    numberOfPeople = int.Parse(Console.ReadLine());
                                    Ticket ticket = new Ticket(movie, numberOfPeople, isVip);
                                    ticketCaller = ticket;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Voert u a.u.b. het aantal personen in (Min 1, Max 10)");
                                }
                                
                                if (numberOfPeople < 1)
                                {
                                    Console.WriteLine("Het aantal personen kan niet kleiner dan 1 zijn:");
                                }
                                if (numberOfPeople > 10)
                                {
                                    Console.WriteLine("Het aantal personen mag niet groter dan 10 zijn:");
                                }
                            }

                            Console.WriteLine("Voert u alstublieft uw naam in:");
                            string customerName = Console.ReadLine();

                            Console.WriteLine("Voert u alstublieft uw e-mail adres in:");
                            string customerEmail = Console.ReadLine();

                            Console.WriteLine("Voert u alsublieft uw geboortedatum in (dd/mm/yyyy):");
                            DateTime? customerBirthDay = null;
                            //deze while loop met try/catch clausule zorgt ervoor dat de gebruiker een correcte datum invult
                            //die ook converteerbaar is naar een DateTime format 
                            //zonder dat de app crasht als de gebruiker een foute datum invult
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
                            Customer customer = new Customer(customerName, customerBirthDay, customerEmail: customerEmail);

                            //dit genereert een random string van cijfers en letters (de reserveringscode waarmee de klant naar de kassa kan)
                            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                            char[] reservationCodeChars = new char[10];
                            Random random = new Random();

                            for (int i = 0; i < reservationCodeChars.Length; i++)
                            {
                                reservationCodeChars[i] = chars[random.Next(chars.Length)];
                            }

                            string reservationCode = new string(reservationCodeChars);

                            Console.WriteLine("Wilt u nog snacks kopen? (ja/nee)");
                            string snackChoice = Console.ReadLine().ToLower();
                            if (snackChoice == "ja")
                            {
                                Snacks[] gekozenSnacks = Snacks.snackKeuze();


                            }

                            Console.WriteLine("\nU heeft gekozen voor: " + movie.movieName + " op " + movie.startTime);
                            Console.WriteLine("De film speelt zich af in zaal: " + movie.whichScreen.screenNumber);
                            Console.WriteLine("Uw reserveringscode is: " + reservationCode + "\n\n");
                            Console.WriteLine("De totale prijs is: " + ticketCaller.totalPrice);
                            break;
                        }
                    //wanneer de gebruiker n of N invult bij de bevestigingsvraag
                    //moet deze een nieuwe film kiezen die aan deze method meegegeven wordt
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
                        Console.WriteLine("Kiest u a.u.b. voor één van de opties:");
                        break;
                }
                    
            }

            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Project_B
{

    class Program {

        static List<Customer> registeredCustomers = new List<Customer>();
    
        public static void Main(string[] args)
        {
            fillRegisteredCustomerList();
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
            Console.WriteLine("| 4) Account registratie                             |");
            Console.WriteLine("| 5) Inloggen                                        |");
            Console.WriteLine("| 6) Zaalstatus bekijken                             |");
            Console.WriteLine("| 7) Afsluiten                                       |");
            Console.WriteLine(" ----------------------------------------------------\n");

            while (userChoice < 1 || userChoice > 6)
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
            //deze switch statement controleert of de gebruiker een van de beschikbare opties op het hoofdmenu kiest
            switch (userChoice)
            {
                case 1:
                    Console.WriteLine("Hoe wilt u de filmlijst bekijken? \n 1) Zoeken op sleutelwoorden \n 2) Ik wil de hele lijst zien");
                    int viewList = 0;  
                    while (viewList < 1 || viewList > 2)
                    {
                        try
                        {
                            viewList = int.Parse(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Maak a.u.b. een keuze uit één van de opties:");
                        }
                    }
                    //Deze switch statement controleert of de gebruiker een van de beschikbare manieren van films zoeken kiest.
                    switch (viewList)
                    {
                         case 1:
                           Console.WriteLine("Voer uw zoektermen in.");
                           string[] searchTerms = Console.ReadLine().Split();
                           Movies.DisplayMovies(searchTerms);
                           break;
                         case 2:
                           Movies.DisplayMovies();
                           break;       
                    }

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

                case 4:
                    registerCustomer();
                    choiceMenu();
                    break;

                case 5:
                    loginCustomer();
                    choiceMenu();
                    break;

                case 6:
                    Console.WriteLine("Van welke voorstelling wilt u de zaalstatus zien?");
                    string allMovies = "";
                    foreach(Movies movie in Movies.movieList)
                    {
                        allMovies += $"{movie.movieID}) {movie.movieName} | ";
                    }
                    Console.WriteLine(allMovies);
                    int seeScreen = 0;
                    while (seeScreen < 1 || seeScreen > Movies.movieList.Count)
                    {
                        try
                        {
                            seeScreen = int.Parse(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Maak a.u.b. een keuze uit één van de opties:");
                        }
                    }
                    Movies.ScreenSeats((Movies)Movies.movieList[seeScreen - 1]);
                    break;

                case 7:
                    Environment.Exit(0);
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
                            Console.WriteLine("Wilt u een VIP ticket kopen? (y/n)");
                            string vipChoice = Console.ReadLine().ToLower();
                            bool isVip = false;
                            if (vipChoice == "y" || vipChoice == "Y")
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
                            DateTime customerBirthDay = DateTime.Today;
                            //deze while loop met try/catch clausule zorgt ervoor dat de gebruiker een correcte datum invult
                            //die ook converteerbaar is naar een DateTime format 
                            //zonder dat de app crasht als de gebruiker een foute datum invult
                            while (customerBirthDay == DateTime.Today)
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

                            //dit genereert een random string van cijfers en letters (de reserveringscode waarmee de klant naar de kassa kan)
                            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                            char[] reservationCodeChars = new char[10];
                            Random random = new Random();

                            for (int i = 0; i < reservationCodeChars.Length; i++)
                            {
                                reservationCodeChars[i] = chars[random.Next(chars.Length)];
                            }

                            string reservationCode = new string(reservationCodeChars);

                            Console.WriteLine("\nU heeft gekozen voor: " + movie.movieName + " op " + movie.startTime);
                            Console.WriteLine("De film speelt zich af in zaal: " + movie.whichScreen.screenNumber);
                            Console.WriteLine("De totale prijs is: " + String.Format("{0:0.00}", ticketCaller.totalPrice));
                            Console.WriteLine("Uw reserveringscode is: " + reservationCode);
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

        private static void registerCustomer()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            Console.WriteLine("Voert u alstublieft uw gewenste gebruikersnaam in:");
            string customerUserName = Console.ReadLine();

            while(registeredCustomers.Exists(Customer => Customer.CustomerUserName == customerUserName))
            {
                Console.WriteLine("Gebruikersnaam bezet, kiest u a.u.b. een andere gebruikersnaam:");
                customerUserName = Console.ReadLine();
            }

            Console.WriteLine("Voert u alstublieft een wachtwoord in:");
            string customerPassword = Console.ReadLine();

            Console.WriteLine("Voert u alstublieft uw naam in:");
            string customerName = Console.ReadLine();

            Console.WriteLine("Voert u alstublieft uw e-mail adres in:");
            string customerEmail = Console.ReadLine();

            Console.WriteLine("Voert u alsublieft uw geboortedatum in (dd/mm/yyyy):");
            DateTime customerBirthDay = DateTime.Today;
            //deze while loop met try/catch clausule zorgt ervoor dat de gebruiker een correcte datum invult
            //die ook converteerbaar is naar een DateTime format 
            //zonder dat de app crasht als de gebruiker een foute datum invult
            while (customerBirthDay == DateTime.Today)
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
            customer.CustomerPassword = customerPassword;
            customer.CustomerUserName = customerUserName;
            registeredCustomers.Add(customer);

            var jsonString = JsonSerializer.Serialize(registeredCustomers, options);

            File.WriteAllText("registered_customers.json", jsonString);
            Console.WriteLine("Account geregistreerd!");
        }

        static void loginCustomer()
        {
            bool loginSuccesful = false;
            Console.WriteLine("\nGebruikersnaam:");
            string username = Console.ReadLine();
            Console.WriteLine("\nWachtwoord:");
            string password = Console.ReadLine();

            foreach (Customer customer in registeredCustomers)
            {
                if(username == customer.CustomerUserName && password == customer.CustomerPassword)
                {
                    Console.WriteLine("\nWelkom " + customer.CustomerName+"\n");
                    loginSuccesful = true;
                }
            }

            if(loginSuccesful == false)
            {
                Console.WriteLine("\nGebruikersnaam of wachtwoord onbekend!\n");
            }
        }
        //vult de registeredCustomer list met de bestaande customers in de Json file
        //dit omdat de Json file volledig overschreven wordt met alle customers die in deze list staan
        //wanneer een nieuwe registratie gemaakt wordt dmv de File.WriteAllText
        private static void fillRegisteredCustomerList()
        {
            string jsonText = File.ReadAllText("registered_customers.json");

            using (JsonDocument document = JsonDocument.Parse(jsonText))
            {
                JsonElement root = document.RootElement;
                JsonElement customerListElement = root;

                foreach(JsonElement customer in customerListElement.EnumerateArray())
                {
                    if(customer.TryGetProperty("CustomerName", out JsonElement CustomerNameElement)&&
                        customer.TryGetProperty("Birthday", out JsonElement BirthdayElement)&&
                        customer.TryGetProperty("Age", out JsonElement AgeElement)&&
                        customer.TryGetProperty("Email", out JsonElement EmailElement)&&
                        customer.TryGetProperty("CustomerPassword", out JsonElement CustomerPasswordElement)&&
                        customer.TryGetProperty("CustomerUserName", out JsonElement CustomerUserNameElement))
                    {
                        string CustomerName = CustomerNameElement.GetString();
                        DateTime Birthday = BirthdayElement.GetDateTime();
                        int Age = AgeElement.GetInt32();
                        string Email = EmailElement.GetString();
                        string CustomerPassword = CustomerPasswordElement.GetString();
                        string CustomerUserName = CustomerUserNameElement.GetString();

                        Customer customer1 = new Customer(CustomerName, Birthday, Email);
                        customer1.CustomerPassword = CustomerPassword;
                        customer1.CustomerUserName = CustomerUserName;
                        customer1.Age = Age;

                        registeredCustomers.Add(customer1);
                    }
                }
            }

        }
    }
}

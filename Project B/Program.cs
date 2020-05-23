using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Project_B
{

    class Program
    {

        static List<Customer> registeredCustomers = new List<Customer>();
        static List<Ticket> reservations = new List<Ticket>();
        public static List<Movies> movies = new List<Movies>();
        static bool loggedIn = false;
        static string loggedInCustomerUsername;


        public static void Main(string[] args)
        {
            Screen.InitScreens();
            Snacks.initSnacks();
            Movies.fillMovieList();
            fillReservationList();
            fillRegisteredCustomerList();

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
            while (true) { 
            int userChoice = 0;

            Console.WriteLine(" ----------------------------------------------------");
            Console.WriteLine("| Selecteer een optie met het bijbehorende nummer:  |");
            Console.WriteLine("| 1) Bekijk ons filmaanbod                           |");
            Console.WriteLine("| 2) Maak een reservatie                             |");
            Console.WriteLine("| 3) Zaalstatus                                      |");
            Console.WriteLine("| 4) Bekijk onze prijzen                             |");
            Console.WriteLine("| 5) Account registratie                             |");
            if (loggedIn)
            {
            Console.WriteLine("| 6) Uitloggen                                       |");
            }
            else {
            Console.WriteLine("| 6) Inloggen                                        |");
            }
            Console.WriteLine("| 7) Reservering annuleren                           |");
            Console.WriteLine("| 8) Dagoverzicht                                    |");
            Console.WriteLine("| 9) Menukaart                                       |");
            Console.WriteLine("| 10) Afsluiten                                      |");
            Console.WriteLine(" ----------------------------------------------------\n");


            while ((userChoice < 1 || userChoice > 10) 
                    && userChoice != 50)
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
                        Console.WriteLine("Wilt u 1) op termen zoeken, of 2) de hele film lijst zien?");
                        string searchChoice = Console.ReadLine();
                        switch (searchChoice)
                        {
                            case "1":
                                Console.WriteLine("Vul uw zoektermen in. (genre, titels...)");
                                string terms = Console.ReadLine();
                                string[] arrayTerms = terms.Split();
                                Movies.DisplayMovies(arrayTerms);
                                break;
                            case "2":
                                Movies.DisplayMovies();
                                Console.WriteLine("\n");
                                break;
                        }
                        break;

                    case 2:
                        Console.WriteLine("Voor welke film wilt u tickets kopen:");
                        userChoice = -1;
                        Movies.DisplayMovies(userChoice);
                        while (userChoice < 1 || userChoice > movies.Count)
                        {
                            //hier wordt gecheckt of de gebruiker wel een bestaand filmnummer kiest
                            //zo ja wordt de reserveTicket method aangeroepen en de geselecteerde film als parameter meegegeven
                            try
                            {
                                userChoice = int.Parse(Console.ReadLine());
                                Movies movieToReserve = movies[userChoice - 1];
                                reserveTicket(movieToReserve);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Voert u a.u.b. een film nummer in:");
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("Van welke film wilt u de zaalstatus zien?");
                        int statusChoice = -1;
                        Movies.DisplayMovies(statusChoice);
                        while (statusChoice < 1 || statusChoice > movies.Count)
                        {
                            try
                            {
                                //zelfde code bij case 2, maar deze roept de zaalstatus functie aan
                                statusChoice = int.Parse(Console.ReadLine());
                                Movies statusOf = movies[statusChoice - 1];
                                Movies.ScreenSeats(statusOf);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Voert u a.u.b. een film nummer in:");
                            }
                        }
                        break;
                    case 4:
                        MoviePrice.PriceList();
                        break;

                    case 5:
                        registerCustomer();
                        break;

                    case 6:
                        if (loggedIn)
                        {
                            loggedInCustomerUsername = "";
                            loggedIn = false;
                            Console.WriteLine("U bent nu uitgelogd.");
                        }
                        else
                        {
                            loginCustomer();
                        }
                        break;

                    case 7:
                        cancelReservation();
                        break;

                    case 8:
                        Movies.dayOverview();
                        break;

                    case 9:
                        Snacks.printSnacks();
                        break;
                    case 10:
                        Environment.Exit(0);
                        break;
                    case 50:
                        string code = "B3stC1n3m4ever!";
                        Console.WriteLine("Enter Code: ");
                        string input = Console.ReadLine();
                        if (input == code)
                        {
                            Console.WriteLine("Welkom, manager");
                            Manager.choiceMenu(true);
                        }
                        break;
                }
            }
        }

        /** in deze method wordt met de meegegeven movie een ticket aangemaakt
         * wanneer de gebruiker aangeeft dat de geselecteerde film de juiste is
         */
        static void reserveTicket(Movies movie)
        {

            Console.WriteLine("U heeft gekozen voor: " + movie.MovieName);
            Console.WriteLine("Type \'y\' om uw keuze te bevestigen en \'n\' om uw keuze te wijzigen: ");
            Customer customer;

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
                            Ticket ticket = null;
                            Console.WriteLine("Wilt u een VIP ticket kopen? (y/n)");
                            Console.WriteLine("Een VIP ticket garandeert de beste zitplaatsen, voor maar drie euro extra!");
                            string vipChoice = "";
                            bool isVip = false;
                            while (vipChoice != "y" || vipChoice != "Y")
                            {
                                vipChoice = Console.ReadLine();
                                if (vipChoice == "y" || vipChoice == "Y")
                                {
                                    isVip = true;
                                    break;
                                }
                                if (vipChoice == "n" || vipChoice == "N")
                                {
                                    isVip = false;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Maakt u a.u.b. een keuze:");
                                }
                            }
                            Console.WriteLine("Hoeveel tickets wilt u bestellen? (Min 1, Max 10)");
                            int numberOfPeople = -1;
                            while (numberOfPeople < 1 || numberOfPeople > 10)
                            {

                                try
                                {
                                    numberOfPeople = int.Parse(Console.ReadLine());
                                    if (numberOfPeople < 1)
                                    {
                                        Console.WriteLine("Het aantal personen kan niet kleiner dan 1 zijn:");
                                    }
                                    else if (numberOfPeople > 10)
                                    {
                                        Console.WriteLine("Het aantal personen mag niet groter dan 10 zijn:");
                                    }
                                    else
                                    {
                                        Ticket tempTicket = new Ticket(movie, numberOfPeople, isVip);

                                        Tuple<int, double>[] peoplePrices = tempTicket.PriceCalculator(numberOfPeople, movie, isVip);
                                        tempTicket.TotalPrice = tempTicket.PriceSummer(peoplePrices);
                                        string[] pickedRows = new string[numberOfPeople];
                                        string[] pickedSeats = new string[numberOfPeople];
                                        ticket = tempTicket;
                                        //for (int i = 0; i < numberOfPeople; i++)
                                        // {
                                        //    while (true)
                                        //    {
                                        //        string row = Movies.SelectRow(movie, isVip);
                                        //        string seat = Movies.SelectSeat(movie);
                                                //kijk in lijst of ze al gereserveerd zijn
                                        //        if ()
                                        //        {

                                        //        }
                                        //       else
                                        //      {
                                        //          pickedRows[i] = row;
                                        //          pickedSeats[i] = seat;
                                        //          break;
                                        //      }
                                        //   }
                                        //}
                                        //ticket = tempTicket;
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Voert u a.u.b. het aantal personen in (Min 1, Max 10)");
                                }


                            }
                            if (loggedIn == true)
                            {
                                customer = registeredCustomers.Find(x => x.CustomerUserName == loggedInCustomerUsername);
                                Console.WriteLine("\nControleer uw gegevens:");
                                Console.WriteLine("Naam: " + customer.CustomerName);
                                Console.WriteLine("Email: " + customer.Email);
                            }
                            else
                            {
                                Console.WriteLine("Voert u alstublieft uw naam in:");
                                string customerName = Console.ReadLine();

                                Console.WriteLine("Voert u alstublieft uw e-mail adres in:");
                                string customerEmail = Console.ReadLine();
                                
                                while(IsValidEmail(customerEmail) == false)
                                {
                                    Console.WriteLine("Voert u a.u.b. een geldig email adres in:");
                                    customerEmail = Console.ReadLine();
                                }



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
                                customer = new Customer(customerName, customerBirthDay, customerEmail);
                            }

                            Console.WriteLine("Wilt u snacks bij de film bestellen? y/n");
                            string snackInput = Console.ReadLine();
                            List<Snacks> chosenSnacks = new List<Snacks>();
                            if(snackInput == "y" || snackInput == "Y")
                            {
                                chosenSnacks = Snacks.snackKeuze();
                                foreach(Snacks s in chosenSnacks)
                                {
                                    ticket.TotalPrice = ticket.TotalPrice + s.Price;
                                }
                            }
                            else
                            {
                                chosenSnacks = null;
                            }

                            //dit genereert een random string van cijfers en letters (de reserveringscode waarmee de klant naar de kassa kan)
                            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                            char[] reservationCodeChars = new char[8];
                            Random random = new Random();

                            for (int i = 0; i < reservationCodeChars.Length; i++)
                            {
                                reservationCodeChars[i] = chars[random.Next(chars.Length)];
                            }

                            string reservationCode = new string(reservationCodeChars);

                            Console.WriteLine("\nU heeft gekozen voor: " + movie.MovieName + " op " + movie.startTime);
                            Console.WriteLine("De film speelt zich af in zaal: " + movie.whichScreen.screenNumber);
                            Console.WriteLine("De totale prijs is: €" + String.Format("{0:0.00}", ticket.TotalPrice));

                            Console.WriteLine("\nWilt u uw keuze bevestigen? (y/n)");
                            string userChoice1 = Console.ReadLine();

                            if (userChoice1 == "y")
                            {
                                Console.WriteLine("Uw reserveringscode is: " + reservationCode);
                                ticket.CustomerName = customer.CustomerName;
                                ticket.CustomerEmail = customer.Email;
                                ticket.ReservationCode = reservationCode;
                                sendCustomerMail(customer.CustomerName, customer.Email, reservationCode,
                                    movie.MovieName, movie.startTime, movie.whichScreen.screenNumber, ticket.TotalPrice);
                                saveReservationJson(ticket);
                            }
                            else
                            {
                                Console.WriteLine("Voor welke film wilt u tickets kopen:");
                                int userChoice = -1;
                                while (userChoice < 1 || userChoice > movies.Count)
                                {
                                    try
                                    {
                                        userChoice = int.Parse(Console.ReadLine());
                                        Movies movieToReserve = movies[userChoice - 1];
                                        reserveTicket(movieToReserve);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Voert u a.u.b. een film nummer in:");
                                    }
                                }
                            }
                            break;
                        }
                    //wanneer de gebruiker n of N invult bij de bevestigingsvraag
                    //moet deze een nieuwe film kiezen die aan deze method meegegeven wordt
                    case "n":
                    case "N":
                        {
                            Console.WriteLine("Voor welke film wilt u tickets kopen:");
                            int userChoice = -1;
                            Movies.DisplayMovies(userChoice);
                            while (userChoice < 1 || userChoice > movies.Count)
                            {
                                try
                                {
                                    userChoice = int.Parse(Console.ReadLine());
                                    Movies movieToReserve = movies[userChoice - 1];
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

        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static void saveReservationJson(Ticket ticket)
        {
            reservations.Add(ticket);

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            var jsonString = JsonSerializer.Serialize(reservations, options);

            File.WriteAllText("reservations.json", jsonString);
        }

        static void fillReservationList()
        {
            string jsonText = File.ReadAllText("reservations.json");

            using (JsonDocument document = JsonDocument.Parse(jsonText))
            {
                JsonElement root = document.RootElement;
                JsonElement reservationList = root;

                foreach (JsonElement ticket in reservationList.EnumerateArray())
                {
                    if (ticket.TryGetProperty("ReservationCode", out JsonElement ReservationCodeElement) &&
                        ticket.TryGetProperty("CustomerName", out JsonElement CustomerNameElement) &&
                        ticket.TryGetProperty("CustomerEmail", out JsonElement CustomerEmailElement) &&
                        ticket.TryGetProperty("MovieName", out JsonElement MovieNameElement) &&
                        ticket.TryGetProperty("NumberOfPeople", out JsonElement NumberOfPeopleElement) &&
                        ticket.TryGetProperty("IsVip", out JsonElement IsVipElement) &&
                        ticket.TryGetProperty("TotalPrice", out JsonElement TotalPriceElement) )
                    {
                        string reservationCode = ReservationCodeElement.GetString();

                        string customerName = CustomerNameElement.GetString();
                        string customerEmail = CustomerEmailElement.GetString();
                        string movieName = MovieNameElement.GetString();
                        int numberOfPeople = NumberOfPeopleElement.GetInt32();
                        bool isVip = IsVipElement.GetBoolean();
                        double totalPrice = TotalPriceElement.GetDouble();
                        


                        Movies tempMovie = movies.Find(x => x.MovieName == movieName);

                        Ticket fillTicket = new Ticket(tempMovie, numberOfPeople, isVip);
                        fillTicket.CustomerName = customerName;
                        fillTicket.CustomerEmail = customerEmail;
                        fillTicket.ReservationCode = reservationCode;
                        reservations.Add(fillTicket);
                    }
                }
            }

        }

        private static void registerCustomer()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            Console.WriteLine("Voert u alstublieft uw gewenste gebruikersnaam in:");
            string customerUserName = Console.ReadLine();

            while (registeredCustomers.Exists(Customer => Customer.CustomerUserName == customerUserName))
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
            while (IsValidEmail(customerEmail) == false)
            {
                Console.WriteLine("Voert u a.u.b. een geldig email adres in:");
                customerEmail = Console.ReadLine();
            }
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
                if (username == customer.CustomerUserName && password == customer.CustomerPassword)
                {
                    Console.WriteLine("\nWelkom, " + customer.CustomerName + "\n");
                    loggedInCustomerUsername = customer.CustomerUserName;
                    loginSuccesful = true;
                }
            }

            if (loginSuccesful == false)
            {
                Console.WriteLine("\nGebruikersnaam of wachtwoord onbekend!\n");
            }

            loggedIn = loginSuccesful;
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

                foreach (JsonElement customer in customerListElement.EnumerateArray())
                {
                    if (customer.TryGetProperty("CustomerName", out JsonElement CustomerNameElement) &&
                        customer.TryGetProperty("Birthday", out JsonElement BirthdayElement) &&
                        customer.TryGetProperty("Age", out JsonElement AgeElement) &&
                        customer.TryGetProperty("Email", out JsonElement EmailElement) &&
                        customer.TryGetProperty("CustomerPassword", out JsonElement CustomerPasswordElement) &&
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

        static void cancelReservation()
        {
            Console.WriteLine("Weet u zeker dat u uw reservering wilt annuleren? (y/n)");
            string userChoice = "";
            while (userChoice != "y" || userChoice != "Y")
            {
                userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "Y":
                    case "y":
                        Console.WriteLine("Wat is uw reserveringscode (Let op: hoofdletter gevoelig!):");
                        string reservationCodeCustomer = Console.ReadLine();
                        bool reservationExists = false;

                        string jsonText = File.ReadAllText("reservations.json");
                        using (JsonDocument document = JsonDocument.Parse(jsonText))
                        {
                            JsonElement root = document.RootElement;
                            JsonElement reservationList = root;
                            reservations.Clear();
                            foreach (JsonElement ticket in reservationList.EnumerateArray())
                            {
                                if (ticket.TryGetProperty("ReservationCode", out JsonElement ReservationCodeElement) &&
                                    ticket.TryGetProperty("CustomerName", out JsonElement CustomerNameElement) &&
                                    ticket.TryGetProperty("CustomerEmail", out JsonElement CustomerEmailElement) &&
                                    ticket.TryGetProperty("MovieName", out JsonElement MovieNameElement) &&
                                    ticket.TryGetProperty("NumberOfPeople", out JsonElement NumberOfPeopleElement) &&
                                    ticket.TryGetProperty("IsVip", out JsonElement IsVipElement) &&
                                    ticket.TryGetProperty("TotalPrice", out JsonElement TotalPriceElement))
                                {
                                    string reservationCode = ReservationCodeElement.GetString();
                                    if (reservationCode == reservationCodeCustomer)
                                    {
                                        Console.WriteLine("Uw reservering is geannuleerd!");
                                        reservationExists = true;
                                    }
                                    else
                                    {
                                        string customerName = CustomerNameElement.GetString();
                                        string customerEmail = CustomerEmailElement.GetString();
                                        string movieName = MovieNameElement.GetString();
                                        int numberOfPeople = NumberOfPeopleElement.GetInt32();
                                        bool isVip = IsVipElement.GetBoolean();
                                        double totalPrice = TotalPriceElement.GetDouble();

                                        Movies tempMovie = movies.Find(x => x.MovieName == movieName);

                                        Ticket fillTicket = new Ticket(tempMovie, numberOfPeople, isVip);
                                        fillTicket.CustomerName = customerName;
                                        fillTicket.CustomerEmail = customerEmail;
                                        fillTicket.ReservationCode = reservationCode;
                                        reservations.Add(fillTicket);
                                    }
                                }
                            }

                            JsonSerializerOptions options = new JsonSerializerOptions();
                            options.WriteIndented = true;
                            var jsonString = JsonSerializer.Serialize(reservations, options);
                            File.WriteAllText("reservations.json", jsonString);

                            if (reservationExists == false)
                            {
                                Console.WriteLine("Onbekend reserveringsnummer!");
                            }
                        }
                        choiceMenu();
                        break;

                    case "N":
                    case "n":
                        choiceMenu();
                        break;

                    default:
                        Console.WriteLine("Voer a.u.b. uw keuze in: ");
                        break;
                }

            }

        }

        static void sendCustomerMail(string customerName, string customerEmail, string reservationCode,
                                    string movieName, DateTime startTime, int screenNumber, double totalPrice)
        {


            string adres = "cinemax.noreply@gmail.com";
            string password = "Cinemax1234";

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(adres);
            msg.To.Add(new MailAddress(customerEmail));
            msg.Subject = "Uw CinemaX reservering";
            msg.Body = "Beste " + customerName + ", hierbij een bevestiging van uw reservering:\n" +
                        "\nReserveringscode: " + reservationCode +
                        "\nFilm: " + movieName +
                        "\nTijd: " + startTime.ToString("dd/MM/yyyy HH:mm") +
                        "\nZaal: " + screenNumber +
                        "\nPrijs: €" + String.Format("{0:0.00}", totalPrice);
            msg.IsBodyHtml = false;



            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            NetworkCredential loginInfo = new NetworkCredential(adres, password);


            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = loginInfo;

            smtpClient.Send(msg);

        }
    }
}

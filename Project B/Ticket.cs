using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Project_B
{
    class Ticket
    {
        private Screen screen;
        public RowSeat rowandseat { get; set; }
        public string ReservationCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string MovieName { get; set; }
        public Movies movie;
        public int NumberOfPeople { get; set; }
        public bool IsVip { get; set; }
        public Tuple<int, double>[] peoplePrices;
        public double TotalPrice { get; set; }

        public Ticket(Movies movie, int numberOfPeople, bool VipChoice)
        {
            this.movie = movie;
            this.MovieName = movie.MovieName;
            this.NumberOfPeople = numberOfPeople;
            this.IsVip = VipChoice;
        }

        public Tuple<int, double>[] PriceCalculator(int x, Movies y, bool z)
        {
            Tuple<int, double>[] collector = new Tuple<int, double>[x];
            for (int i = 0; i < x; i++)
            {
                Console.WriteLine("Voer persoon " + (i + 1) + " zijn/haar leeftijd in.");
                int personIage = -1; 
                while(personIage < 0 || personIage > 120)
                {
                    try
                    {
                       personIage = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Voert u a.u.b. de leeftijd van de tickethouder in.");
                    }

                    if (personIage < 1)
                    {
                        Console.WriteLine("Leeftijd kan niet kleiner dan 1 zijn.");
                    }
                    if (personIage > 120)
                    {
                        Console.WriteLine("Leeftijd kan niet groter dan 120 zijn.");
                    }
                }
                double personIprice = MoviePrice.calcTicketPrice(movie, personIage, IsVip);
                collector[i] = Tuple.Create(personIage, personIprice);
            }
            return collector;
        }

        public double PriceSummer (Tuple<int, double>[] looper)
        {
            double total = 0.00;
            for(int j = 0; j < looper.Length; j++)
            {
                total += looper[j].Item2;
            }
            return total;
        }

        //vult de registeredCustomer list met de bestaande customers in de Json file
        //dit omdat de Json file volledig overschreven wordt met alle customers die in deze list staan
        //wanneer een nieuwe registratie gemaakt wordt dmv de File.WriteAllText
        public static void fillRegisteredCustomerList()
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

                        Program.registeredCustomers.Add(customer1);
                    }
                }
            }

        }
    }
}

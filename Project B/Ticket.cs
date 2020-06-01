using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Ticket
    {
        private Screen screen;
        public string ReservationCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string MovieName { get; set; }
        public Movies movie;
        public int NumberOfPeople { get; set; }
        public bool IsVip { get; set; }
        public Tuple<int, double>[] peoplePrices;
        public double TotalPrice { get; set; }
        public int SelectedRow { get; set; }
        public int SelectedSeat { get; set; }

        public Ticket(Movies movie, int numberOfPeople, bool VipChoice, int selectedRow, int selectedSeat)
        {
            this.movie = movie;
            this.MovieName = movie.MovieName;
            this.NumberOfPeople = numberOfPeople;
            this.IsVip = VipChoice;
            this.SelectedRow = selectedRow;
            this.SelectedSeat = selectedSeat;
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class MoviePrice
    {
        public static double basePrice = 7.00;
        public static double price3D = 10.00;
        public static double IMAXPrice = 12.00;
        public static double auro3DPrice = 15.00;
        public static double childrensDiscount = 0.8;
        public static double elderlyDiscount = 0.7;
        public static double studentDiscount = 0.9;

        public static Func<double, double, double> calcDiscount = (x, y) => x * y;
        public static Func<double, double, double> round = (a, b) =>
        {
            double p = calcDiscount(a, b);
            double roundedValue = Math.Round(p, 2);
            return roundedValue;
        };

        //Deze method koppelt een prijs aan de ticket met de leeftijd van de persoon als basis.
        public static double calcTicketPrice(Ticket ticket, Customer person)
        {
            if (person.Age < 13)
            {
                return round(basePrice, childrensDiscount);
            }
            else if (person.Age > 17 && person.Age < 25)
            {
                return round(basePrice, studentDiscount);
            }
            else if (person.Age > 64)
            {
                return round(basePrice, elderlyDiscount);
            }
            else
            {
                return basePrice;
            }
        }

        public static void PriceList()
        {
            Console.WriteLine("Onze Prijzen");
            Console.WriteLine("-------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Standaard Filmprijs = " + basePrice);
            Console.WriteLine("Kinderkorting = " + round(basePrice, childrensDiscount));
            Console.WriteLine("Bejaardenkorting = " + round(basePrice, elderlyDiscount));
            Console.WriteLine("Studentenkorting = " + round(basePrice, studentDiscount));
            Console.WriteLine(" ");
            Console.WriteLine("3D Filmprijs = " + price3D);
            Console.WriteLine("Kinderkorting = " + round(price3D, childrensDiscount));
            Console.WriteLine("Bejaardenkorting = " + round(price3D, elderlyDiscount));
            Console.WriteLine("Studentenkorting = " + round(price3D, studentDiscount));
            Console.WriteLine(" ");
            Console.WriteLine("IMAX Filmprijs = " + IMAXPrice);
            Console.WriteLine("Kinderkorting = " + round(IMAXPrice, childrensDiscount));
            Console.WriteLine("Bejaardenkorting = " + round(IMAXPrice, elderlyDiscount));
            Console.WriteLine("Studentenkorting = " + round(IMAXPrice, studentDiscount));
            Console.WriteLine(" ");
            Console.WriteLine("Auro3D Filmprijs = " + auro3DPrice);
            Console.WriteLine("Kinderkorting = " + round(auro3DPrice, childrensDiscount));
            Console.WriteLine("Bejaardenkorting = " + round(auro3DPrice, elderlyDiscount));
            Console.WriteLine("Studentenkorting = " + round(auro3DPrice, studentDiscount));
            Console.WriteLine(" ");
            Console.WriteLine("-------------------------");
        }

    }
}

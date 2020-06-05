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
        public static double VIPcost = 3.00;
        public static double childrensDiscount = 0.80;
        public static double elderlyDiscount = 0.70;
        public static double studentDiscount = 0.90;

        public static Func<double, double, double> calcDiscount = (x, y) => x * y;
        public static Func<double, double, double> round = (a, b) =>
        {
            double p = calcDiscount(a, b);
            double roundedValue = Math.Round(p, 2);
            return roundedValue;
        };

        public static Func<Movies, double> typeChecker1 = x => x.movieType == "Base" ? basePrice : typeChecker2(x);
        public static Func<Movies, double> typeChecker2 = x => x.movieType == "3D" ? price3D : typeChecker3(x);
        public static Func<Movies, double> typeChecker3 = x => x.movieType == "IMAX" ? IMAXPrice : typeChecker4(x);
        public static Func<Movies, double> typeChecker4 = x => x.movieType == "Auro3D" ? auro3DPrice : basePrice;

        //Deze method koppelt een prijs aan de ticket met de leeftijd van de persoon als basis.
        public static double calcTicketPrice(Movies movie, int age, bool vip)
        {
            double price = typeChecker1(movie);
            if (vip)
            {
                price += VIPcost;
            }
            if (age < 13)
            {
                return round(price, childrensDiscount);
            }
            else if (age < 23)
            {
                return round(price, studentDiscount);
            }
            else if (age > 64)
            {
                return round(price, elderlyDiscount);
            }
            else
            {
                return price;
            }
        }

        public static void PriceList()
        {
            Console.WriteLine("Onze Prijzen");
            Console.WriteLine("-------------------------");
            Console.WriteLine(" ");
            Console.WriteLine("Standaard Filmprijs = " + basePrice.ToString("0.00"));
            Console.WriteLine("Kinderkorting = " + round(basePrice, childrensDiscount).ToString("0.00"));
            Console.WriteLine("Bejaardenkorting = " + round(basePrice, elderlyDiscount).ToString("0.00"));
            Console.WriteLine("Studentenkorting = " + round(basePrice, studentDiscount).ToString("0.00"));
            Console.WriteLine(" ");
            Console.WriteLine("3D Filmprijs = " + price3D.ToString("0.00"));
            Console.WriteLine("Kinderkorting = " + round(price3D, childrensDiscount).ToString("0.00"));
            Console.WriteLine("Bejaardenkorting = " + round(price3D, elderlyDiscount).ToString("0.00"));
            Console.WriteLine("Studentenkorting = " + round(price3D, studentDiscount).ToString("0.00"));
            Console.WriteLine(" ");
            Console.WriteLine("IMAX Filmprijs = " + IMAXPrice);
            Console.WriteLine("Kinderkorting = " + round(IMAXPrice, childrensDiscount).ToString("0.00"));
            Console.WriteLine("Bejaardenkorting = " + round(IMAXPrice, elderlyDiscount).ToString("0.00"));
            Console.WriteLine("Studentenkorting = " + round(IMAXPrice, studentDiscount).ToString("0.00"));
            Console.WriteLine(" ");
            Console.WriteLine("Auro3D Filmprijs = " + auro3DPrice.ToString("0.00"));
            Console.WriteLine("Kinderkorting = " + round(auro3DPrice, childrensDiscount).ToString("0.00"));
            Console.WriteLine("Bejaardenkorting = " + round(auro3DPrice, elderlyDiscount).ToString("0.00"));
            Console.WriteLine("Studentenkorting = " + round(auro3DPrice, studentDiscount).ToString("0.00"));
            Console.WriteLine(" ");
            Console.WriteLine("-------------------------");
        }

    }
}

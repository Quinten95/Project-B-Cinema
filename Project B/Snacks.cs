using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Project_B
{
    class Snacks
    {
        private string snackName;
        private string nutritionInfo;
        public double priceInfo;

        public Snacks(string snackName, string nutritionInfo, double priceInfo)
        {
            this.snackName = snackName;
            this.nutritionInfo = nutritionInfo;
            this.priceInfo = priceInfo;

        }
        static Snacks snack1 = new Snacks("Popcorn zout small", "196 Kcal, beker van 50g", 3.50);
        static Snacks snack2 = new Snacks("Popcorn zout medium", "392 Kcal, beker van 100g", 4.50);
        static Snacks snack3 = new Snacks("Popcorn zout large", "745 Kcal, beker van 190g", 6.00);
        static Snacks snack4 = new Snacks("Popcorn zoet small", "382 Kcal, beker van 90g", 3.50);
        static Snacks snack5 = new Snacks("Popcorn zoet medium", "764 Kcal, beker van 180g", 4.50);
        static Snacks snack6 = new Snacks("Popcorn zoet large", "1484 Kcal, beker van 350g", 6.00);
        static Snacks snack7 = new Snacks("Nacho's klein", "1200 Kcal", 4.00);
        static Snacks snack8 = new Snacks("Nacho's groot", "2000 Kcal", 6.00);
        static Snacks snack9 = new Snacks("Broodje Hot Dog", "980 Kcal", 3.00);
        static Snacks snack10 = new Snacks("M&M's", "1012 Kcal", 3.25);
        static Snacks snack11 = new Snacks("Maltesers", "1000 Kcal", 3.25);
        static Snacks snack12 = new Snacks("Kit Kat", "950 Kcal", 3.25);
        static Snacks snack13 = new Snacks("Flesje Cola", "102 Kcal", 2.99);
        static Snacks snack14 = new Snacks("Flesje Fanta", "142 Kcal", 2.99);
        static Snacks snack15 = new Snacks("Flesje Sprite", "129 Kcal", 2.99);
        static Snacks snack16 = new Snacks("flesje Spa", "0 Kcal", 2.99);
        static Snacks snack17 = new Snacks("flesje Ice Tea", "35 Kcal", 2.99);
        static Snacks snack18 = new Snacks("Ben & Jerry's Strawberry Cheesecake", "450 Kcal", 4.00);
        static Snacks snack19 = new Snacks("Ben & Jerry's Cookie Dough", "450Kcal", 4.00);
        static Snacks snack20 = new Snacks("Ben & Jerry's Topped", "450Kcal", 4.00);
        static Snacks snack21 = new Snacks("Bugles", "540Kcal per 100g", 3.75);
        static Snacks snack22 = new Snacks("Doritos", "499Kcal per 100g", 3.75);
        static Snacks snack23 = new Snacks("Lay's", "551Kcal per 100g", 3.75);


        public static Snacks[] snackKeuze()
        {
            Snacks[] snacklijst = new Snacks[]
            {
                snack1, snack2, snack3, snack4, snack5, snack6, snack7, snack8, snack9, snack10, snack11, snack12, snack13, snack14, snack15, snack16, snack17, snack18, snack19, snack20, snack21, snack22, snack23
            };
            Snacks[] gekozenSnacks = new Snacks[10];
            int counter = 0;
            Console.WriteLine("ons menu");
            foreach (Snacks snack in snacklijst)
            {
                Console.WriteLine(" " + snack1.snackName + ", " + snack1.nutritionInfo + ", prijs: " + snack1.priceInfo);
            }
            Console.WriteLine("kies een snack van de lijst en voer de nummer in.");
            string SnackChoice = Console.ReadLine().ToLower();
            foreach (Snacks snack in snacklijst)
            {
                if(snack.snackName.ToLower() == SnackChoice)
                {
                    gekozenSnacks[counter] = snack;
                    counter++;
                }
                    
            }

            if(counter == 0)
            {
                Console.WriteLine("Uw zoekterm komt niet overeen met de snacks. Gebruik de hele snacknaam a.u.b.");
            }
            


            return gekozenSnacks;

        }

    }
}

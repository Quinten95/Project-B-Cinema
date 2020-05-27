using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Project_B
{
    class Snacks
    {
        private string snackName;
        private string nutritionInfo;
        public double Price { get; set; }
        public static ArrayList snackList = new ArrayList(); 

        public Snacks(string snackName, string nutritionInfo, double price)
        {
            this.snackName = snackName;
            this.nutritionInfo = nutritionInfo;
            this.Price = price;
        }

        public static void initSnacks()
        {
            snackList.Add(new Snacks("Popcorn zout small", "196 Kcal, beker van 50g", 3.50));
            snackList.Add(new Snacks("Popcorn zout medium", "392 Kcal, beker van 100g", 4.50));
            snackList.Add(new Snacks("Popcorn zout large", "745 Kcal, beker van 190g", 6.00));
            snackList.Add(new Snacks("Popcorn zoet small", "382 Kcal, beker van 90g", 3.50));
            snackList.Add(new Snacks("Popcorn zoet medium", "764 Kcal, beker van 180g", 4.50));
            snackList.Add(new Snacks("Popcorn zoet large", "1484 Kcal, beker van 350g", 6.00));
            snackList.Add(new Snacks("Nacho's klein", "1200 Kcal", 4.00));
            snackList.Add(new Snacks("Nacho's groot", "2000 Kcal", 6.00));
            snackList.Add(new Snacks("Broodje Hot Dog", "980 Kcal", 3.00));
            snackList.Add(new Snacks("M&M's", "1012 Kcal", 3.25));
            snackList.Add(new Snacks("Maltesers", "1000 Kcal", 3.25));
            snackList.Add(new Snacks("Kit Kat", "950 Kcal", 3.25));
            snackList.Add(new Snacks("Flesje Cola", "102 Kcal", 2.99));
            snackList.Add(new Snacks("Flesje Fanta", "142 Kcal", 2.99));
            snackList.Add(new Snacks("Flesje Sprite", "129 Kcal", 2.99));
            snackList.Add(new Snacks("flesje Spa", "0 Kcal", 2.99));
            snackList.Add(new Snacks("flesje Ice Tea", "35 Kcal", 2.99));
            snackList.Add(new Snacks("Ben & Jerry's Strawberry Cheesecake", "450 Kcal", 4.00));
            snackList.Add(new Snacks("Ben & Jerry's Cookie Dough", "450Kcal", 4.00));
            snackList.Add(new Snacks("Ben & Jerry's Topped", "450Kcal", 4.00));
            snackList.Add(new Snacks("Bugles", "540Kcal per 100g", 3.75));
            snackList.Add(new Snacks("Doritos", "499Kcal per 100g", 3.75));
            snackList.Add(new Snacks("Lay's", "551Kcal per 100g", 3.75));
        }       
       
        public static void printSnacks()
        {
            Console.WriteLine("Menu");
            foreach (Snacks snack in snackList)
            {
                Console.WriteLine($"{snack.snackName} - {snack.nutritionInfo}, prijs: {snack.Price}");
            }
        }
        
        public static List<Snacks> snackKeuze()
        {
            int counter = 0;
            List<Snacks> gekozenSnacks = new List<Snacks>();
            printSnacks();
            Console.WriteLine("Kies een snack van de lijst door de hele naam in te voeren.");
            while (counter < 10)
            {
                int countStorage = counter;
                for (int i = counter - 1; i < counter;)
                {
                    string SnackChoice = Console.ReadLine().ToLower();
                    foreach (Snacks snack in snackList)
                    {
                        if (snack.snackName.ToLower() == SnackChoice)
                        {
                            gekozenSnacks.Add(snack);
                            counter++;
                            break;
                        }
                    }
                    if (counter == countStorage)
                    {
                        Console.WriteLine("Uw zoekterm komt niet overeen met de snacks. Gebruik de hele snacknaam a.u.b.");
                    }
                    else
                    {
                        i += 2;
                    }
                }
                Console.WriteLine("Wilt u nog meer snacks bestellen? (y/n)");
                
                while(true)
                {
                    string cont = Console.ReadLine();
                    if (cont == "y" || cont == "Y")
                    {
                        break;
                    }
                    else if (cont == "n" || cont == "N")
                    {
                        return gekozenSnacks;
                    }
                    else
                    {
                        Console.WriteLine("Voert u a.u.b een van de mogelijke opties in.");
                    }
                }
            }

            return gekozenSnacks;
        }
    }
}

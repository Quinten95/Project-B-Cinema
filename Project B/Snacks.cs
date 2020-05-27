using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace Project_B
{
    class Snacks
    {
        public string snackName { get; set; }
        public string nutritionInfo { get; set; }
        public double Price { get; set; }

        public Snacks(string snackName, string nutritionInfo, double price)
        {
            this.snackName = snackName;
            this.nutritionInfo = nutritionInfo;
            this.Price = price;
        }

        public static void FillSnackList()
        {
            string jsonText = File.ReadAllText("snacks.json");

            using (JsonDocument document = JsonDocument.Parse(jsonText))
            {
                JsonElement root = document.RootElement;
                JsonElement snackList = root;
                foreach (JsonElement snack in snackList.EnumerateArray())
                {
                    if (snack.TryGetProperty("snackName", out JsonElement snackNameElement) &&
                        snack.TryGetProperty("nutritionInfo", out JsonElement nutritionInfoElement) &&
                        snack.TryGetProperty("Price", out JsonElement PriceElement))
                    {
                        string snackName = snackNameElement.GetString();
                        string nutritionInfo = nutritionInfoElement.GetString();
                        double Price = PriceElement.GetDouble();

                        Snacks fillSnacks = new Snacks(snackName, nutritionInfo, Price);
                        Program.snacks.Add(fillSnacks);
                    }
                }
            }

        }
       
        public static void printSnacks()
        {
            Console.WriteLine("Menu");
            foreach (Snacks snack in Program.snacks)
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
                    foreach (Snacks snack in Program.snacks)
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

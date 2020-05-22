using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Project_B
{
    class Manager
    {
        public static void choiceMenu(bool Open)
        {
             while (Open) { 
                int managerChoice = 0;

                Console.WriteLine(" ----------------------------------------------------");
                Console.WriteLine("| Selecteer een optie met het bijbehoorende nummer:  |");
                Console.WriteLine("| 1) Nieuwe film toevoegen                           |");
                Console.WriteLine("| 2) Terug naar het hoofdmenu                        |");
                Console.WriteLine("| 3) Afsluiten                                       |");
                Console.WriteLine(" ----------------------------------------------------\n");


                while (managerChoice < 1 || managerChoice > 3)
                {
                    try
                    {
                        managerChoice = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Maak a.u.b. een keuze uit één van de opties:");
                    }

                }
                //deze switch statement controleert of de gebruiker optie 1 of 2 kiest
                switch (managerChoice)
                {
                    case 1:
                        Console.WriteLine("Coming soon");

                        break;
                    case 2:
                        Open = false;
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }

               
            }
        }

        //private static void addMovie()
        //{
        //    JsonSerializerOptions options = new JsonSerializerOptions();
        //    options.WriteIndented = true;

        //    Console.WriteLine("Voer de titel in:");
        //    string movieName = Console.ReadLine();

        //    Console.WriteLine("Voert u alstublieft een wachtwoord in:");
        //    string customerPassword = Console.ReadLine();

        //    Console.WriteLine("Voert u alstublieft uw naam in:");
        //    string customerName = Console.ReadLine();

        //    Console.WriteLine("Voert u alstublieft uw e-mail adres in:");
        //    string customerEmail = Console.ReadLine();

        //    Console.WriteLine("Voert u alsublieft uw geboortedatum in (dd/mm/yyyy):");
        //    DateTime customerBirthDay = DateTime.Today;
        //    //deze while loop met try/catch clausule zorgt ervoor dat de gebruiker een correcte datum invult
        //    //die ook converteerbaar is naar een DateTime format 
        //    //zonder dat de app crasht als de gebruiker een foute datum invult
        //    while (customerBirthDay == DateTime.Today)
        //    {
        //        try
        //        {
        //            string customerBDayStr = Console.ReadLine();
        //            CultureInfo dutchCI = new CultureInfo("nl-NL", false);
        //            customerBirthDay = DateTime.Parse(customerBDayStr, dutchCI);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("Uw geboortedatum moet als dd/mm/yyyy ingevoerd worden, bijv: 01/01/2020:");
        //        }
        //    }
        //    Customer customer = new Customer(customerName, customerBirthDay, customerEmail);
        //    customer.CustomerPassword = customerPassword;
        //    customer.CustomerUserName = customerUserName;
        //    registeredCustomers.Add(customer);

        //    var jsonString = JsonSerializer.Serialize(registeredCustomers, options);

        //    File.WriteAllText("registered_customers.json", jsonString);
        //    Console.WriteLine("Account geregistreerd!");
        //}
    }
}

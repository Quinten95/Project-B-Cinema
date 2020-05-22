using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                        addMovie();
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

        private static void addMovie()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;

            Console.WriteLine("Vul de gewensde ID van de film in.");
            int movieID = -1;
            while (movieID < 0)
            {
                try
                {
                    movieID = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Vul een positief getal in.");
                }
            }

            Console.WriteLine("Vul de titel in:");
            string movieName = Console.ReadLine();
            Console.WriteLine("Vul de speeltijd in minuten in.");
            int runTime = -1;
            while (runTime < 0)
            {
                try
                {
                    runTime = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Vul een positief getal in.");
                }

            }
            Console.WriteLine("Vul de regisseur in:");
            string director = Console.ReadLine();
            Console.WriteLine("Vul de genre in:");
            string genre = Console.ReadLine();
            Console.WriteLine("Vul het soort film in. Alleen 'Base', '3D', 'IMAX' en 'Auro3D' zijn valiede opties.");
            string movieType = Console.ReadLine();
            while (true)
            {
                if (movieType == "3D" || movieType == "Base" || movieType == "Auro3D" || movieType == "IMAX")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Alleen 'Base', '3D', 'IMAX' en 'Auro3D' zijn valiede opties.");
                    movieType = Console.ReadLine();
                }
            }
            Console.WriteLine("Vul een beschrijving in:");
            string synopsis = Console.ReadLine();
            Console.WriteLine("Selecteer een zaal. (getal tussen 1 en 5");
            int screenNumber = 0;
            while (screenNumber < 1 || screenNumber > 5)
            {
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    if (choice < 1 || choice > 5)
                    {
                        Console.WriteLine("Het ingevoerde getal moet tussen 1 en 5 zijn.");
                    }
                    else
                    {
                        screenNumber = choice - 1;
                        Console.WriteLine($"U heeft gekozen voor scherm {choice}.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Vul een getal tussen 1 en 5 in.");
                }

            };

            Console.WriteLine("Vul een geldige starttijd in.");
            Console.WriteLine("De ingevulde dag kan niet de huidige dag zijn.");
            Console.WriteLine("Voorbeeld = 20 Juli 2020 15:00:00");
            DateTime startTime = DateTime.Today;
            while (startTime == DateTime.Today)
            {
                try
                {
                    string startString = Console.ReadLine();
                    CultureInfo dutchCI = new CultureInfo("nl-NL", false);
                    startTime = DateTime.Parse(startString, dutchCI);
                }
                catch (Exception e)
                {
                    Console.WriteLine("De datum is niet goed ingevuld. Probeer het opnieuw.");
                    Console.WriteLine("Voorbeeld = 20 Juli 2020 15:00:00");
                }
            }
            Screen tempScreen = (Screen)Screen.screenList[screenNumber];
            Console.WriteLine("Kloppen al deze gegevens? Zo ja, vul 'y' in.");
            Console.WriteLine($"ID: {movieID}, Titel: {movieName}, Starttijd: {startTime}, Zaal: {tempScreen.screenNumber} \n" +
                $"Duur : {runTime} minuten, Genre(s) : {genre}, Regisseur: {director}, Type: {movieType}. Beschrijving: \n {synopsis}");
            string snackInput = Console.ReadLine();
            if (snackInput == "y" || snackInput == "Y")
            {
                Movies newMovie = new Movies(movieID, movieName, startTime, screenNumber, runTime, genre, director, movieType, synopsis);
                Program.movies.Add(newMovie);
                var jsonString = JsonSerializer.Serialize(Program.movies, options);
                File.WriteAllText("movies.json", jsonString);
                Console.WriteLine("Film toegevoegd.");
            }
            else
            {
                Console.WriteLine("");
            }
        }
    }
}

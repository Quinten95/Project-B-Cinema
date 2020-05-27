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
                Console.WriteLine("| Selecteer een optie met het bijbehorende nummer:   |");
                Console.WriteLine("| 1) Nieuwe film toevoegen                           |");
                Console.WriteLine("| 2) Film verwijderen                                |");
                Console.WriteLine("| 3) Filmdata veranderen                             |");
                Console.WriteLine("| 4) Film kopieëren                                  |");
                Console.WriteLine("| 5) Terug naar het hoofdmenu                        |");
                Console.WriteLine("| 6) Afsluiten                                       |");
                Console.WriteLine(" ----------------------------------------------------\n");


                while (managerChoice < 1 || managerChoice > 6)
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
                        AddMovie();
                        break;
                    case 2:
                        DeleteMovie();
                        break;
                    case 3:
                        ChangeMovie();
                        break;
                    case 4:
                        Console.WriteLine("Coming soon");
                        break;
                    case 5:
                        Open = false;
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                }

               
            }
        }
        //Deze method geeft de manager de mogelijkheid om een nieuwe film in het systeem toe te voegen.
        private static void AddMovie()
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
            Console.WriteLine($"ID: {movieID}, Titel: {movieName}, Starttijd: {startTime}, Zaal: {tempScreen.ScreenNumber} \n" +
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
                Console.WriteLine("Terugkeren naar het hoofdmenu...");
            }
        }

        private static void DeleteMovie()
        {
            Console.WriteLine("Welke film wilt u verwijderen?");
            foreach(Movies movie in Program.movies)
            {
                Console.WriteLine($"{movie.movieID}) {movie.MovieName}");
            }
            Movies choice = null;
            while (choice == null)
            {
                try
                {
                    int tempchoice = int.Parse(Console.ReadLine());
                    choice = Program.movies[tempchoice - 1];
                }
                catch (Exception e)
                {
                    Console.WriteLine("Voer a.u.b het ID van een film in.");
                }
            }
            Console.WriteLine($"U heeft gekozen voor {choice.movieID}) {choice.MovieName}.");
            Console.WriteLine("Weet u zeker dat u deze film wilt verwijderen?(y/n)");
            string confirm = Console.ReadLine();
            switch (confirm)
            {
                case "Y":
                case "y":
                    Console.WriteLine($"Film {choice.MovieName} is verwijderd.");
                    Program.movies.RemoveAt(choice.movieID - 1);
                    JsonSerializerOptions options = new JsonSerializerOptions();
                    options.WriteIndented = true;
                    var jsonString = JsonSerializer.Serialize(Program.movies, options);
                    File.WriteAllText("movies.json", jsonString);
                    break;
                case "N":
                case "n":
                    Console.WriteLine("Verwijdering afgezegd. Terugkeren naar het hoofdmenu...");
                    break;
                default:
                    Console.WriteLine("Verkeerde invoer. Terugkeren naar het hoofdmenu... ");
                    break;
            }
        }

        private static void ChangeMovie()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            Console.WriteLine("Van welke film wilt u de data veranderen?");
            Movies.DisplayMovies(0);
            Movies chosenMovie = null;
            int choice1 = 0;
            while (chosenMovie == null)
            {
                try
                {
                    choice1 = int.Parse(Console.ReadLine());
                    if(choice1 > 0 || choice1 <= Program.movies.Count)
                    {
                        chosenMovie = Program.movies[choice1 - 1];
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Voer een geldige optie in.");
                }
            }
            Console.WriteLine($"U heeft gekozen voor film {chosenMovie.movieID}");
            Console.WriteLine("Welke data wilt u veranderen? \n Starttijd / Zaal / Beschrijving");
            string choice2 = "";
            while(choice2.ToLower() != "Starttijd".ToLower() && choice2.ToLower() != "Zaal".ToLower() && choice2.ToLower() != "Beschrijving".ToLower())
            {
                choice2 = Console.ReadLine();
                if(choice2.ToLower() != "Starttijd".ToLower() && choice2.ToLower() != "Zaal".ToLower() && choice2.ToLower() != "Beschrijving".ToLower())
                {
                    Console.WriteLine("Verkeerde invoer. Kies een van de bovenstaande opties.");
                }
            }
            if (choice2.ToLower() == "Starttijd".ToLower())
            {
                Console.WriteLine($"Vul een nieuwe starttijd in. Oude is {Program.movies[choice1 - 1].startTime}.");
                while (true)
                {
                    try
                    {
                        string startString = Console.ReadLine();
                        CultureInfo dutchCI = new CultureInfo("nl-NL", false);
                        chosenMovie.startTime = DateTime.Parse(startString, dutchCI);
                        Console.WriteLine($"Nieuwe datum is {chosenMovie.startTime}");
                        break;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("De datum is niet goed ingevuld. Probeer het opnieuw.");
                        Console.WriteLine("Voorbeeld = 20 Juli 2020 15:00:00");
                    }
                }
            }
            else if(choice2.ToLower() == "Zaal".ToLower())
            {
                Console.WriteLine($"Vul een nieuwe zaal in. Oude is {Program.movies[choice1 - 1].screenNumber}.");
                int choice = -1;
                while (choice < 1 || choice > 5)
                {
                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                        if (choice < 1 || choice > 5)
                        {
                            Console.WriteLine("Het ingevoerde getal moet tussen 1 en 5 zijn.");
                        }
                        else
                        {
                            chosenMovie.screenNumber = choice;
                            Console.WriteLine($"Nieuwe zaal is {chosenMovie.screenNumber}");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Vul een getal tussen 1 en 5 in.");
                    }
                };
            }
            else if(choice2.ToLower() == "Beschrijving".ToLower())
            {
                Console.WriteLine($"Vul een nieuwe beschrijving in. Oude: {Program.movies[choice1 - 1].Synopsis}.");
                chosenMovie.Synopsis = Console.ReadLine();
                Console.WriteLine($"Nieuwe bescrijving is: {chosenMovie.Synopsis}");
            }
            Console.WriteLine("Kloppen al deze gegevens? Zo ja, vul 'y' in.");
            string snackInput = Console.ReadLine();
            if (snackInput == "y" || snackInput == "Y")
            {
                Program.movies[choice1 - 1].startTime = chosenMovie.startTime;
                Program.movies[choice1 - 1].screenNumber = chosenMovie.screenNumber;
                Program.movies[choice1 - 1].Synopsis = chosenMovie.Synopsis;
                var jsonString = JsonSerializer.Serialize(Program.movies, options);
                File.WriteAllText("movies.json", jsonString);
                Console.WriteLine("Film data veranderd.");
            }
            else
            {
                Console.WriteLine("Terugkeren naar het hoofdmenu...");
            }
        }

        private static void CopyMovie()
        {

        }
    }
}

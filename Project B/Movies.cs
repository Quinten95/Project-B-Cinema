using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Project_B
{
    class Movies
    {
        public int movieID { get; set; }
        public string MovieName { get; set; }
        public DateTime startTime { get; set; }
        public int runTime { get; set; }
        public string genre { get; set; }
        public string director { get; set; }
        public Screen whichScreen { get; set; }
        public int screenNumber { get; set; }
        public string movieType { get; set; }
        public string Synopsis { get; set; }

        public Movies(int movieID, string movieName, DateTime startTime,
             int screenNumber, int runTime, string genre, string director, string movieType, string synopsis)
        {
            this.movieID = movieID;
            this.MovieName = movieName;
            this.startTime = startTime;
            this.runTime = runTime;
            this.genre = genre;
            this.director = director;
            this.whichScreen = (Screen)Screen.screenList[screenNumber];
            this.screenNumber = screenNumber;
            this.movieType = movieType;
            this.Synopsis = synopsis;
        }

        public static void DisplayMovies()
        {
            foreach (Movies movie in Program.movies)
            {
                movieCatalog(movie);
            }   
            
        }

        //Overflow functie als de user op sleutelwoorden wilt zoeken.
        public static void DisplayMovies(string[] terms)
        {
            foreach (Movies movie in Program.movies)
            {
                for(int i = 0; i < terms.Length; i++)
                {
                    bool movieNameChecker = false;
                    bool movieGenreChecker = false;
                    //Deze foreach loops kijken of individuele termen overeenkomen met filmtitels of genres.
                    foreach(String part in movie.MovieName.Split())
                    {
                        if(part.ToLower() == terms[i].ToLower())
                        {
                            movieNameChecker = true;
                            break;
                        }
                    }
                    foreach (String part in movie.genre.Split(","))
                    {
                        if (part.ToLower() == terms[i].ToLower())
                        {
                            movieGenreChecker = true;
                            break;
                        }
                    }
                    if (terms[i].ToLower() == movie.genre.ToLower() || movieNameChecker || movieGenreChecker ||
                    terms[i] == "" + movie.movieID || terms[i].ToLower() == movie.director.ToLower())
                    {
                        movieCatalog(movie);
                        break;
                    }
                }
            }
        }

        public static void DisplayMovies(int overFlow)
        {
            string print = "";
            foreach(Movies movie in Program.movies)
            {
                print += movie.movieID + ")" + movie.MovieName + " : " + movie.startTime +"\n";
            }
            Console.WriteLine(print);
        }

        
        public static string SelectRow(Movies movie, bool vip)
        {
           Console.WriteLine("Selecteer een rij. Vul in als een cijfer. Voor de VIP vul je het cijfer tussen de haakjes in.");
           //implementeer vip check
           string choice = ""; 
           try
           {
               choice = Console.ReadLine();
               int checker = int.Parse(choice);
               if (checker < 1 && checker > movie.whichScreen.amountOfRows)
               {
                  Console.WriteLine($"Voer een getal tussen 1 en {movie.whichScreen.amountOfRows} in.");
               }
               else
               {
                    return choice;
               }
           }
           catch (Exception e)
           {
               Console.WriteLine($"Voer een getal tussen 1 en {movie.whichScreen.amountOfRows} in.");
           }
            return choice;
        }
        public static string SelectSeat(Movies movie)
        {
            Console.WriteLine("Selecteer een stoel. Vul in als een cijfer.");
            //implementeer vip check
            string choice = "";
            try
            {
                choice = Console.ReadLine();
                int checker = int.Parse(choice);
                if (checker < 1 && checker > movie.whichScreen.amountOfSeats)
                {
                    Console.WriteLine($"Voer een getal tussen 1 en {movie.whichScreen.amountOfSeats} in.");
                }
                else
                {
                    return choice;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Voer een getal tussen 1 en {movie.whichScreen.amountOfSeats} in.");
            }
            return choice;
        }

        public static void fillMovieList()
        {
            string jsonText = File.ReadAllText("movies.json");

            using (JsonDocument document = JsonDocument.Parse(jsonText))
            {
                JsonElement root = document.RootElement;
                JsonElement moviesList = root;
                foreach (JsonElement movie in moviesList.EnumerateArray())
                {
                    if (movie.TryGetProperty("movieID", out JsonElement movieIDElement) &&
                        movie.TryGetProperty("MovieName", out JsonElement MovieNameElement) &&
                        movie.TryGetProperty("startTime", out JsonElement startTimeElement) &&
                        movie.TryGetProperty("runTime", out JsonElement runTimeElement) &&
                        movie.TryGetProperty("genre", out JsonElement genreElement) &&
                        movie.TryGetProperty("director", out JsonElement directorElement) &&
                        movie.TryGetProperty("screenNumber", out JsonElement screenNumberElement) &&
                        movie.TryGetProperty("movieType", out JsonElement movieTypeElement) &&
                        movie.TryGetProperty("Synopsis", out JsonElement SynopsisElement))
                    {
                        int movieID = movieIDElement.GetInt32();
                        string MovieName = MovieNameElement.GetString();
                        DateTime startTime = startTimeElement.GetDateTime();
                        int runTime = runTimeElement.GetInt32();
                        string genre = genreElement.GetString();
                        string director = directorElement.GetString();
                        int screenNumber = screenNumberElement.GetInt32();
                        string movieType = movieTypeElement.GetString();
                        string Synopsis = SynopsisElement.GetString();

                        Movies fillMovies = new Movies(movieID, MovieName, startTime, screenNumber, runTime, genre, director, movieType, Synopsis);
                        Program.movies.Add(fillMovies);
                    }
                }
            }

        }

        public static void dayOverview()
        {
            Console.WriteLine("Voor welke dag wilt u het dagoverzicht zien?");
            Console.WriteLine("1) Vandaag \n2) Andere ");
            int dayChoice = -1;
            while (dayChoice < 1 || dayChoice > 2)
            {
                try
                {
                    dayChoice = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Maak a.u.b. een keuze uit één van de opties:");
                }

            }
            switch (dayChoice)
            {
                case 1:
                    Console.WriteLine($"Films van vandaag {DateTime.Today.ToString("dd/MM/yyyy")}");
                    int counter1 = 0;
                    foreach(Movies movie in Program.movies)
                    {
                        if(movie.startTime.Date == DateTime.Today.Date)
                        {
                            movieCatalog(movie);
                            counter1++;
                        }   
                    }
                    if (counter1 == 0)
                    {
                        Console.WriteLine("Er zijn geen films gevonden voor vandaag.\n");
                    }
                    break;
                case 2:
                    Console.WriteLine("Vul een datum in. (schrijf als dd/mm/yyyy)");
                    DateTime searchDate = new DateTime(01/01/1960);
                    while (searchDate == new DateTime(01/01/1960))
                    {
                        try
                        {   
                            string searchDatestring = Console.ReadLine();
                            searchDate = DateTime.Parse(searchDatestring);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("De datum moet als dd/mm/yyyy ingevoerd worden, bijv: 01/01/2020");
                        }
                    }
                    int counter2 = 0;
                    foreach (Movies movie in Program.movies)
                    {
                        if(movie.startTime.Date == searchDate)
                        {
                            movieCatalog(movie);
                            counter2++;
                        }   
                    }
                    if(counter2 == 0)
                    {
                        Console.WriteLine($"Er zijn geen films gevonden voor de datum {searchDate.Date.ToString("dd/MM/yyyy")}\n");
                    }
                    break;
            }

        }        
        //deze method maakt per film die meegegeven wordt een mooi weergegeven detail overzicht
        public static void movieCatalog(Movies movie)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Filmnummer: " + movie.movieID);
            Console.WriteLine(movie.MovieName);
            Console.WriteLine("Zaal " + movie.whichScreen.screenNumber);
            Console.WriteLine(movie.startTime.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("Filmlengte: " + movie.runTime + " minuten");
            Console.WriteLine("Genre: " + movie.genre);
            Console.WriteLine("Regisseur: " + movie.director);
            Console.WriteLine("Beschrijving: " + movie.Synopsis);
        }

        //Deze methods stellen nieuwe waardes voor films in. In de management functies worden ze aangeroepen.
        public static int ChangeID()
        {
            int movieID = Program.movies.Count + 1;
            return movieID;
        }
        public static DateTime ChangeStart()
        {
            Console.WriteLine("Voorbeeld = 20 Juli 2020 15:00:00");
            while (true)
            {
                try
                {
                    string startString = Console.ReadLine();
                    CultureInfo dutchCI = new CultureInfo("nl-NL", false);
                    DateTime newStart = DateTime.Parse(startString, dutchCI);
                    Console.WriteLine($"Nieuwe datum is {newStart}");
                    return newStart;
                }
                catch (Exception e)
                {
                    Console.WriteLine("De datum is niet goed ingevuld. Probeer het opnieuw.");
                    Console.WriteLine("Voorbeeld = 20 Juli 2020 15:00:00");
                }
            }
        }

        public static int ChangeScreen()
        {
            int choice = -1;
            while (choice < 1 || choice > 5)
            {
                try
                {
                    int storage = int.Parse(Console.ReadLine());
                    if (storage < 1 || storage > 5)
                    {
                        Console.WriteLine("Het ingevoerde getal moet tussen 1 en 5 zijn.");
                    }
                    else
                    {
                        choice = storage;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Vul een getal tussen 1 en 5 in.");
                }
            }
            return choice - 1;
        }
    }
}

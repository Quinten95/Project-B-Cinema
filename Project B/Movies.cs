using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Project_B
{
    class Movies
    {
        public int movieID;
        public string MovieName { get; set; }
        public DateTime startTime;
        public int runTime;
        public string genre;
        public string director;
        public Screen whichScreen;
        public string movieType;
        public static List<Movies> movieList = new List<Movies>();

        public Movies(int movieID, string movieName, DateTime startTime,
            Screen whichScreen, int runTime, string genre, string director, string movieType)
        {
            this.movieID = movieID;
            this.MovieName = movieName;
            this.startTime = startTime;
            this.runTime = runTime;
            this.genre = genre;
            this.director = director;
            this.whichScreen = whichScreen;
            this.movieType = movieType;
        }

        public static void DisplayMovies()
        {
            foreach (Movies movie in movieList)
            {
                movieCatalog(movie);
            }   
            
        }

        //Overflow functie als de user op sleutelwoorden wilt zoeken.
        public static void DisplayMovies(string[] terms)
        {
            foreach (Movies movie in movieList)
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
            foreach(Movies movie in movieList)
            {
                print += movie.movieID + ")" + movie.MovieName + "\n";
            }
            Console.WriteLine(print);
        }

        //deze method print de status van een zaal.

        public static void ScreenSeats(Movies movie)
        {
            Console.WriteLine($"Status van zaal {movie.whichScreen.screenNumber} tijdens {movie.MovieName} om {movie.startTime}.");
            string[] RowBlueprint = new string[movie.whichScreen.amountOfRows];
            for (int i = 0; i < RowBlueprint.Length; i++)
            {
                string RowI = "";
                for(int j = 0; j < RowBlueprint.Length; j++)
                {
                    //hier komt de check of de zitplaats niet in de json staat.
                    RowI += $"{j + 1} ";
                }
                RowBlueprint[i] = RowI;
            }
            for (int rowCounter = 1; rowCounter < movie.whichScreen.amountOfRows; rowCounter++)
            {
                Console.WriteLine($"Rij {rowCounter} : {RowBlueprint[rowCounter - 1]}");
            }
            Console.WriteLine($"VIP Rij : {movie.whichScreen.amountOfVip} / {movie.whichScreen.amountOfVip}");
        }

        //deze method initialiseert de films en zet ze in een ArrayList, waardoor de data makkelijk opnieuw te gebruiken is
        public static void InitMovies()
        {
            movieList.Add(new Movies(1, "No Time To Die", new DateTime(2020, 11, 12, 10, 00, 00), (Screen)Screen.screenList[0], 163, "Actie, Avontuur, Thriller", "Cary Joji Fukunaga", "Base"));
            movieList.Add(new Movies(2, "Knives Out", new DateTime(2020, 11, 28, 18, 00, 00), (Screen)Screen.screenList[0], 130, "Drama, Thriller", "Rian Johnson", "Base"));
            movieList.Add(new Movies(3, "The Passion", new DateTime(2020, 04, 09, 21, 30, 00), (Screen)Screen.screenList[1], 100, "Music", "david Grifhorst", "3D"));
            movieList.Add(new Movies(4, "Farewell", new DateTime(2020, 12, 31, 23, 00, 00), (Screen)Screen.screenList[1], 90, "Documentaire", "Pieter van Huystee", "IMAX"));
            movieList.Add(new Movies(5, "The Turning", new DateTime(2020, 04, 16, 13, 00, 00), (Screen)Screen.screenList[2], 100, "Horror", "Floria Sigismondi", "Base"));
            movieList.Add(new Movies(6, "Mission: Impossible - Fallout", new DateTime(2020, 04, 04, 12, 30, 00), (Screen)Screen.screenList[3], 145, "Actie", "Christopher McQuarrie", "Auro3D"));
            movieList.Add(new Movies(7, "Black Widow", new DateTime(2020, 04, 29, 14, 00, 00), (Screen)Screen.screenList[3], 130, "Actie, Avontuur, Science Fiction", "Cate Shortland", "IMAX"));
            movieList.Add(new Movies(8, "Honey Boy", new DateTime(2020, 04, 16, 16, 00, 00), (Screen)Screen.screenList[4], 94, "Drama", "Alma Har'el", "Base"));
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
                    foreach(Movies movie in movieList)
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
                    foreach (Movies movie in movieList)
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
            
        }
    }
}

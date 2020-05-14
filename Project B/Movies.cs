using System;
using System.Collections;
using System.Collections.Generic;
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
                    foreach(String part in movie.movieName.Split())
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


        public static void ScreenSeats(Movies movie)
        {
            Console.WriteLine($"Status van zaal {movie.whichScreen.screenNumber} tijdens {movie.movieName} om {movie.startTime} =");
            for (int rowCounter = 1; rowCounter < movie.whichScreen.amountOfRows; rowCounter++)
            {
                Console.WriteLine($"Rij {rowCounter} : {movie.whichScreen.amountOfSeatsPerRow} / {movie.whichScreen.amountOfSeatsPerRow}");
            }
            Console.WriteLine($"VIP Rij : {movie.whichScreen.amountOfVip} / {movie.whichScreen.amountOfVip}");
            Console.WriteLine($"Totaal : {movie.whichScreen.amountOfSeats} / {movie.whichScreen.amountOfSeats}");
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

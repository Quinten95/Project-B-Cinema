using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Movies
    {
        public int movieID;
        public string movieName;
        public DateTime startTime;
        public int runTime;
        public string genre;
        public string director;
        public Screen whichScreen;
        public static ArrayList movieList = new ArrayList();

        

        public Movies(int movieID, string movieName, DateTime startTime,
            Screen whichScreen, int runTime, string genre, string director)
        {
            this.movieID = movieID;
            this.movieName = movieName;
            this.startTime = startTime;
            this.runTime = runTime;
            this.genre = genre;
            this.director = director;
            this.whichScreen = whichScreen;
        }

        public static void DisplayMovies()
        {
            foreach (Movies movie in movieList)
            {
                movieCatalog(movie);
            }          
        }

        public static void InitMovies()
        {
            movieList.Add(new Movies(1, "movieOne", new DateTime(2020, 04, 04), (Screen) Screen.screenList[0], 147, "genre", "director"));
            movieList.Add(new Movies(2, "movieTwo", new DateTime(2020, 04, 04), (Screen)Screen.screenList[0], 99, "genre", "director"));
            movieList.Add(new Movies(3, "movieThree", new DateTime(2020, 04, 04), (Screen)Screen.screenList[1], 105, "genre", "director"));
            movieList.Add(new Movies(4, "movieFour", new DateTime(2020, 04, 04), (Screen)Screen.screenList[1], 109, "genre", "director"));
            movieList.Add(new Movies(5, "movieFive", new DateTime(2020, 04, 04), (Screen)Screen.screenList[2], 128, "genre", "director"));
            movieList.Add(new Movies(6, "Mission: Impossible - Fallout", new DateTime(2020, 04, 04), (Screen)Screen.screenList[3], 145, "Actie", "Christopher McQuarrie"));
            movieList.Add(new Movies(7, "movieSeven", new DateTime(2020, 04, 04), (Screen)Screen.screenList[3], 100, "genre", "director"));
            movieList.Add(new Movies(8, "movieEight", new DateTime(2020, 04, 04), (Screen)Screen.screenList[4], 121, "genre", "director"));
        }
        
    
  
        
        

        public static void movieCatalog(Movies movie)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Filmnummer: " + movie.movieID);
            Console.WriteLine(movie.movieName);
            Console.WriteLine("Zaal " + movie.whichScreen.screenNumber);
            Console.WriteLine("Starttijden = 09:00, 13:00, 17:00");
            Console.WriteLine("Filmlengte: " + movie.runTime + " minuten");
            Console.WriteLine("Genre: " + movie.genre);
            Console.WriteLine("Regisseur: " + movie.director);
            
        }
    }
}

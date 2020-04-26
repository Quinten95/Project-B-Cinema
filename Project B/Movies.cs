﻿using System;
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
        public string movieType;
        public static ArrayList movieList = new ArrayList();

        public Movies(int movieID, string movieName, DateTime startTime,
            Screen whichScreen, int runTime, string genre, string director, string movieType)
        {
            this.movieID = movieID;
            this.movieName = movieName;
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

        //deze method initialiseert de films en zet ze in een ArrayList, waardoor de data makkelijk opnieuw te gebruiken is
        public static void InitMovies()
        {
            movieList.Add(new Movies(1, "No Time To Die", new DateTime(2020, 11, 12), (Screen)Screen.screenList[0], 163, "Actie, Avontuur, Thriller", "Cary Joji Fukunaga", "Base"));
            movieList.Add(new Movies(2, "Knives Out", new DateTime(2020, 11, 28), (Screen)Screen.screenList[0], 130, "Drama, Thriller", "Rian Johnson", "Base"));
            movieList.Add(new Movies(3, "The Passion", new DateTime(2020, 04, 09), (Screen)Screen.screenList[1], 100, "Music", "david Grifhorst", "3D"));
            movieList.Add(new Movies(4, "Farewell", new DateTime(2020, 12, 31), (Screen)Screen.screenList[1], 90, "Docs", "Pieter van Huystee", "IMAX"));
            movieList.Add(new Movies(5, "The Turning", new DateTime(2020, 04, 16), (Screen)Screen.screenList[2], 100, "Horror", "Floria Sigismondi", "Base"));
            movieList.Add(new Movies(6, "Mission: Impossible - Fallout", new DateTime(2020, 04, 04), (Screen)Screen.screenList[3], 145, "Actie", "Christopher McQuarrie", "Auro3D"));
            movieList.Add(new Movies(7, "Black Widow", new DateTime(2020, 04, 29), (Screen)Screen.screenList[3], 130, "Actie, Advontuur, Science Fiction", "Cate Shortland", "IMAX"));
            movieList.Add(new Movies(8, "Honey Boy", new DateTime(2020, 04, 16), (Screen)Screen.screenList[4], 94, "Drama", "Alma Har'el", "Base"));
        }
        
    
  
        
        
        //deze method maakt per film die meegegeven wordt een mooi weergegeven detail overzicht
        public static void movieCatalog(Movies movie)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Filmnummer: " + movie.movieID);
            Console.WriteLine(movie.movieName);
            Console.WriteLine("Zaal " + movie.whichScreen.screenNumber);
            Console.WriteLine(movie.startTime.ToString("dd/MM/yyyy HH:mm"));
            Console.WriteLine("Filmlengte: " + movie.runTime + " minuten");
            Console.WriteLine("Genre: " + movie.genre);
            Console.WriteLine("Regisseur: " + movie.director);
            
        }
    }
}

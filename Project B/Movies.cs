using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Movies
    {
        private int movieID;
        private string movieName;
        private DateTime startTime;
        private int runTime;
        private string genre;
        private string director;
        public Screen WhichScreen { get; set; }

        public Movies(int movieID, string movieName, DateTime startTime, 
            Screen whichScreen, int runTime, string genre, string director)
        {
            this.movieID = movieID;
            this.movieName = movieName;
            this.startTime = startTime;
            this.runTime = runTime;
            this.genre = genre;
            this.director = director;
            this.WhichScreen = whichScreen;
        }
        public static Movies movieOne = new Movies(1, "movieOne", DateTime.Parse("6/22/2020 10:00:00 AM"), Screen.screenOne, 128, "genre", "director");


    }
    
}

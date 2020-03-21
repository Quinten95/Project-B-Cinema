using System;
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
        public int WhichScreen;

        public Movies(int movieID, string movieName, DateTime startTime,
            int whichScreen, int runTime, string genre, string director)
        {
            this.movieID = movieID;
            this.movieName = movieName;
            this.startTime = startTime;
            this.runTime = runTime;
            this.genre = genre;
            this.director = director;
            this.WhichScreen = whichScreen;
        }

        DateTime movieStart = DateTime.Parse("09:00:00 AM");

        public static Movies movieOne = new Movies(1, "movieOne", new DateTime(2020, 04, 04), Screen.screenOne.screenNumber, 112, "genre", "director");
        public static Movies movieTwo = new Movies(2, "movieTwo", new DateTime(2020, 04, 04), Screen.screenOne.screenNumber, 99, "genre", "director");
        public static Movies movieThree = new Movies(3, "movieThree", new DateTime(2020, 04, 04), Screen.screenTwo.screenNumber, 105, "genre", "director");
        public static Movies movieFour = new Movies(4, "movieFour", new DateTime(2020, 04, 04), Screen.screenTwo.screenNumber, 109, "genre", "director");
        public static Movies movieFive = new Movies(5, "movieFive", new DateTime(2020, 04, 04), Screen.screenThree.screenNumber, 128, "genre", "director");
        public static Movies movieSix = new Movies(6, "movieSix", new DateTime(2020, 04, 04), Screen.screenFour.screenNumber, 145, "genre", "director");
        public static Movies movieSeven = new Movies(7, "movieSeven", new DateTime(2020, 04, 04), Screen.screenFour.screenNumber, 100, "genre", "director");
        public static Movies movieEight = new Movies(8, "movieEight", new DateTime(2020, 04, 04), Screen.screenFive.screenNumber, 121, "genre", "director");


    }
}

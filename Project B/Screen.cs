using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Screen
    {
        public int screenNumber;
        public int amountOfSeats;
        public int amountOfVip;
        public int amountOfRows;
        public int amountOfSeatsPerRow;
        public static ArrayList screenList = new ArrayList();

        public Screen(int screenNumber, int amountOfSeats, int amountOfVip, int amountOfRows, int amountOfSeatsPerRow)
        {
            this.screenNumber = screenNumber;
            this.amountOfSeats = amountOfSeats;
            this.amountOfVip = amountOfVip;
            this.amountOfRows = amountOfRows;
            this.amountOfSeatsPerRow = amountOfSeatsPerRow;

        }

        //List of screens - Three with 150 seats, one with 300 seats, and one with 500 seats
        public static void InitScreens() {
            screenList.Add(new Screen(1, 150, 15, 10, 15));
            screenList.Add(new Screen(2, 150, 15, 10, 15));
            screenList.Add(new Screen(3, 150, 15, 10, 15));
            screenList.Add(new Screen(4, 300, 30, 30, 10));
            screenList.Add(new Screen(5, 500, 40, 25, 20));
        }

        //deze method print de status van een zaal.

        public static void ScreenSeats(Movies movie)
        {
            Console.WriteLine($"Status van zaal {movie.whichScreen.screenNumber} tijdens {movie.MovieName} om {movie.startTime}.");
            string[] RowBlueprint = new string[movie.whichScreen.amountOfRows];
            for (int i = 0; i < RowBlueprint.Length; i++)
            {
                string RowI = "";
                for (int j = 0; j < RowBlueprint.Length; j++)
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
            string vipRow = "";
            for (int k = 0; k < RowBlueprint.Length; k++)
            {
                //hier komt de check of de zitplaats niet in de json staat.
                vipRow += $"{k + 1} ";
            }
            Console.WriteLine($"VIP Rij ({movie.whichScreen.amountOfRows}): {vipRow}");
        }
    }

}

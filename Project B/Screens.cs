using System;
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

        public Screen(int screenNumber, int amountOfSeats, int amountOfVip, int amountOfRows, int amountOfSeatsPerRow)
        {
            this.screenNumber = screenNumber;
            this.amountOfSeats = amountOfSeats;
            this.amountOfVip = amountOfVip;
            this.amountOfRows = amountOfRows;
            this.amountOfSeatsPerRow = amountOfSeatsPerRow;

        }

        //List of screens - Three with 150 seats, one with 300 seats, and one with 500 seats
        public static Screen screenOne = new Screen(1, 150, 15, 10, 15);
        public static Screen screenTwo = new Screen(2, 150, 15, 10, 15);
        public static Screen screenThree = new Screen(3, 150, 15, 10, 15);
        public static Screen screenFour = new Screen(4, 300, 30, 30, 10);
        public static Screen screenFive = new Screen(5, 500, 30, 25, 20);
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Screen
    {
        private int screenNumber;
        private int amountOfSeats;
        private int amountOfVip;
        private int amountOfRows;
        private int amountOfSeatsPerRow;

        public static Screen screenOne = new Screen(1, 150, 15, 10, 15);

        public Screen(int screenNumber, int amountOfSeats, int amountOfVip, int amountOfRows, int amountOfSeatsPerRow)
        {
            this.screenNumber = screenNumber;
            this.amountOfSeats = amountOfSeats;
            this.amountOfVip = amountOfVip;
            this.amountOfRows = amountOfRows;
            this.amountOfSeatsPerRow = amountOfSeatsPerRow;

        }
    }

}

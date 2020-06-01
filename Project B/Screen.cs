using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Project_B
{
    class Screen
    {
        public int ScreenNumber { get; set; }
        public int AmountOfSeats { get; set; }
        public int AmountOfVip { get; set; }
        public int AmountOfRows { get; set; }
        public int AmountOfSeatsPerRow { get; set; }

        public static List<Screen> screenList = new List<Screen>();

        public Screen(int screenNumber, int amountOfVip, int amountOfRows, int amountOfSeatsPerRow)
        {
            this.ScreenNumber = screenNumber;
            this.AmountOfVip = amountOfVip;
            this.AmountOfRows = amountOfRows;
            this.AmountOfSeatsPerRow = amountOfSeatsPerRow;

            AmountOfSeats = amountOfSeatsPerRow * amountOfRows;

        }

        //List of screens - Three with 150 seats, one with 300 seats, and one with 500 seats
        public static void InitScreens() {
            screenList.Add(new Screen(1, 15, 10, 15));
            screenList.Add(new Screen(2, 15, 10, 15));
            screenList.Add(new Screen(3, 15, 10, 15));
            screenList.Add(new Screen(4, 30, 15, 20));
            screenList.Add(new Screen(5, 40, 20, 25));
        }

    }
}

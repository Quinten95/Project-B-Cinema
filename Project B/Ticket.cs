using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Ticket
    {
        private int screen;
        private int seat;
        private int row;
        private string movie;

        public Ticket(int screen, int seat, int row, string movie)
        {
            this.screen = screen;
            this.seat = seat;
            this.row = row;
            this.movie = movie;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Ticket
    {
        private Screen screen;
        private int seat;
        private int row;
        private Movies movie;
        private int numberOfPeople;

        public Ticket(Movies movie, int numberOfPeople)
        {
            this.movie = movie;
            this.numberOfPeople = numberOfPeople;

        }
    }
}

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
        public double priceOfTicket;

        public Ticket(Movies movie, Customer customer)
        {
            this.movie = movie;
            this.screen = movie.whichScreen;
            this.priceOfTicket = MoviePrice.calcTicketPrice(this, customer);
        }
    }
}

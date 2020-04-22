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
        private bool isVip;

        public Ticket(Movies movie, Customer customer, bool VipChoice)
        {
            this.movie = movie;
            this.screen = movie.whichScreen;
            this.isVip = VipChoice;
            this.priceOfTicket = MoviePrice.calcTicketPrice(movie, customer, isVip);
        }
    }
}

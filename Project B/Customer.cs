﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Customer
    {
        private string customerName;
        private DateTime birthday;
        public int Age { get; set; }
        public string Email { get; set; }

        public Customer(string customerName, DateTime birthday)
        {
            this.customerName = customerName;
            this.birthday = birthday;
        }
    }
}

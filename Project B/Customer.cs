using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Customer
    {
        public string CustomerName { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerUserName { get; set; }

        public Customer(string customerName, DateTime birthday, string email)
        {
            this.CustomerName = customerName;
            this.Birthday = birthday;
            this.Email = email;
            var today = DateTime.Today;
            this.Age = today.Year - birthday.Year;
        }
    }
}

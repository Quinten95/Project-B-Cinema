using System;
using System.Collections.Generic;
using System.Text;

namespace Project_B
{
    class Snacks
    {
        private string snackName;
        private string nutritionInfo;
        public int Price { get; set; }

        public Snacks(string snackName, string nutritionInfo)
        {
            this.snackName = snackName;
            this.nutritionInfo = nutritionInfo;
        }

    }
}

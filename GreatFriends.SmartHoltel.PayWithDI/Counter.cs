using System;
using System.Collections.Generic;
using System.Text;

namespace GreatFriends.SmartHoltel.PayWithDI
{
    public class Counter
    {
        private int prefix;
        private int nextId = 1;

        public Counter()
        {
            var r = new Random();
            prefix = r.Next(100, 999 + 1);
        }


        public void Print()
        {
            Console.WriteLine($"{prefix}---{nextId++:0000}");
        }

    }
}

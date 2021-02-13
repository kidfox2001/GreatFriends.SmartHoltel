using System;
using System.Collections.Generic;
using System.Text;

namespace GreatFriends.SmartHoltel.PayWithDI
{
    public class Counter : ICounter
    {
        private readonly IEnumerable<IOutput> outputs;
        private int prefix;
        private int nextId = 1;

        public Counter(IEnumerable<IOutput> outputs)
        {
            var r = new Random();
            prefix = r.Next(100, 999 + 1);
            this.outputs = outputs;
        }
        public void Print()
        {
            foreach (var output in outputs)
                output.Write($"{prefix}-{nextId++:0000}");
        }
    }

}

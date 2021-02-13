using System;
using System.Collections.Generic;
using System.Text;

namespace GreatFriends.SmartHoltel.PayWithDI
{
    public interface IOutput
    {
        void Write(string s);
    }


    public class ConsoleOutput : IOutput
    {
        public void Write(string s)
        {
            Console.WriteLine(s);
        }
    }

    public class SpeakerOutput : IOutput
    {
        public void Write(string s)
        {
            Console.Beep();
        }
    }
}

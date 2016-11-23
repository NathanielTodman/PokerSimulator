using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Betting
    {
        public double PlayerWallet { get; set; }
        public double ComputerWallet { get; set; }
        public double Pot { get; set; }

        double Bet;

        public Betting()
        {
            Console.WriteLine("How much do you want to bet?");
            Bet = Convert.ToDouble(Console.ReadLine());

          
        }
    }
}

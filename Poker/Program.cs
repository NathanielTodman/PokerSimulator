using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetWindowSize(65, 50);
            //Remove scroll bars by setting buffer to window size
            Console.BufferWidth = 65;
            Console.BufferHeight = 50;            
            Console.Title = "Nathaniel's 1v1 Poker Simulator";
            DealCards dc = new DealCards();
            bool Quit = false;

            while (!Quit)
            {
                dc.Deal();
                int x = 0; int y = 0;
                Console.SetCursorPosition(x,y);

                char selection = ' ';
                while(!selection.Equals('Y') && !selection.Equals('N'))
                {
                    Console.SetCursorPosition(0, 48);                 
                    Console.WriteLine("Play again? (Y)es or (N)o");
                    selection = Convert.ToChar(Console.ReadLine().ToUpper());

                    if (selection.Equals('Y'))
                        Quit = false;
                    else if (selection.Equals('N'))
                        Quit = true;
                    else
                        Console.WriteLine("Invalid input, try again.");

                }
            }
        }
    }
}

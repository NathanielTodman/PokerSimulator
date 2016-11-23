using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class DrawCards
    {
        //Draw cards outline
        public static void DrawCardOutline(int xCoor, int yCoor)
        {
            Console.BackgroundColor = ConsoleColor.White;

            int x = xCoor * 12; //10 underscores to draw the line and one space either side for the edges
            int y = yCoor;


            Console.SetCursorPosition(x, y);//Starting position of where to draw the card
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" __________\n");//Top edge of the card

            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(x, y + 1 + i);

                if (i != 9)
                {
                    Console.WriteLine("|          |");//Left and right edges of the card
                }
                else
                {
                    Console.WriteLine("|__________|");//Bottom of the card
                }
            }
        }
        //Display suit and value on the card
        public static void DrawCardSuitValue(Card card, int xCoor, int yCoor)
        {
            char cardSuit = ' ';
            int x = xCoor * 12;
            int y = yCoor;

            //Encode each byte array from the CodePage437 into a character for the suit symbol
            //Hearts and diamonds are red, spades and clubs are black
            switch (card.mySuit)
            {
                case Card.Suit.Hearts:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 3 })[0];
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Card.Suit.Diamonds:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 4 })[0];
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Card.Suit.Clubs:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 5 })[0];
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Card.Suit.Spades:
                    cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 6 })[0];
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
            }

            //Display encoded character on all 4 corners and value
            Console.SetCursorPosition(x + 2, y + 2);
            Console.Write(cardSuit);
            Console.SetCursorPosition(x + 9, y + 2);
            Console.Write(cardSuit);
            Console.SetCursorPosition(x + 2, y + 9);
            Console.Write(cardSuit);
            Console.SetCursorPosition(x + 9, y + 9);
            Console.Write(cardSuit);
            Console.SetCursorPosition(x + 4, y + 5);
            Console.Write(card.myValue);
            Console.ForegroundColor = ConsoleColor.Black;

        }
    }
}


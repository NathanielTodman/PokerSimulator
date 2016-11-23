using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class DealCards : DeckOfCards
    {
        private Card[] PlayerHand;
        private Card[] ComputerHand;
        private Card[] SortedPlayerHand;
        private Card[] SortedComputerHand;
        private Card[] FlopHand;
        public Card[] FirstPlayerHand;
        public Card[] FirstComputerHand;


        public DealCards()
        {
            FlopHand = new Card[5];
            PlayerHand = new Card[7];
            FirstPlayerHand = new Card[2];
            SortedPlayerHand = new Card[7];
            ComputerHand = new Card[7];
            FirstComputerHand = new Card[2];
            SortedComputerHand = new Card[7];

        }
        public void Deal()
        {
            SetupDeck(); //Create deck and then shuffle them
            GetHand(); //Deal hand
            SortCards(); //Sort cards  
            DisplayCards(); //Display cards
            EvaluateHands(); //Evaluate hand

        }
        public void GetHand()
        {
            //Deal 2 cards for the player
            for (int i = 0; i < 2; i++)
                PlayerHand[i] = GetDeck[i];
            //Deal 2 cards for the CPU
            for (int i = 2; i < 4; i++)
                ComputerHand[i - 2] = GetDeck[i];
            //Deal 5 cards for flop      
            for (int i = 4; i < 9; i++)
                FlopHand[i - 4] = GetDeck[i];
            //Remember first hands before merging with flop for evaluating personal high card
            FirstPlayerHand = PlayerHand;
            FirstComputerHand = ComputerHand;
            //Add flop hand to both hands
            FlopHand.CopyTo(PlayerHand, 2);
            FlopHand.CopyTo(ComputerHand, 2);


        }
        public void SortCards()
        {
            var QueryPlayer = from hand in PlayerHand
                              orderby hand.myValue
                              select hand;
            var QueryComputer = from hand in ComputerHand
                                orderby hand.myValue
                                select hand;
            var index = 0;

            foreach(var element in QueryPlayer.ToList())
            {
                SortedPlayerHand[index] = element;
                index++;
            }
            index = 0;
            foreach (var element in QueryComputer.ToList())
            {
                SortedComputerHand[index] = element;
                index++;
            }


        }
        public void DisplayCards()
        {
            Console.Clear();
            int x = 0; //Horizontal position of cursor
            int y = 1; //Vertical position of cursor

            //Display player hand
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Players hand");
            for (int i = 0; i < 2; i++) 
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(PlayerHand[i], x, y);
                x++;//Move right for next card
            }

            y = 15; //Move the row of CPU cards down below the players
            x = 0;
            //Display flop
            Console.SetCursorPosition(x, 14);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Flop");
            for (int i = 0; i < 5; i++)
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(FlopHand[i], x, y);
                x++;//Move right for next card
            }


            y = 29; //Move the row of CPU cards down below the players
            x = 0;
            //Display CPU hand
            Console.SetCursorPosition(x, 28);
            Console.ForegroundColor = ConsoleColor.DarkYellow;            
            Console.WriteLine("Computers hand");            
            for (int i = 0; i < 2; i++)
            {                
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(ComputerHand[i], x, y);
                x++;//Move right for next card
            }




        }
        public void EvaluateHands()
        {
            //evaluate each players hand pre flop
            HandEvaluator firstPlayerHandEvaluator = new HandEvaluator(FirstPlayerHand);
            HandEvaluator firstComputerHandEvaluator = new HandEvaluator(FirstComputerHand);
            //get each players high card value
            Hand playerHighCard = firstPlayerHandEvaluator.EvaluateHand();
            Hand computerHighCard = firstComputerHandEvaluator.EvaluateHand();

            //create player's computer's evaluation objects (passing SORTED hand to constructor)
            HandEvaluator playerHandEvaluator = new HandEvaluator(SortedPlayerHand);
            HandEvaluator computerHandEvaluator = new HandEvaluator(SortedComputerHand);

            //get the player's and computer's hand
            Hand playerHand = playerHandEvaluator.EvaluateHand();
            Hand computerHand = computerHandEvaluator.EvaluateHand();

            //display each hand
            Console.WriteLine("\n\n\n\n\n\n\n\nPlayer's Hand: " + playerHand);
            Console.WriteLine("\nComputer's Hand: " + computerHand);

            //evaluate hands
            if (playerHand > computerHand)
            {
                Console.WriteLine("Player WINS!");
            }
            else if (playerHand < computerHand)
            {
                Console.WriteLine("Computer WINS!");
            }
            else 
            
            
            //if the hands are the same, evaluate the values
            {
                //first evaluate who has higher value of poker hand
                if (playerHandEvaluator.HandValues.Total > computerHandEvaluator.HandValues.Total)
                    Console.WriteLine("Player WINS!");
                else if (playerHandEvaluator.HandValues.Total < computerHandEvaluator.HandValues.Total)
                    Console.WriteLine("Computer WINS!");
                //if both have the same poker hand (for example, both have a pair of queens), 
                //then the player with the next higher card wins
                else if (firstPlayerHandEvaluator.HandValues.HighCard > firstComputerHandEvaluator.HandValues.HighCard)
                    Console.WriteLine("Player WINS!");
                else if (firstPlayerHandEvaluator.HandValues.HighCard < firstComputerHandEvaluator.HandValues.HighCard)
                    Console.WriteLine("Computer WINS!");
                //if high card is of same value check second card in players hand
                else if (firstPlayerHandEvaluator.HandValues.SecondHighCard > firstComputerHandEvaluator.HandValues.SecondHighCard)
                    Console.WriteLine("Player WINS!");
                else if (firstPlayerHandEvaluator.HandValues.SecondHighCard < firstComputerHandEvaluator.HandValues.SecondHighCard)
                    Console.WriteLine("Computer WINS!");
                else
                    Console.WriteLine("DRAW, everyone's a winner!");
            }
        }
    }
}

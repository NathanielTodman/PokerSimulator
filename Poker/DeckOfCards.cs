using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class DeckOfCards : Card
    {
        const int NumberOfCards = 52; //Total number of cards in the deck
        private Card[] Deck; //Array of all the cards

        public DeckOfCards()
        {
            Deck = new Card[NumberOfCards];
        }

        public Card[] GetDeck { get { return Deck; } } //Get current deck

        //Create deck of 52 cards, 13 per suit, 4 suits
        public void SetupDeck()
        {
            int i = 0;
            foreach(Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach(Value v in Enum.GetValues(typeof(Value)))
                {
                    Deck[i] = new Card { mySuit = s, myValue = v };
                    i++;
                }
            }
            ShuffleCards();
        }
        // Shuffle the deck
        public void ShuffleCards()
        {
            Random rand = new Random();
            Card temp;
            //Run the shuffle 1000 times
            for (int shuffle = 0; shuffle < 1000; shuffle++)
            {
                for (int i = 0; i < NumberOfCards; i++)
                {
                    //Switch the cards
                    int SecondCardIndex = rand.Next(13);
                    temp = Deck[i];
                    Deck[i] = Deck[SecondCardIndex];
                    Deck[SecondCardIndex] = temp;
                }
            }
        }
    }
}

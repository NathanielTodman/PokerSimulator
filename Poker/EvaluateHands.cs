using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public enum Hand
    {
        Nothing,
        HighCard,
        OnePair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind
    }

    public struct HandValue
    {
        public int Total { get; set; }
        public int HighCard { get; set; }
        public int SecondHighCard { get; set; }
    }

    class HandEvaluator : Card
    {
        private int heartsSum;
        private int diamondSum;
        private int clubSum;
        private int spadesSum;
        private Card[] cards;
        private HandValue handValue;

        public HandEvaluator(Card[] sortedHand)
        {
            heartsSum = 0;
            diamondSum = 0;
            clubSum = 0;
            spadesSum = 0;
            cards = new Card[7];
            Cards = sortedHand;
            handValue = new HandValue();
            if((int)cards[0].myValue > (int)cards[1].myValue)
            {
                handValue.HighCard = (int)cards[0].myValue;
                handValue.SecondHighCard = (int)cards[1].myValue;
            }
            else if ((int)cards[1].myValue > (int)cards[0].myValue)
            {
                handValue.HighCard = (int)cards[1].myValue;
                handValue.SecondHighCard = (int)cards[0].myValue;
            }

        }

        public HandValue HandValues
        {
            get { return handValue; }
            set { handValue = value; }
        }

        public Card[] Cards
        {
            get { return cards; }
            set
            {
                cards[0] = value[0];
                cards[1] = value[1];
                cards[2] = value[2];
                cards[3] = value[3];
                cards[4] = value[4];
                cards[5] = value[5];
                cards[6] = value[6];
            }
        }

        public Hand EvaluateHand()
        {
            //get the number of each suit on hand
            getNumberOfSuit();
            if (FourOfKind())
                return Hand.FourKind;
            else if (FullHouse())
                return Hand.FullHouse;
            else if (Flush())
                return Hand.Flush;
            else if (Straight())
                return Hand.Straight;
            else if (ThreeOfKind())
                return Hand.ThreeKind;
            else if (TwoPairs())
                return Hand.TwoPairs;
            else if (OnePair())
                return Hand.OnePair;

            //if the hand is nothing, than the player with highest card wins
            
            return Hand.HighCard;

            

        
    }


    private void getNumberOfSuit()
        {
            foreach (var element in Cards)
            {
                if (element.mySuit == Card.Suit.Hearts)
                    heartsSum++;
                else if (element.mySuit == Card.Suit.Diamonds)
                    diamondSum++;
                else if (element.mySuit == Card.Suit.Clubs)
                    clubSum++;
                else if (element.mySuit == Card.Suit.Spades)
                    spadesSum++;
            }
        }

        private bool FourOfKind()
        {
            //if the first 4 cards, add values of the four cards and last card is the highest
            if (cards[3].myValue == cards[4].myValue && cards[3].myValue == cards[5].myValue && cards[3].myValue == cards[6].myValue)
            {
                handValue.Total = (int)cards[1].myValue * 4;
                return true;
            }
            else if (cards[2].myValue == cards[3].myValue && cards[2].myValue == cards[4].myValue && cards[2].myValue == cards[5].myValue)
            {
                handValue.Total = (int)cards[2].myValue * 4;
                return true;
            }
            else if (cards[1].myValue == cards[2].myValue && cards[1].myValue == cards[3].myValue && cards[1].myValue == cards[4].myValue)
            {
                handValue.Total = (int)cards[1].myValue * 4;
                return true;
            }
            else if (cards[0].myValue == cards[1].myValue && cards[0].myValue == cards[2].myValue && cards[0].myValue == cards[3].myValue)
            {
                handValue.Total = (int)cards[0].myValue * 4;
                return true;
            }

            return false;
        }

        private bool FullHouse()
        {
            //the first three cards and last two cards are of the same value
            //first two cards, and last three cards are of the same value
            if
                ((cards[2].myValue == cards[3].myValue && cards[2].myValue == cards[4].myValue && cards[5].myValue == cards[6].myValue) ||
                (cards[2].myValue == cards[3].myValue && cards[4].myValue == cards[5].myValue && cards[4].myValue == cards[6].myValue))
            {
                handValue.Total = (int)(cards[2].myValue) + (int)(cards[3].myValue) + (int)(cards[4].myValue) +
                    (int)(cards[5].myValue) + (int)(cards[6].myValue);
                return true;
            }
            else if
                ((cards[1].myValue == cards[2].myValue && cards[1].myValue == cards[3].myValue && cards[4].myValue == cards[5].myValue) ||
                (cards[1].myValue == cards[2].myValue && cards[3].myValue == cards[4].myValue && cards[3].myValue == cards[5].myValue))
            {
                handValue.Total = (int)(cards[1].myValue) + (int)(cards[2].myValue) + (int)(cards[3].myValue) +
                    (int)(cards[4].myValue) + (int)(cards[5].myValue);
                return true;
            }
            else if ((cards[0].myValue == cards[1].myValue && cards[0].myValue == cards[2].myValue && cards[3].myValue == cards[4].myValue) ||
                (cards[0].myValue == cards[1].myValue && cards[2].myValue == cards[3].myValue && cards[2].myValue == cards[4].myValue))
            {
                handValue.Total = (int)(cards[0].myValue) + (int)(cards[1].myValue) + (int)(cards[2].myValue) +
                    (int)(cards[3].myValue) + (int)(cards[4].myValue);
                return true;
            }
            return false;
        }

        private bool Flush()
        {
            //if all suits are the same
            if (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
            {
                //if flush, the player with higher cards win
                //whoever has the last card the highest, has automatically all the cards total higher
                handValue.Total = (int)cards[6].myValue;
                return true;
            }

            return false;
        }

        private bool Straight()
        {
            //if 5 consecutive values
            if (cards[2].myValue + 1 == cards[3].myValue &&
                cards[3].myValue + 1 == cards[4].myValue &&
                cards[4].myValue + 1 == cards[5].myValue &&
                cards[5].myValue + 1 == cards[6].myValue)
            {
                //player with the highest value of the last card wins
                handValue.Total = (int)cards[6].myValue;
                return true;
            }

            else if (
                cards[1].myValue + 1 == cards[2].myValue &&
                cards[2].myValue + 1 == cards[3].myValue &&
                cards[3].myValue + 1 == cards[4].myValue &&
                cards[4].myValue + 1 == cards[5].myValue)
            {
                handValue.Total = (int)cards[5].myValue;
                return true;
            }
            else if (
                cards[0].myValue + 1 == cards[1].myValue &&
                cards[1].myValue + 1 == cards[2].myValue &&
                cards[2].myValue + 1 == cards[3].myValue &&
                cards[3].myValue + 1 == cards[4].myValue)
            {
                //player with the highest value of the last card wins
                handValue.Total = (int)cards[4].myValue;
                return true;
            }


            return false;
        }

        private bool ThreeOfKind()
        {
            //if 3 cards are the same
            if (cards[4].myValue == cards[5].myValue && cards[4].myValue == cards[6].myValue)
            {
                handValue.Total = (int)cards[6].myValue * 3;
                return true;
            }
            else if (cards[3].myValue == cards[4].myValue && cards[3].myValue == cards[5].myValue)
            {
                handValue.Total = (int)cards[5].myValue * 3;
                return true;
            }

            else if (cards[2].myValue == cards[3].myValue && cards[2].myValue == cards[4].myValue)
            {
                handValue.Total = (int)cards[4].myValue * 3;
                return true;
            }
            else if (cards[1].myValue == cards[2].myValue && cards[1].myValue == cards[3].myValue)
            {
                handValue.Total = (int)cards[3].myValue * 3;
                return true;
            }
            else if (cards[0].myValue == cards[1].myValue && cards[0].myValue == cards[2].myValue)
            {
                handValue.Total = (int)cards[2].myValue * 3;
                return true;
            }

            return false;
        }

        private bool TwoPairs()
        {
            //[6]+[5] 1st pair and check for second pair
            if (cards[6].myValue == cards[5].myValue && cards[4].myValue == cards[3].myValue)
            {
                handValue.Total = ((int)cards[4].myValue * 2) + ((int)cards[6].myValue * 2);
                return true;
            }
            else if (cards[6].myValue == cards[5].myValue && cards[3].myValue == cards[2].myValue)
            {
                handValue.Total = ((int)cards[3].myValue * 2) + ((int)cards[6].myValue * 2);
                return true;
            }
            else if (cards[6].myValue == cards[5].myValue && cards[2].myValue == cards[1].myValue)
            {
                handValue.Total = ((int)cards[2].myValue * 2) + ((int)cards[6].myValue * 2);
                return true;
            }
            else if (cards[6].myValue == cards[5].myValue && cards[1].myValue == cards[0].myValue)
            {
                handValue.Total = ((int)cards[1].myValue * 2) + ((int)cards[6].myValue * 2);
                return true;
            }
            //[5]+[4] first pair and check for second pair
            else if (cards[5].myValue == cards[4].myValue && cards[3].myValue == cards[2].myValue)
            {
                handValue.Total = ((int)cards[3].myValue * 2) + ((int)cards[5].myValue * 2);
                return true;
            }
            else if (cards[5].myValue == cards[4].myValue && cards[2].myValue == cards[1].myValue)
            {
                handValue.Total = ((int)cards[2].myValue * 2) + ((int)cards[5].myValue * 2);
                return true;
            }
            else if (cards[5].myValue == cards[4].myValue && cards[1].myValue == cards[0].myValue)
            {
                handValue.Total = ((int)cards[1].myValue * 2) + ((int)cards[5].myValue * 2);
                return true;
            }
            //[4]+[3] pair check for second pair
            else if (cards[4].myValue == cards[3].myValue && cards[2].myValue == cards[1].myValue)
            {
                handValue.Total = ((int)cards[2].myValue * 2) + ((int)cards[4].myValue * 2);
                return true;
            }
            else if (cards[4].myValue == cards[3].myValue && cards[1].myValue == cards[0].myValue)
            {
                handValue.Total = ((int)cards[1].myValue * 2) + ((int)cards[4].myValue * 2);
                return true;
            }
            //[3]+[2] pair check for second pair
            else if (cards[3].myValue == cards[2].myValue && cards[1].myValue == cards[0].myValue)
            {
                handValue.Total = ((int)cards[1].myValue * 2) + ((int)cards[3].myValue * 2);
                return true;
            }
            return false;
        }

        private bool OnePair()
        {
            //if 1,2 -> 5th card has the highest value
            //2.3
            //3,4
            //4,5 -> card #3 has the highest value
            if (cards[6].myValue == cards[5].myValue)
            {
                handValue.Total = (int)cards[6].myValue * 2;
                return true;
            }
            else if (cards[5].myValue == cards[4].myValue)
            {
                handValue.Total = (int)cards[5].myValue * 2;
                return true;
            }
            else if (cards[4].myValue == cards[3].myValue)
            {
                handValue.Total = (int)cards[4].myValue * 2;
                return true;
            }
            else if (cards[3].myValue == cards[2].myValue)
            {
                handValue.Total = (int)cards[3].myValue * 2;
                return true;
            }
            else if (cards[2].myValue == cards[1].myValue)
            {
                handValue.Total = (int)cards[2].myValue * 2;
                return true;
            }
            else if (cards[1].myValue == cards[0].myValue)
            {
                handValue.Total = (int)cards[1].myValue * 2;
                return true;
            }
            return false;

        }
    }
}

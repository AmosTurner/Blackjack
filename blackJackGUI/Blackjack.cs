using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackJackGUI
{
    public class Blackjack
    {
        private Deck Deck = new Deck();
        private List<Card> playerHand = new List<Card>();
        private List<Card> dealerHand = new List<Card>();
        private const int handNum = 2; // number of cards dealt to players

        public Blackjack()
        {
            Start();
        }

        // Accessor and mutator methods
        public List<Card> getPlayerHand()
        {
            return playerHand;
        }
        public List<Card> getDealerHand()
        {
            return dealerHand;
        }

        // Starts a BlackJack game
        public void Start()
        {
            Deck = new Deck();
            playerHand.Clear();
            dealerHand.Clear();
            for (int i = 0; i < handNum; i++)
            {
                DealCardToPlayer();
                DealCardToDealer();
            }
        }
        
        public void DealCardToPlayer()
        {
            Card c = Deck.DrawFromDeck();
            playerHand.Add(c);
        }

        public void DealCardToDealer()
        {
            Card c = Deck.DrawFromDeck();
            dealerHand.Add(c);
        }

        public int GetPlayerSum()
        {
            int sum = 0;
            foreach (Card c in playerHand)
            {
                sum += GetBlackjackValue(c);
            }

            return sum;
        }

        public int GetDealerSumBefore()
        {
            int sum = GetBlackjackValue(dealerHand[1]);

            return sum;
        }

        public int GetDealerSum()
        {
            int sum = 0;
            foreach (Card c in dealerHand)
            {
                sum += GetBlackjackValue(c);
            }

            return sum;
        }

        public int GetBlackjackValue(Card c)
        {
            if (c.getRank() > 10) return 10;
            return c.getRank();
        }

        public bool CompareHands()
        {
            if (GetDealerSum() > GetPlayerSum())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Card Hit()
        {
            Card c = Deck.DrawFromDeck();
            playerHand.Add(c);
            return c;
        }

        public Card DealerHit()
        {
            Card c = Deck.DrawFromDeck();
            dealerHand.Add(c);
            return c;
        }

        public List<Card> Stand()
        {
            List<Card> cards = new List<Card>();
            while (true)
            {
                if (GetDealerSum() < 17) 
                {
                    cards.Add(DealerHit());
                }
                else
                {
                    break;
                }
            }
            return cards;
        }

        public bool CheckWinner()
        {
            if ((GetDealerSum() >= 17 && GetDealerSum() <= 21) && (CompareHands()))
                {
                    Console.WriteLine("Dealer wins!");
                    return false;
                }
            // Need this code block to output that the dealer lost, not busted
            //else if ((GetDealerSum() <= 21) && (CompareHands()))
            //{

            //}
            else
                {
                    Console.WriteLine("Dealer busts. Player wins!");
                    return true;
                }
        }
        public bool IsBust()
        {
            if (GetPlayerSum() > 21)
            {
                Console.WriteLine("Bust. You lose :(");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
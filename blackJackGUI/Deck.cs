using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackJackGUI
{
    public class Deck
    {
        private List<Card> Cards = new List<Card>();

        public Deck()
        {
            Init();
            Shuffle();
        }

        public void Init()
        {
            MakeSuit("hearts");
            MakeSuit("spades");
            MakeSuit("diamonds");
            MakeSuit("clubs");
        }
        public void MakeSuit(String suitname)
        {
            for (int i = 1; i < 14; i++)
            {
                Card c = new Card(suitname, i);
                Cards.Add(c);
            }
        }

        /* Swaps a random card in the deck with another random card in the deck for 1000 iterations */
        public void Shuffle()
        {
            for (int i = 0; i < 1000; i++)
            {
                Random random = new Random();
                int index = random.Next(0, Cards.Count);
                int index2 = random.Next(0, Cards.Count);
                Card temp = Cards[index];
                Cards[index] = Cards[index2];
                Cards[index2] = temp;
            }
        }

        // removes top card and returns it to player
        public Card DrawFromDeck()
        {
            Card c = Cards[0];
            Cards.RemoveAt(0);
            return c;
        }

    }
}

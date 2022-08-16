using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackJackGUI
{
    public class Card
    {
        private String Suit;
        private int Rank;

        public Card(String Suit, int Rank)
        {
            this.Suit = Suit;
            this.Rank = Rank;
        }

        public int getRank()
        {
            return Rank;
        }

        public String getSuit()
        {
            return Suit;
        }

        public String GetName()
        {
            if (Rank == 1)
            {
                return "Ace of " + Suit;
            }
            if (Rank == 11)
            {
                return "Jack of " + Suit;
            }
            if (Rank == 12)
            {
                return "Queen of " + Suit;
            }
            if (Rank == 13)
            {
                return "King of " + Suit;
            }

            return Rank + " of " + Suit;
        }

    }
}

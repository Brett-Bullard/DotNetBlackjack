using System;

namespace CardLibrary
{
    public enum Suite{
        Spade,
        Heart,
        Club,
        Diamond
    }

    public enum CardName{
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
    public class Card: IEquatable<Card>
    {
        public readonly Suite Suite;
        public readonly CardName CardName;
        public readonly int[] Values;
        public Card(Suite suite, CardName cardName, int[] values)
        {
            Suite = suite; 
            CardName = cardName; 
            Values = values; 

        }

        public bool Equals(Card card)
        {
            return Suite == card.Suite && CardName == card.CardName; 
        }


    }
}

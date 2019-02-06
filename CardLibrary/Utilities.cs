using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CardLibrary
{

    public static class ListUtilities
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        private static Random random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


    }
    public static class CardUtilities
    {


        public static int[] CardValues(this List<Card> cards)
        {
            int value = 0;
            int secondaryValue = 0;
            foreach (Card card in cards)
            {
                value += card.Values[0];
                if (card.CardName == CardName.Ace)
                {
                    secondaryValue += card.Values[1];
                }
                else
                {
                    secondaryValue += card.Values[0];
                }
            }
            return new int[] { value, secondaryValue };
        }

        public static bool HasBlackJack(this List<Card> cards)
        {
            int[] cardValues = CardValues(cards);

            return cardValues[0] == 21 || cardValues[1] == 21;
        }
        public static int BestBlackJackValue(this List<Card> cards)
        {
            int[] cardValues = CardValues(cards);

            if (cardValues[0] > 21)
                return cardValues[1];
            else if (cardValues[1] > 21)
                return cardValues[0];
            if (cardValues[0] > cardValues[1])
                return cardValues[0];
            else
                return cardValues[1];
        }

        public static bool IsBusted(this List<Card> cards)
        {
            int[] cardValues = CardValues(cards); 
            return cardValues[0] > 21 && cardValues[1] > 21;
        }


    }
}
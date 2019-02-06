using System;
using System.Collections.Generic;

namespace CardLibrary
{
    public enum DeckType
    {
        Blackjack,
        Poker
    }
    public abstract class Deck
    {
        protected abstract Card[] GetDeck();
        protected List<Card> Cards;
        protected int DeckSize = 52;
        private List<Card> DealtCards;
        public int NumDecks;

        protected Deck()
        {
            Cards = new List<Card>();
            DealtCards = new List<Card>();

        }

        public void GenerateDecks(int numDecks)
        {
            for (int i = 0; i < numDecks; i++)
            {
                Cards.AddRange(GetDeck());
            }
            NumDecks = numDecks;
        }

        public void AddDeck()
        {
            Cards.AddRange(GetDeck());
            NumDecks++;
        }

        public void ShuffleDeck()
        {
            Cards.Shuffle();
        }

        public Card GetCard()
        {
            Card card = Cards[0];
            Cards.Remove(card);
            DealtCards.Add(card);
            return card;
        }

        public List<Card> GetCardsForPlay(int numCards)
        {
            List<Card> cardsForPlay = new List<Card>(); 
            for(int i = 0; i< numCards; i++)
            {
                cardsForPlay.Add(GetCard()); 
            }

            return cardsForPlay; 
        }

        public void GatherCards()
        {
            Cards.AddRange(DealtCards);
            DealtCards = new List<Card>();
        }

    }

    public class BlackJackDeck : Deck
    {

        protected override Card[] GetDeck()
        {
            Card[] deck = new Card[DeckSize];
            int counter = 0;
            foreach (Suite suite in ListUtilities.GetValues<Suite>())
            {
                foreach (CardName cardName in ListUtilities.GetValues<CardName>())
                {
                    if (cardName == CardName.Ace)
                    {
                        deck[counter] = new Card(suite, cardName, new int[] { 1, 11 });
                    }
                    else if (cardName == CardName.King || cardName == CardName.Queen || cardName == CardName.Jack)
                    {
                        deck[counter] = new Card(suite, cardName, new int[] { 10 });
                    }
                    else
                    {
                        int value = 0;
                        switch (cardName.ToString().ToLower()) {
                            case "one":
                                value = 1;
                                break;
                            case "two":
                                value = 2;
                                break;
                            case "three":
                                value = 3;
                                break;
                            case "four":
                                value = 4;
                                break;
                            case "five":
                                value = 5;
                                break;
                            case "six":
                                value = 6;
                                break;
                            case "seven":
                                value = 7;
                                break;
                            case "eight":
                                value = 8;
                                break;
                            case "nine":
                                value = 9;
                                break;
                            case "ten":
                                value = 10;
                                break;
                            default:
                                value = 0;
                                Console.WriteLine("Error parsing " + cardName.ToString() + " to int value");
                                break; 
                            
                        }

                        deck[counter] = new Card(suite, cardName, new int[] { value });
                    }
                    counter++;
                }
            }
            return deck;
        }
    }

    public class PokerDeck : Deck
    {

        protected override Card[] GetDeck()
        {
            Card[] deck = new Card[DeckSize];
            int counter = 0;
            foreach (Suite suite in ListUtilities.GetValues<Suite>())
            {
                foreach (CardName cardName in ListUtilities.GetValues<CardName>())
                {
                    if (cardName == CardName.Ace)
                    {
                        deck[counter] = new Card(suite, cardName, new int[] { 14 });
                    }
                    else if (cardName == CardName.Jack)
                    {
                        deck[counter] = new Card(suite, cardName, new int[] { 11 });
                    }
                    else if (cardName == CardName.Queen)
                    {
                        deck[counter] = new Card(suite, cardName, new int[] { 12 });
                    }
                    else if (cardName == CardName.King)
                    {
                        deck[counter] = new Card(suite, cardName, new int[] { 13 });
                    }
                    else
                    {
                        int value = 0;
                        bool parsed = int.TryParse(cardName.ToString(), out value);
                        if (!parsed)
                        {
                            //potentiall log something here
                            Console.WriteLine("Error parsing " + cardName.ToString() + " to an int value, setting value to 0");
                        }
                        deck[counter] = new Card(suite, cardName, new int[] { value });
                    }
                    counter++;
                }
            }
            return deck;
        }
    }

    public class CardFactory
    {
        public Deck GetDeckType(DeckType deckType)
        {
            switch (deckType)
            {
                case DeckType.Blackjack:
                    return new BlackJackDeck();
                case DeckType.Poker:
                    return new PokerDeck();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}